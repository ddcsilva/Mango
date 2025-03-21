using Mango.Web.Core.DTOs;

namespace Mango.Web.Core.Base;

public interface IBaseService
{
    Task<ResponseDTO> SendAsync(RequestDTO request);
}
