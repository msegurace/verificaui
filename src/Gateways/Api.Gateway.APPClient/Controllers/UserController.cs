using Api.Gateways.Models.DTOs;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.APPClient.Controllers
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
               
        [HttpGet("get/{id}")]
        public async Task<UsuarioDto> Get(int id)
        {
            return await _userProxy!.GetAsync(id);
        }

        [HttpPost("register")]
        public async Task<VerificaGenericResponse> RegisterAsync(VerificaAppUserDto user)
        {
            return await _userProxy!.RegisterAsync(user);
        }

        [HttpPost("endregister")]
        public async Task<VerificaGenericResponse> EndRegisterAsync(VerificaAppUserDto user)
        {
            return await _userProxy!.EndRegisterAsync(user);
        }

        [HttpPost("validate")]
        public async Task<VerificaGenericResponse> ValidateAsync(VerificaAppUserDto user)
        {
            return await _userProxy!.ValidateAsync(user);
        }
    }
}
