﻿using Api.Gateways.Proxies.Config;
using Api.Gateways.Models;
using Api.Gateways.Models.Commands;
using Api.Gateways.Models.DTOs;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateways.Proxies
{
    public interface ITokenProxy
    {
        Task<DataCollection<Token2FADto>> GetAllAsync(int page = 1, int take = 10, string? ids = null);
        Task<Token2FADto> GetAsync(int id);
        Task<bool> CreateAsync(TokenCreateCommand command);
        Task<bool> ModifyAsync(TokenModifyCommand command);
    }
    public class TokenProxy: ITokenProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public TokenProxy(IOptions<ApiUrls> apiUrls,
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);
            _apiUrls = apiUrls.Value;
            httpClient.BaseAddress = new Uri(_apiUrls.TokenUrl!);
            _httpClient = httpClient;
        }


        [HttpGet("getall")]
        public async Task<DataCollection<Token2FADto>> GetAllAsync(int page = 1, int take = 10, string? ids = null)
        {
            var request = await _httpClient.GetAsync($"v1/users/getall/?page={page}&take={take}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<Token2FADto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
             )!;
        }

        [HttpGet("get/{id}")]
        public async Task<Token2FADto> GetAsync(int id)
        {
            var request = await _httpClient.GetAsync($"v1/users/get/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<Token2FADto>(
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
        public async Task<bool> CreateAsync(TokenCreateCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 new MediaTypeHeaderValue("application/json")
             );

            var request = await _httpClient.PostAsync("v1/users/add", content);
            request.EnsureSuccessStatusCode();
            return true;

        }

        [HttpPost("modify")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<bool> ModifyAsync(TokenModifyCommand command)
        {
            var content = new StringContent(
               JsonSerializer.Serialize(command),
               Encoding.UTF8,
               new MediaTypeHeaderValue("application/json")
           );

            var request = await _httpClient.PostAsync("v1/users/modify", content);
            request.EnsureSuccessStatusCode();
            return true;
        }


    }
}