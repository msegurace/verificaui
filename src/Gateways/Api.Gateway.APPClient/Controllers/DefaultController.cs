using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.APPClient.Controllers
{
    [ApiController]
    [Route("/")]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public string Index()
        {
            return "Running....";
        }
    }
}
