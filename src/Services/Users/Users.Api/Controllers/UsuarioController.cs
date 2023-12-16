using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;
using System.Security.Claims;
using User.Service.EventHandlers.Commands;
using User.Service.Queries.DTOs;
using Users.Service.Queries;
using Users.Service.Queries.DTOs;

namespace Users.Api.Controllers
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("v1/users")]
    public class UsuarioController:ControllerBase
    {
        private readonly ILogger<DefaultController> _logger;
        private readonly IUsuarioQueryService _queryService;
        private IMediator _mediator;

        public UsuarioController(ILogger<DefaultController> logger, IUsuarioQueryService queryService, IMediator mediator)
        {
            _logger = logger;
            _queryService = queryService;
            _mediator = mediator;
        }

        [HttpGet("getall")]
        public async Task<DataCollection<UsuarioDto>> GetAll(int page = 1, int take = 10, string? ids = null)
        {
            var id = User.Claims.Single(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            IEnumerable<int> users = null;
            if (!String.IsNullOrEmpty(ids))
            {
                users = ids.Split(',').Select(x=> Convert.ToInt32(x));
            }
            return await _queryService.GetAllAsync(page, take, users);
        }

        [HttpGet("get/{id}")]
        public async Task<UsuarioDto> Get(int id)
        {
            return await _queryService.GetAsync(id);
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(UserCreateCommand command)
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
        public async Task<IActionResult> Modify(UserModifyCommand command)
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

        [HttpPost("register")]
        [ProducesResponseType(typeof(VerificaGenericResponse), StatusCodes.Status200OK)]
        public async Task<VerificaGenericResponse> RegisterAsync(VerificaAppUserDto user)
        {
            try
            {
                return await _queryService.RegisterAsync(user);
            }
            catch (Exception ex)
            {
                return new VerificaGenericResponse()
                {
                    code = "NOK",
                    content = ex.Message
                };
            }
        }

        [HttpPost("endregister")]
        [ProducesResponseType(typeof(VerificaGenericResponse), StatusCodes.Status200OK)]
        public async Task<VerificaGenericResponse> EndRegisterAsync(VerificaAppUserDto user)
        {
            try
            {
                return await _queryService.EndRegistrationAsync(user);
            }
            catch (Exception ex)
            {
                return new VerificaGenericResponse()
                {
                    code = "NOK",
                    content = ex.Message
                };
            }
        }
        [HttpPost("validate")]
        [ProducesResponseType(typeof(VerificaGenericResponse), StatusCodes.Status200OK)]
        public async Task<VerificaGenericResponse> ValidateAsync(VerificaAppUserDto user)
        {
            try
            {
                return await _queryService.ValidateAsync(user);
            }
            catch (Exception ex)
            {
                return new VerificaGenericResponse()
                {
                    code = "NOK",
                    content = ex.Message
                };
            }
        }
    }

}


