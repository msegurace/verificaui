using Api.Gateways.Models;
using Api.Gateways.Models.Commands;
using Api.Gateways.Models.DTOs;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.SPAClient.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("apps")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationProxy? _ApplicationProxy;

        public ApplicationController(IApplicationProxy? ApplicationProxy)
        {
            _ApplicationProxy = ApplicationProxy;
        }

        [HttpGet("getall")]
        public async Task<DataCollection<AplicacionDto>> GetAll(int page, int take)
        {
            return await _ApplicationProxy!.GetAllAsync(page, take);
        }

        [HttpGet("get/{id}")]
        public async Task<AplicacionDto> Get(int id)
        {
            return await _ApplicationProxy!.GetAsync(id);
        }

        [HttpPost("add")]
        public async Task<bool> CreateAsync(ApplicationCreateCommand command)
        {
            return await _ApplicationProxy!.CreateAsync(command);

        }

        [HttpPost("modify")]
        public async Task<bool> ModifyAsync(ApplicationModifyCommand command)
        {
            return await _ApplicationProxy!.ModifyAsync(command);
        }
    }
}
