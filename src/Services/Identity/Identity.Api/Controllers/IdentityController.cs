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

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginInformation info)
        {
            if (info != null)
            {
                var user = await _userProxy.LoginAsync(info);
                if (user != null)
                {
                    var token = _jwtAuthenticationService.GenerateToken(user);
                    return Ok(token);
                }
                else
                {
                    return BadRequest("Access denied");
                }                
            }
            else
            {
                return BadRequest();
            }
        }
      
    }
}
