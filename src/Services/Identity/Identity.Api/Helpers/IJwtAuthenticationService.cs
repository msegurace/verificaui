using Identity.Service.Queries.DTOs;

namespace Identity.Api.Helpers
{
    public interface IJwtAuthenticationService
    {
        string GenerateToken(UsuarioDto usuario);

        bool ValidateToken(string authToken);
    }
}
