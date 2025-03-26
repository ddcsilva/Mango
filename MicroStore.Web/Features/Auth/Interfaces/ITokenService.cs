namespace MicroStore.Web.Features.Auth.Interfaces;

public interface ITokenService
{
    void SetToken(string token);
    string? GetToken();
    void ClearToken();
}
