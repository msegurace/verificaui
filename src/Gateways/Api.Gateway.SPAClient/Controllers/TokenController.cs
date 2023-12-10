using Api.Gateways.Models;
using Api.Gateways.Models.Commands;
using Api.Gateways.Models.DTOs;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.SPAClient.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("tokens")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenProxy _tokenProxy;
        private readonly IApplicationProxy _appProxy;
        private readonly IUserProxy _userProxy;

        public TokenController(ITokenProxy tokenProxy, IApplicationProxy applicationProxy, IUserProxy userProxy)
        {
            _tokenProxy = tokenProxy;
            _appProxy = applicationProxy;
            _userProxy = userProxy;
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

        /// <summary>
        /// En este método se evaluará el riesgo que supone para la organización
        /// este inicio de sesión. Se valorará lo siguiente:
        /// 1) Que el usuario sea administrador, si es así se solicitará 2FA.
        /// 2) Que el horario sea el normal de la organización, en este ejemplo ponemos que se trabaja de 7 a 17. Si no lo es +10.
        /// 3) Que el origen de la petición sea interna o externa, esto realmente se debería hacer con la IP del solicitante. Si es externa +10.
        /// 4) La categorización del ENS (Baja: 0, Media +10, Alta +20)
        /// 
        /// Si la suma total de la evaluación es >= 20, se mostrará el 2FA.
        /// </summary>
        /// <param name="info">Usuario y aplicación de origen</param>
        /// <returns></returns>
        [HttpPost("evaluaterisk")]
        public async Task<EvaluateRiskResult> EvaluateRiskAsync(EvaluateRiskInformation info)
        {

            var result = new EvaluateRiskResult();

            try
            {
                if (info != null)
                {
                    //Administrador
                    var user = await _userProxy.GetAsync(info.idUsuario);
                    if (user != null)
                    {
                        if (user.admin)
                        {
                            result.admin = 20;
                        }
                    }
                    else
                    {
                        result.error = true;
                    }
                    //Horario
                    if (DateTime.Now.Hour < 7 || DateTime.Now.Hour > 16)
                    {
                        result.time = 10;
                    }
                    var app = await _appProxy.GetAsync(info.idAplicacion);
                    if (app != null)
                    {
                        //ORIGEN
                        if (app.origen.Equals("EXTERNA"))
                        {
                            result.origin = 10;
                        }
                        //ENS
                        if (app.clasificacion_ens.Equals("ALTA"))
                        {
                            result.classification = 20;
                        }
                        else if (app.clasificacion_ens.Equals("MEDIA"))
                        {
                            result.classification = 10;
                        }
                    }
                    else
                    {
                        result.error = true;
                    }
                }
            }
            catch (Exception)
            {
                result.error = true;
            }
            return result;
        }
    }
}
