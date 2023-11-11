using Microsoft.AspNetCore.Mvc;
using Users.Service.Queries;
using Users.Service.Queries.DTOs;
using System.Security.Claims;

namespace Users.Api.Controllers
{
    [ApiController]
    [Route("v1/users")]
    public class LoginController:ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUsuarioQueryService _queryService;

        public LoginController(ILogger<LoginController> logger, IUsuarioQueryService queryService)
        {
            _logger = logger;
            _queryService = queryService;
        }


        [HttpPost("login")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<UsuarioDto> Login([FromBody] LoginInformation info)
        {
            try
            {
               //var id = User.Claims.Single(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
                return await _queryService.LoginAsync(info);

            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
