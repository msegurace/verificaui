using Api.Gateways.Proxies.Config;
using Api.Gateways.Models;
using Api.Gateways.Models.Commands;
using Api.Gateways.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Api.Gateways.Proxies
{
    public interface IApplicationProxy
    {
        Task<DataCollection<AplicacionDto>> GetAllAsync(int page = 1, int take = 10, string? ids = null);
        Task<AplicacionDto> GetAsync(int id);
        Task<bool> CreateAsync(ApplicationCreateCommand command);
        Task<bool> ModifyAsync(ApplicationModifyCommand command);
    }
    public class ApplicationProxy : IApplicationProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public ApplicationProxy(IOptions<ApiUrls> apiUrls,
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);
            _apiUrls = apiUrls.Value;
            httpClient.BaseAddress = new Uri(_apiUrls.ApplicationUrl!);
            _httpClient = httpClient;
        }


        [HttpGet("getall")]
        public async Task<DataCollection<AplicacionDto>> GetAllAsync(int page = 1, int take = 10, string? ids = null)
        {
            var request = await _httpClient.GetAsync($"v1/app/getall?page={page}&take={take}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<AplicacionDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
             )!;
        }

        [HttpGet("get/{id}")]
        public async Task<AplicacionDto> GetAsync(int id)
        {
            var request = await _httpClient.GetAsync($"v1/app/get/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<AplicacionDto>(
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
        public async Task<bool> CreateAsync(ApplicationCreateCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 new MediaTypeHeaderValue("application/json")
             );

            var request = await _httpClient.PostAsync("v1/app/add", content);
            request.EnsureSuccessStatusCode();
            return true;

        }

        [HttpPost("modify")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<bool> ModifyAsync(ApplicationModifyCommand command)
        {
            var content = new StringContent(
               JsonSerializer.Serialize(command),
               Encoding.UTF8,
               new MediaTypeHeaderValue("application/json")
           );

            var request = await _httpClient.PostAsync("v1/app/modify", content);
            request.EnsureSuccessStatusCode();
            return true;
        }


    }
}

