using Identity.Service.Queries.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Identity.Api.Helpers
{
    public class JwtAuthenticationService : IJwtAuthenticationService
    {
        private readonly string _key;

        public JwtAuthenticationService(string key)
        {
            _key = key;

        }

        /// <summary>
        /// Autentica a un usuario contra el LDAP y devuelve un JWT si es satisfactorio.
        /// </summary>
        /// <param name="Info"></param>
        /// <param name="ldapService"></param>
        /// <returns></returns>
        public string GenerateToken(UsuarioDto usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.id.ToString()),
                    new Claim(ClaimTypes.GivenName, usuario.nombre),
                    new Claim(ClaimTypes.Surname, $"{usuario.apellido1} {usuario.apellido2}"),
                    new Claim(ClaimTypes.Email, usuario.email),
                    new Claim(ClaimTypes.Name, usuario.username),
                    new Claim(ClaimTypes.HomePhone, usuario.telefono)
                }),
                //Expires = DateTime.UtcNow.AddMinutes(5),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
        public bool ValidateToken(string authToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = GetValidationParameters();

                SecurityToken validatedToken;
                IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true, // Because there is expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = "Sample",
                ValidAudience = "Sample",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)) // The same key as the one that generate the token
            };
        }
    }

}
