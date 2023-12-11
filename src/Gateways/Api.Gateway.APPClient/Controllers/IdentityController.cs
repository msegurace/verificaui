
using Api.Gateways.Models.DTOs;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.APPClient.Controllers
{
    [ApiController]
    [Route("/")]
    public class IdentityController : ControllerBase
    {
        private readonly ILogger<IdentityController> _logger;
        private readonly IIdentityProxy _identityProxy;

        public IdentityController(ILogger<IdentityController> logger, IIdentityProxy identityProxy)
        {
            _logger = logger;
            _identityProxy = identityProxy;
        }

        [HttpPost("login")]
        public async Task<UsuarioDto> LoginAsync([FromBody] LoginInformation info)
        {
            return await _identityProxy.LoginAsync(info);
        }

        [HttpPost("auth")]
        public async Task<string> AuthAsync([FromBody] LoginInformation info)
        {
            return await _identityProxy.AuthAsync(info);

        }
    }
}
