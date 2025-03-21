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
                return new ResponseDTO{ Result = null, IsSuccess = false, Message = "A URL da requisição não pode estar vazia." };
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

            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            if (requestDTO.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json");
            }

            var apiResponse = await client.SendAsync(message);

            if (!apiResponse.IsSuccessStatusCode)
            {
                return new ResponseDTO { Result = null, IsSuccess = false, Message = $"Erro: {apiResponse.StatusCode}" };
            }

            var apiContent = await apiResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseDTO>(apiContent) ?? new ResponseDTO { IsSuccess = false, Message = "Erro ao desserializar resposta da API." };
        }
        catch (Exception ex)
        {
            return new ResponseDTO { Result = null, IsSuccess = false, Message = $"Erro ao consumir API: {ex.Message}" };
        }
    }
}
