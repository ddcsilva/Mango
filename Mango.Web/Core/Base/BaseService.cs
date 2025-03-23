using Mango.Web.Core.DTOs;
using Mango.Web.Core.Enums;
using Newtonsoft.Json;
using System.Text;

namespace Mango.Web.Core.Base;

public class BaseService(IHttpClientFactory httpClientFactory) : IBaseService
{
    public async Task<ResponseDTO> SendAsync(RequestDTO requestDTO)
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

            message.Headers.Accept.Add(new("application/json"));

            if (requestDTO.Data is not null)
            {
                message.Content = new StringContent(
                    JsonConvert.SerializeObject(requestDTO.Data),
                    Encoding.UTF8,
                    "application/json"
                );
            }

            var apiResponse = await client.SendAsync(message);
            var apiContent = await apiResponse.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);

            if (response is not null)
            {
                response.IsSuccess = apiResponse.IsSuccessStatusCode;
                return response;
            }

            return new ResponseDTO
            {
                IsSuccess = false,
                Message = $"Erro {(int)apiResponse.StatusCode}: {apiResponse.ReasonPhrase}"
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
}

