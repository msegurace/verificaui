using Microsoft.AspNetCore.Http;

namespace Api.Gateways.Proxies.Config
{
    /// <summary>
    /// Lee el token de las cabeceras Http y para reenviarlo a los microservicios
    /// </summary>
    public static class HttpClientTokenExtension
    {
        public static void AddBearerToken(this HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext.User.Identity!.IsAuthenticated && httpContextAccessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                if (httpContextAccessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    var token = httpContextAccessor?.HttpContext?.Request.Headers["Authorization"].ToString();
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
                    }
                }                
            }
        }
    }
}
