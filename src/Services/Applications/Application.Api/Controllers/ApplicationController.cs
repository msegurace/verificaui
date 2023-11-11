using Application.Service.Queries;
using Application.Service.Queries.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;
using User.Service.EventHandlers.Commands;

namespace Application.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("v1/app")]
    public class ApplicationController:ControllerBase
    {
        private readonly ILogger<ApplicationController> _logger;
        private readonly IApplicationQueryService _queryService;
        private IMediator _mediator;

        public ApplicationController(ILogger<ApplicationController> logger, IApplicationQueryService queryService, IMediator mediator)
        {
            _logger = logger;
            _queryService = queryService;
            _mediator = mediator;
        }

        [HttpGet("getall")]
        public async Task<DataCollection<AplicacionDto>> GetAll(int page = 1, int take = 10, string? ids = null)
        {
            IEnumerable<int> users = null;

            if (!String.IsNullOrEmpty(ids))
            {
                users = ids.Split(',').Select(x=> Convert.ToInt32(x));
            }
            return await _queryService.GetAllAsync(page, take, users);
        }

        [HttpGet("get/{id}")]
        public async Task<AplicacionDto> Get(int id)
        {
            return await _queryService.GetAsync(id);
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(ApplicationCreateCommand command)
        {
            try
            {
                await _mediator.Publish(command);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }


        }

        [HttpPost("modify")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Modify(ApplicationModifyCommand command)
        {
            try
            {
                await _mediator.Publish(command);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}


