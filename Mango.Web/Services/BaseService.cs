using Mango.Web.DTOs;
using Mango.Web.Enums;
using Mango.Web.Interfaces;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Mango.Web.Services;

public class BaseService : IBaseService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public BaseService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ResponseDTO?> SendAsync(RequestDTO requestDTO)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("MangoAPI");
            var message = new HttpRequestMessage();

            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            message.RequestUri = new Uri(requestDTO.Url);

            if (requestDTO.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json");
            }

            switch (requestDTO.ApiType)
            {
                case ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                case ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }

            var apiResponse = await client.SendAsync(message);

            switch (apiResponse.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new ResponseDTO { IsSuccess = false, Message = "Not Found" };
                case HttpStatusCode.Forbidden:
                    return new ResponseDTO { IsSuccess = false, Message = "Access Denied" };
                case HttpStatusCode.Unauthorized:
                    return new ResponseDTO { IsSuccess = false, Message = "Unauthorized" };
                case HttpStatusCode.InternalServerError:
                    return new ResponseDTO { IsSuccess = false, Message = "Internal Server Error" };
                default:
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
                    var responseDto = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);
                    return responseDto;
            }
        }
        catch (Exception ex)
        {
            return new ResponseDTO { IsSuccess = false, Message = ex.Message };
        }
    }
}
