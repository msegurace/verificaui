
using AuthApi.Models;

namespace AuthApi.Auth
{
    public interface IJwtAuthenticationService
    {
        string Authenticate(AuthInfo authInfo);

        bool ValidateToken(string authToken);
    }
}
