using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthApi.Auth;
using AuthApi.Models;

namespace AuthApi.Controllers
{
    /// <summary>
    /// Controlador por defecto de la API.
    /// Contendrá los métodos generales de la API.
    /// </summary>    
    [Authorize]
    [Route("api/v1")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "General")]
    public class DefaultController : ControllerBase
    {
        private readonly ILogger<DefaultController> _logger;
        private readonly IJwtAuthenticationService _authService;

        /// <summary>
        /// Constructor del Controlador principal
        /// </summary>
        /// <param name="logger">Inyección de la dependencia para la interfaz para crear logs.</param>
        /// <param name=""></param>
        /// 
        public DefaultController(ILogger<DefaultController> logger,
            IJwtAuthenticationService authService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _authService = authService;
        }

        /// <summary>
        /// Devuelve el estado del controlador.
        /// </summary>
        /// <returns>Retorna un Json indicando que el controlador está corriendo.</returns>
        [AllowAnonymous]
        [HttpGet]
        public object Get()
        {
            var responseObject = new { Status = "Running" };

            return responseObject;
        }

        private string GetFromResponse(string origin, string sub)
        {
            var resp = string.Empty;
            var a = origin.IndexOf(sub, 0);
            if (a > 0)
            {
                var b = origin.IndexOf(":", a);
                var c = origin.IndexOf("\"", b + 3);
                var c1 = origin.IndexOf(",", b + 2);
                resp = origin.Substring(c1 < c ? b + 2 : b + 3, c1 < c ? c1 - (b + 2) : c - (b + 3));
            }
            else
            {
                throw new Exception("Operación inválida al crear el token");
            }
            return resp;
        }


        /// <summary>
        /// Autentica el servicio con un usuario y contraseña devolviendo un JWT válido. La autenticación se hará contra el LDAP del paciente.
        /// </summary>
        /// <param name="user">El usuario y la contraseña a autenticar</param>
        /// <returns>JWT válido para que la aplicación cliente lo incluya en sus cabeceras o un 401 - No autorizado</returns>
        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] AuthInfo user)
        {
            string token = "";
            if (!string.IsNullOrEmpty(user.auth_token))
            {
                if (_authService.ValidateToken(user.auth_token))
                {
                    return Ok(user.auth_token);
                }
            }
            token = _authService.Authenticate(user);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

    }
}
