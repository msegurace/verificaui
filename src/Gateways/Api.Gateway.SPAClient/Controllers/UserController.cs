using Api.Gateways.Models;
using Api.Gateways.Models.Commands;
using Api.Gateways.Models.DTOs;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;

namespace Api.Gateway.SPAClient.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserProxy? _userProxy;

        public UserController(IUserProxy? userProxy)
        {
            _userProxy = userProxy;
        }

        [HttpGet("getall")]
        public async Task<DataCollection<UsuarioDto>> GetAll(int page, int take)
        {
            return await _userProxy!.GetAllAsync(page, take);
        }

        [HttpGet("get/{id}")]
        public async Task<UsuarioDto> Get(int id)
        {
            return await _userProxy!.GetAsync(id);
        }

        [HttpPost("add")]
        public async Task<bool> CreateAsync(UserCreateCommand command)
        {
            return await _userProxy!.CreateAsync(command);

        }

        [HttpPost("modify/{id}")]
        public async Task<bool> ModifyAsync(UserModifyCommand command)
        {
            return await _userProxy!.ModifyAsync(command);
        }
    }
}
