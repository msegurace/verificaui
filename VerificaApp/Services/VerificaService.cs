
using VerificaApp.Models;
using VerificaApp.Models;
using System.Text;

namespace VerificaApp.Services
{
    public class VerificaAppService : IVerificaAppService
    {
        HttpClient httpClient;

        public VerificaAppService()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri(CommonConstants.BASE_URL);
        }

        /// <summary>
        /// Petición genérica para 
        /// </summary>
        /// <param name="user">Clase del usuario a registrar</param>
        /// <returns></returns>
        public async Task<VerificaAppGenericResponse> GenericRequest(VerificaAppUser user, string endpoint)
        {
            VerificaAppGenericResponse response = new VerificaAppGenericResponse();
            if (CheckNetwork())
            {
                string strPayload = JsonSerializer.Serialize(user);
                HttpContent httpContent = new StringContent(strPayload, Encoding.UTF8, "application/json");
                HttpResponseMessage responseFromSSO = await httpClient.PostAsync(this.httpClient.BaseAddress + endpoint, httpContent);

                string content = await responseFromSSO.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize(content, VerificaAppGenericResponseContext.Default.VerificaAppGenericResponse);
            }
            else
            {
                response.code = "NOK";
                response.content = "No se pudo conectar a la red.";
            }
            return response;
        }

        /// <summary>
        /// Registra a un usuario en el sistema
        /// </summary>
        /// <param name="user">Clase del usuario a registrar</param>
        /// <returns></returns>
        public async Task<VerificaAppGenericResponse> RegisterUser(VerificaAppUser user)
        {
            VerificaAppGenericResponse response = new VerificaAppGenericResponse();
            if (CheckNetwork())
            {
                string strPayload = JsonSerializer.Serialize(user);
                HttpContent httpContent = new StringContent(strPayload, Encoding.UTF8, "application/json");
                HttpResponseMessage responseFromSSO = await httpClient.PostAsync(this.httpClient.BaseAddress + CommonConstants.REGISTER_USER_URL, httpContent);
                
                string content = await responseFromSSO.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize(content, VerificaAppGenericResponseContext.Default.VerificaAppGenericResponse);
            }
            else
            {
                response.code = "NOK";
                response.content = "No se pudo conectar a la red.";
            }
            return response;
        }

        /// <summary>
        /// Envía la OTP suministrada por el usuario para validarla en el servidor y terminar
        /// el registro del usuario en el sistema con su GUID
        /// </summary>
        /// <param name="user">Clase del usuario a registrar</param>
        /// <returns>Respuesta genérica</returns>
        public async Task<VerificaAppGenericResponse> EndRegisterUser(VerificaAppUser user)
        {
            VerificaAppGenericResponse response = new VerificaAppGenericResponse();
            if (CheckNetwork())
            {
                string strPayload = JsonSerializer.Serialize(user);
                HttpContent httpContent = new StringContent(strPayload, Encoding.UTF8, "application/json");
                HttpResponseMessage responseFromSSO = await httpClient.PostAsync(this.httpClient.BaseAddress + CommonConstants.END_REGISTER_USER_URL, httpContent);
                string content = await responseFromSSO.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize(content, VerificaAppGenericResponseContext.Default.VerificaAppGenericResponse);
            }
            else
            {
                response.code = "NOK";
                response.content = "No se pudo conectar a la red.";
            }
            return response;
        }


        /// <summary>
        /// Valida que el usuario/contraseña/teléfono y guid sean correctos en GdI
        /// </summary>
        /// <param name="user">Objeto del usuario a validar</param>
        /// <returns>Ok / Nok</returns>
        public async Task<VerificaAppGenericResponse> ValidateUser(VerificaAppUser user)
        {

            VerificaAppGenericResponse response = new VerificaAppGenericResponse();
            if (CheckNetwork())
            {
                string strPayload = JsonSerializer.Serialize(user);
                HttpContent httpContent = new StringContent(strPayload, Encoding.UTF8, "application/json");
                HttpResponseMessage responseFromSSO = await httpClient.PostAsync(this.httpClient.BaseAddress + CommonConstants.VALIDATE_USER_URL, httpContent);
                string content = await responseFromSSO.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize(content, VerificaAppGenericResponseContext.Default.VerificaAppGenericResponse);
            }
            else
            {
                response.code = "NOK";
                response.content = "No se pudo conectar a la red.";
            }
            return response;

        }

        /// <summary>
        /// Obtiene los tokens activos del usuario conectado.
        /// 
        /// </summary>
        /// <param name="user">usuario conectado</param>
        /// <returns></returns>
        public async Task<VerificaAppGenericResponse> GetTokens(VerificaAppUser user)
        {
            VerificaAppGenericResponse response = new VerificaAppGenericResponse();
            List<AuthRequest> auths = new List<AuthRequest>();
            if (CheckNetwork())
            {
                string strPayload = JsonSerializer.Serialize(user);
                HttpContent httpContent = new StringContent(strPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage responseFromSSO = await httpClient.PostAsync(this.httpClient.BaseAddress + CommonConstants.GET_TOKENS_URL, httpContent);
                string content = await responseFromSSO.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize(content, VerificaAppGenericResponseContext.Default.VerificaAppGenericResponse);
            }
            else
            {
                response.code = "NOK";
                response.content = "No se pudo conectar a la red.";
            }
            return response;
        }

        /// <summary>
        /// Envía una solicitud para aceptar un token correspondiente al usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> AcceptToken(VerificaAppUser user)
        {
            if (CheckNetwork())
            {
                string strPayload = JsonSerializer.Serialize(user);
                HttpContent httpContent = new StringContent(strPayload, Encoding.UTF8, "application/json");

                this.httpClient.BaseAddress = new Uri(CommonConstants.BASE_URL);
                HttpResponseMessage response = await httpClient.PostAsync(this.httpClient.BaseAddress + CommonConstants.ACCEPT_TOKEN_URL, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    return content;
                }
            }
            return null;
        }


        /// <summary>
        /// Envía una solicitud para rechazar un token correspondiente al usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> RejectToken(VerificaAppUser user)
        {

            if (CheckNetwork())
            {
                string strPayload = JsonSerializer.Serialize(user);
                HttpContent httpContent = new StringContent(strPayload, Encoding.UTF8, "application/json");

                this.httpClient.BaseAddress = new Uri(CommonConstants.BASE_URL);
                HttpResponseMessage response = await httpClient.PostAsync(this.httpClient.BaseAddress + CommonConstants.REJECT_TOKEN_URL, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    return content;
                }
            }
            return null;
        }

       
        private bool CheckNetwork()
        {
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            return (accessType == NetworkAccess.Internet);
        }

        
    }
}
