using Microsoft.AspNetCore.Mvc;

namespace Users.Api.Controllers
{
    [ApiController]
    [Route("users/v1")]
    public class DefaultController:ControllerBase
    {
        private readonly ILogger<DefaultController> _logger;

        public DefaultController(ILogger<DefaultController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "Running...";
        }
    }
}
