using Api.Gateways.Models;
using Api.Gateways.Models.Commands;
using Api.Gateways.Models.DTOs;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Api.Gateway.APPClient.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("tokens")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenProxy _tokenProxy;
        private readonly IUserProxy _userProxy;
        private readonly IApplicationProxy _applicationProxy;

        public TokenController(ITokenProxy tokenProxy, IUserProxy userProxy, IApplicationProxy applicationProxy)
        {
            _tokenProxy = tokenProxy;
            _userProxy = userProxy;
            _applicationProxy = applicationProxy;
         
        }

        [HttpGet("getall")]
        public async Task<DataCollection<Token2FADto>> GetAll(int page, int take)
        {
            return await _tokenProxy!.GetAllAsync(page, take);
        }

        [HttpGet("get/{id}")]
        public async Task<Token2FADto> Get(int id)
        {
            return await _tokenProxy!.GetAsync(id);
        }

        [HttpPost("add")]
        public async Task<Token2FADto> CreateAsync(TokenCreateCommand command)
        {
            return await _tokenProxy!.CreateAsync(command);

        }

        [HttpPost("modify/{id}")]
        public async Task<bool> ModifyAsync(TokenModifyCommand command)
        {
            return await _tokenProxy!.ModifyAsync(command);
        }

        [HttpPost("accept")]
        public async Task<bool> AcceptAsync(TokenAcceptCommand command)
        {
            return await _tokenProxy!.AcceptAsync(command);

        }

        [HttpGet("getTokenStatus/{id}")]
        public async Task<string> TokenStatusAync(int id)
        {
            var token = await _tokenProxy!.GetAsync(id);
            if (token.creado < DateTime.Now.AddMinutes(-5))
            {
                return "E"; //EXPIRADO
            }
            if ((bool)token.rechazado!)
            {
                return "R"; //RECHAZADO
            }
            if ((bool)token.aceptado!)
            {
                return "A"; //ACEPTADO
            }
            return "V"; //TODAVÍA EN VIGOR
            
        }

        [HttpPost("reject")]
        public async Task<bool> RejectAsync(TokenRejectCommand command)
        {
            return await _tokenProxy!.RejectAsync(command);
        }

        [HttpGet("getAllForApp")]
        public async Task<List<AuthItemDto>> GetAllForAppAsync(int idUser, int page, int take)
        {
            var items = new List<AuthItemDto>();
            var tokens = await _tokenProxy!.GetAllForUserAsync(idUser, page, take);
            if (tokens.HasItems)
            {
                foreach(var token in tokens.Items)
                {
                    var usuario = await _userProxy.GetAsync(token.idusuario);
                    var aplicacion = await _applicationProxy.GetAsync(token.idaplicacion);
                    items.Add(new AuthItemDto()
                    {
                        id = token.id,
                        usuario = usuario.username,
                        aplicacion = aplicacion.descripcion,
                        token = token.token,
                        creado = token.creado!.Value,
                        expira = token.creado!.Value.AddMinutes(5),
                        aceptado = token.aceptado!.Value,
                        rechazado = token.rechazado!.Value
                    });
                }
            }
            return items;
        }

    }
}
