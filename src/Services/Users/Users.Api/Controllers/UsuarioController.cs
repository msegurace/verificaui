﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;
using User.Service.EventHandlers.Commands;
using Users.Service.Queries;
using Users.Service.Queries.DTOs;

namespace Users.Api.Controllers
{
    [ApiController]
    [Route("users/v1")]
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
        public async Task<DataCollection<UsuarioDto>> GetAll(int page = 1, int take = 10, string ids = null)
        {
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

        [HttpPost("modify/{id}")]
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
    }
}


