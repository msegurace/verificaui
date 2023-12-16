using Api.Gateways.Proxies.Config;
using Api.Gateways.Models;
using Api.Gateways.Models.Commands;
using Api.Gateways.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
//
namespace Api.Gateways.Proxies
{
    public interface IUserProxy
    {
        Task<DataCollection<UsuarioDto>> GetAllAsync(int page = 1, int take = 10, string ids = null);
        Task<UsuarioDto> GetAsync(int id);
        Task<bool> CreateAsync(UserCreateCommand command);
        Task<bool> ModifyAsync(UserModifyCommand command);
        Task<VerificaGenericResponse> RegisterAsync(VerificaAppUserDto user);
        Task<VerificaGenericResponse> EndRegisterAsync(VerificaAppUserDto user);
        Task<VerificaGenericResponse> ValidateAsync(VerificaAppUserDto user);
    }
    public class UserProxy : IUserProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public UserProxy(IOptions<ApiUrls> apiUrls,
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);
            _apiUrls = apiUrls.Value;
            httpClient.BaseAddress = new Uri(_apiUrls.UserUrl!);
            _httpClient = httpClient;

        }

        public async Task<DataCollection<UsuarioDto>> GetAllAsync(int page = 1, int take = 10, string? userIds = null)
        {
           // string ids = string.Join(",", (userIds) ?? new List<int>());

            var request = await _httpClient.GetAsync($"v1/users/getall?page={page}&take={take}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<UsuarioDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
             )!;
        }

        [HttpGet("get/{id}")]
        public async Task<UsuarioDto> GetAsync(int id)
        {
            var request = await _httpClient.GetAsync($"v1/users/get/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<UsuarioDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
             )!;
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<bool> CreateAsync(UserCreateCommand command)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(command),
                Encoding.UTF8,
                new MediaTypeHeaderValue("application/json")
            );

            var request = await _httpClient.PostAsync(_apiUrls.UserUrl + "v1/users/add", content);
            request.EnsureSuccessStatusCode();
            return true;

        }

        [HttpPost("modify")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<bool> ModifyAsync(UserModifyCommand command)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(command),
                Encoding.UTF8,
                new MediaTypeHeaderValue("application/json")
            );

            var request = await _httpClient.PostAsync(_apiUrls.UserUrl + "v1/users/modify", content);
            request.EnsureSuccessStatusCode();
            return true;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(VerificaGenericResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<VerificaGenericResponse> RegisterAsync(VerificaAppUserDto user)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(user),
                Encoding.UTF8,
                new MediaTypeHeaderValue("application/json")
            );

            var request = await _httpClient.PostAsync(_apiUrls.UserUrl + "v1/users/register", content);
            request.EnsureSuccessStatusCode();

            var res =  JsonSerializer.Deserialize<VerificaGenericResponse>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
             )!;

            return res;
        }

        [HttpPost("endregister")]
        [ProducesResponseType(typeof(VerificaGenericResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<VerificaGenericResponse> EndRegisterAsync(VerificaAppUserDto user)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(user),
                Encoding.UTF8,
                new MediaTypeHeaderValue("application/json")
            );

            var request = await _httpClient.PostAsync(_apiUrls.UserUrl + "v1/users/endregister", content);
            request.EnsureSuccessStatusCode();

            var res = JsonSerializer.Deserialize<VerificaGenericResponse>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
             )!;

            return res;
        }

        [HttpPost("validate")]
        [ProducesResponseType(typeof(VerificaGenericResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<VerificaGenericResponse> ValidateAsync(VerificaAppUserDto user)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(user),
                Encoding.UTF8,
                new MediaTypeHeaderValue("application/json")
            );

            var request = await _httpClient.PostAsync(_apiUrls.UserUrl + "v1/users/validate", content);
            request.EnsureSuccessStatusCode();

            var res = JsonSerializer.Deserialize<VerificaGenericResponse>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
             )!;

            return res;
        }
    }
}
