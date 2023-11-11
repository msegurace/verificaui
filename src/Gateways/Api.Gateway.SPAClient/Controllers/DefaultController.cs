using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.SPAClient.Controllers
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
