using System.Text;

namespace VerificaApp.Services
{
    public class VerificaAppService : IVerificaAppService
    {
        HttpClient httpClient;
        bool authCalled = false;

        public VerificaAppService()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri(CommonConstants.BASE_URL);
            //httpClient.DefaultRequestHeaders
            //.Accept
            //.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

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
                await this.Auth();
                var strPayload = JsonSerializer.Serialize(user);
                var httpContent = new StringContent(strPayload, Encoding.UTF8, "application/json");
                var resp = await httpClient.PostAsync(this.httpClient.BaseAddress + endpoint, httpContent);
                resp.EnsureSuccessStatusCode();

                string content = await resp.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize(content, VerificaAppGenericResponseContext.Default.VerificaAppGenericResponse);
            }
            else
            {
                response.code = "NOK";
                response.content = "No se pudo conectar a la red.";
            }
            return response;
        }

        private async Task Auth()
        {
            if (authCalled) return;

            AuthInfo info = new AuthInfo()
            {
                username = "msegced",
                password = "msegced"
            };
            var strPayload = JsonSerializer.Serialize(info);
            HttpContent httpContent = new StringContent(strPayload, Encoding.UTF8, "application/json");
            HttpResponseMessage resp = await httpClient.PostAsync(this.httpClient.BaseAddress + CommonConstants.AUTH_USER, httpContent);
            string token = await resp.Content.ReadAsStringAsync();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token);
            resp.EnsureSuccessStatusCode();
            authCalled = true;
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
                await this.Auth();
                var strPayload = JsonSerializer.Serialize(user);
                var httpContent = new StringContent(strPayload, Encoding.UTF8, "application/json");
                var resp = await httpClient.PostAsync(this.httpClient.BaseAddress + CommonConstants.REGISTER_USER_URL, httpContent);
                resp.EnsureSuccessStatusCode();

                string content = await resp.Content.ReadAsStringAsync();
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
                //await this.Auth();
                var strPayload = JsonSerializer.Serialize(user);
                var httpContent = new StringContent(strPayload, Encoding.UTF8, "application/json");
                var resp = await httpClient.PostAsync(this.httpClient.BaseAddress + CommonConstants.END_REGISTER_USER_URL, httpContent);
                //resp.EnsureSuccessStatusCode();

                string content = await resp.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize(content, VerificaAppGenericResponseContext.Default.VerificaAppGenericResponse);
                
                //content = "{\"code\":\"OK\",\"content\":{\"uid\":\"mseggon\",\"phone\":null,\"password\":null,\"registering\":null,\"token\":null,\"guid\":\"3cfe59d0-2362-4670-81f5-67d78112d24c\",\"firebase\":null,\"otp\":\"\"}}";
                //response = JsonSerializer.Deserialize(content, VerificaAppGenericResponseContext.Default.VerificaAppGenericResponse);
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
                await this.Auth();
                var strPayload = JsonSerializer.Serialize(user);
                var httpContent = new StringContent(strPayload, Encoding.UTF8, "application/json");
                var resp = await httpClient.PostAsync(this.httpClient.BaseAddress + CommonConstants.REGISTER_USER_URL, httpContent);
                resp.EnsureSuccessStatusCode();

                string content = await resp.Content.ReadAsStringAsync();
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
        /// <returns></returns>
        public async Task<VerificaAppGenericResponse> GetTokens(int idUser)
        {
            VerificaAppGenericResponse response = new VerificaAppGenericResponse();
            List<AuthRequest> auths = new List<AuthRequest>();
            if (CheckNetwork())
            {
                await Auth();

                HttpResponseMessage resp = await httpClient.GetAsync(this.httpClient.BaseAddress + CommonConstants.GET_TOKENS_URL + $"?iduser={idUser}&page=1&take=20");
                resp.EnsureSuccessStatusCode();

                string content = await resp.Content.ReadAsStringAsync();
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
        /// <returns></returns>
        public async Task<bool> AcceptToken(int id)
        {
            if (CheckNetwork())
            {
                string strPayload = JsonSerializer.Serialize("{ \"id\":" + id + "}");
                HttpContent httpContent = new StringContent(strPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(this.httpClient.BaseAddress + CommonConstants.ACCEPT_TOKEN_URL, httpContent);

                return response.IsSuccessStatusCode;
            }
            return false;
        }


        /// <summary>
        /// Envía una solicitud para aceptar un token correspondiente al usuario
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RejectToken(int id)
        {
            if (CheckNetwork())
            {
                string strPayload = JsonSerializer.Serialize("{ \"id\":" + id + "}");
                HttpContent httpContent = new StringContent(strPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(this.httpClient.BaseAddress + CommonConstants.REJECT_TOKEN_URL, httpContent);

                return response.IsSuccessStatusCode;
            }
            return false;
        }


        private bool CheckNetwork()
        {
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            return (accessType == NetworkAccess.Internet);
        }

        
    }
}
