using Identity.Service.Queries;
using Identity.Service.Queries.DTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;

namespace Identity.Api.Controllers
{
    [ApiController]
    [Route("v1/users")]
    public class UsuarioController:ControllerBase
    {
        private readonly ILogger<DefaultController> _logger;
        private readonly IUsuarioQueryService _queryService;

        public UsuarioController(ILogger<DefaultController> logger, IUsuarioQueryService queryService)
        {
            _logger = logger;
            _queryService = queryService;
        }

        [HttpGet]
        public async Task<DataCollection<UsuarioDto>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            IEnumerable<int> users = null;

            if (!String.IsNullOrEmpty(ids))
            {
                users = ids.Split(',').Select(x=> Convert.ToInt32(x));
            }
            return await _queryService.GetAllAsync(page, take, users);
        }

        [HttpGet("{id}")]
        public async Task<UsuarioDto> Get(int id)
        {
            return await _queryService.GetAsync(id);
        }
    }
}
