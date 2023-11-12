using Identity.Service.Queries.DTOs;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Identity.Service.Proxies.User
{
    public interface IUserProxy
    {
        Task<UsuarioDto> LoginAsync(LoginInformation info);
        Task<string> AuthAsync(LoginInformation info);
    }
    public class UserProxy:IUserProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public UserProxy(IOptions<ApiUrls> apiUrls, HttpClient httpClient)
        {
            _apiUrls = apiUrls.Value;
            httpClient.BaseAddress = new Uri(_apiUrls.UserUrl!);
            _httpClient = httpClient;

        }

        public async Task<string> AuthAsync(LoginInformation info)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(info),
                Encoding.UTF8,
                new MediaTypeHeaderValue("application/json")
            );

            var request = await _httpClient.PostAsync("/v1/users/login", content);
            request.EnsureSuccessStatusCode();
            return await request.Content.ReadAsStringAsync();
        }

        public async Task<UsuarioDto> LoginAsync(LoginInformation info)
        {
            UsuarioDto? user = null;
            var content = new StringContent(
                JsonSerializer.Serialize(info),
                Encoding.UTF8,
                new MediaTypeHeaderValue("application/json")
            );

            var request = await _httpClient.PostAsync("/v1/users/login", content);
            if (request.IsSuccessStatusCode)
            {
                var resp = await request.Content.ReadAsStringAsync();
                user = JsonSerializer.Deserialize<UsuarioDto>(resp);
            }
            return user!;
        }
    }
}
