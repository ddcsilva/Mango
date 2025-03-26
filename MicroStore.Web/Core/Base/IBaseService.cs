using MicroStore.Web.Core.DTOs;

namespace MicroStore.Web.Core.Base;

public interface IBaseService
{
    Task<ResponseDTO> SendAsync(RequestDTO request, bool withBearer = true);
}
