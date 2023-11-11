﻿using Api.Gateways.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
//https://youtu.be/5Xwj3DvvOyM?si=KDyBz_z0-_WmZSiC
namespace Api.Gateways.Proxies
{
    public interface IIdentityProxy
    {
        Task<string> LoginAsync([FromBody] LoginInformation info);
    }
    public class IdentityProxy:IIdentityProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public IdentityProxy(IOptions<ApiUrls> apiUrls, HttpClient httpClient)
        {
            _apiUrls = apiUrls.Value;
            httpClient.BaseAddress = new Uri(_apiUrls.IdentityUrl!);
            _httpClient = httpClient;

        }

        public async Task<string> LoginAsync(LoginInformation info)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(info),
                Encoding.UTF8,
                new MediaTypeHeaderValue("application/json")
            );

            var resp =   await _httpClient.PostAsync("/v1/identity/login", content);
            resp.EnsureSuccessStatusCode();

            return await resp.Content.ReadAsStringAsync();
        }
    }
}
