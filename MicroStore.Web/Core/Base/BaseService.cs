using MicroStore.Web.Core.DTOs;
using MicroStore.Web.Core.Enums;
using MicroStore.Web.Features.Auth.Interfaces;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace MicroStore.Web.Core.Base;

public class BaseService(IHttpClientFactory httpClientFactory, ITokenService tokenService) : IBaseService
{
    public async Task<ResponseDTO> SendAsync(RequestDTO requestDTO, bool withBearer = true)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(requestDTO.Url))
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "A URL da requisição não pode estar vazia."
                };
            }

            var client = httpClientFactory.CreateClient("MangoAPI");

            var message = new HttpRequestMessage
            {
                RequestUri = new Uri(requestDTO.Url),
                Method = requestDTO.ApiType switch
                {
                    ApiType.POST => HttpMethod.Post,
                    ApiType.PUT => HttpMethod.Put,
                    ApiType.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get
                }
            };

            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (withBearer)
            {
                var token = tokenService.GetToken();
                if (!string.IsNullOrWhiteSpace(token))
                {
                    message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }

            if (requestDTO.Data is not null)
            {
                var json = JsonConvert.SerializeObject(requestDTO.Data);
                message.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            var apiResponse = await client.SendAsync(message);
            var apiContent = await apiResponse.Content.ReadAsStringAsync();

            if (!apiResponse.IsSuccessStatusCode)
            {
                return CreateErrorResponse(apiResponse.StatusCode);
            }

            var response = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);

            return response ?? new ResponseDTO
            {
                IsSuccess = false,
                Message = "Erro ao desserializar resposta da API."
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO
            {
                IsSuccess = false,
                Message = $"Erro inesperado ao consumir API: {ex.Message}"
            };
        }
    }

    private static ResponseDTO CreateErrorResponse(HttpStatusCode status) =>
        status switch
        {
            HttpStatusCode.NotFound => new() { IsSuccess = false, Message = "Recurso não encontrado." },
            HttpStatusCode.Forbidden => new() { IsSuccess = false, Message = "Acesso negado." },
            HttpStatusCode.Unauthorized => new() { IsSuccess = false, Message = "Não autorizado." },
            HttpStatusCode.InternalServerError => new() { IsSuccess = false, Message = "Erro interno no servidor." },
            _ => new() { IsSuccess = false, Message = $"Erro {(int)status}: {status}" }
        };
}
