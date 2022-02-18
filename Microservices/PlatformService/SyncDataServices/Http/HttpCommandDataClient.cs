﻿using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpclient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration )
        {
            _httpclient = httpClient;
            _configuration = configuration;
        }
        
        public async Task SendPlatformToCommand(PlatformReadDto plat)
        {
            var httpContent = new StringContent(
                    JsonSerializer.Serialize(plat),
                    Encoding.UTF8,
                    "application/json" );

            var response = await _httpclient.PostAsync($"{_configuration["CommandService"]}", httpContent);
            
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("==> Sync POST a CommandService fue OK");
            } else
            {
                Console.WriteLine("==> Sync POST a CommandService NO fue OK");
            }
        }
    }
}
