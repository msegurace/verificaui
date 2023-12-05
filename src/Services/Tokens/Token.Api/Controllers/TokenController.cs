using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Common.Collection;
using Service.Common.Mapping;
using Token.Persistence.Database;
using Token.Service.EventHandlers.Commands;
using Token.Service.Queries;
using Token.Service.Queries.DTOs;

namespace Users.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("v1/tokens")]
    public class TokenController:ControllerBase
    {
        private readonly ILogger<TokenController> _logger;
        private readonly ITokenQueryService _queryService;
        private IMediator _mediator;
        private ApplicationDbContext _context;

        public TokenController(ILogger<TokenController> logger, 
            ITokenQueryService queryService, 
            IMediator mediator,
            ApplicationDbContext context)
        {
            _logger = logger;
            _queryService = queryService;
            _mediator = mediator;
            _context = context;
        }

        [HttpGet("getall")]
        public async Task<DataCollection<Token2FADto>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            IEnumerable<int> tokens = null;

            if (!String.IsNullOrEmpty(ids))
            {
                tokens = ids.Split(',').Select(x=> Convert.ToInt32(x));
            }
            return await _queryService.GetAllAsync(page, take, tokens);
        }

        [HttpGet("get/{id}")]
        public async Task<Token2FADto> Get(int id)
        {
            return await _queryService.GetAsync(id);
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Token2FADto> Create(TokenCreateCommand command)
        {
            try
            {
                await _mediator.Publish(command);
                return await _queryService.GetNewAsync(command.idaplicacion, command.idusuario);
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        [HttpPost("accept")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AcceptToken(TokenAcceptCommand command)
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

        [HttpPost("reject")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RejectToken(TokenRejectCommand command)
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


