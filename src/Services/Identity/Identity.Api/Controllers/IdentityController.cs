using Identity.Api.Helpers;
using Identity.Service.Proxies.User;
using Identity.Service.Queries;
using Identity.Service.Queries.DTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;

namespace Identity.Api.Controllers
{
    [ApiController]
    [Route("v1/identity")]
    public class IdentityController:ControllerBase
    {
        private readonly ILogger<DefaultController> _logger;
        private readonly IIdentityQueryService _queryService;
        private readonly IUserProxy _userProxy;
        private readonly IJwtAuthenticationService _jwtAuthenticationService;

        public IdentityController(ILogger<DefaultController> logger, IIdentityQueryService queryService, IUserProxy userProxy, IJwtAuthenticationService jwtAuthenticationService)
        {
            _logger = logger;
            _queryService = queryService;
            _userProxy = userProxy;
            _jwtAuthenticationService = jwtAuthenticationService;
        }

        [HttpPost("auth")]
        public async Task<string> AuthAsync([FromBody] LoginInformation info)
        {
            return await _userProxy.AuthAsync(info);
        }

        [HttpPost("login")]
        public async Task<UsuarioDto> LoginAsync([FromBody] LoginInformation info)
        {
            return await _userProxy.LoginAsync(info);
        }
    }
}
