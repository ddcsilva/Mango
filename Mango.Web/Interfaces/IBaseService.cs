using Mango.Web.DTOs;

namespace Mango.Web.Interfaces;

public interface IBaseService
{
    Task<ResponseDTO?> SendAsync(RequestDTO request);
}
