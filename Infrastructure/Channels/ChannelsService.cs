using System.Net.Http.Headers;
using System.Net.Http.Json;
using Application.DTOs;
using Application.Interfaces.Channels;

namespace Infrastructure.Channels
{
    public class ChannelsService : IChannels
    {
        private readonly HttpClient _httpClient;

        public ChannelsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> JoinChannelAsync(string token, Guid chnnelId)
        {
            _httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", token);
        
            var response = await _httpClient.PostAsync($"join/{chnnelId}", null);
            return response;
            
        }
        
       public async Task<HttpResponseMessage> CreateAsync(string token, CreateChannelDto dto)
        {
            _httpClient.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var request = new
                {
                    name = dto.Name,
                    description = dto.Description,
                    tags = dto.Tags,
                    isPublic = dto.IsPublic,
                    isPublicName = dto.IsPublicName,
                    isVisible = dto.IsVisible,
                };

                var response = await _httpClient.PostAsJsonAsync(string.Empty,request);
                return response;
        
        }

        public async Task<HttpResponseMessage> GetMyChannelsAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization
            = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


            var response = await _httpClient.GetAsync("mychannels");
            return response;            
        }

        public async Task<HttpResponseMessage> GetChannelById(string token, Guid id)
        {
            _httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{id}");
            return response;
        }
        public async Task<HttpResponseMessage> SearchByNameAsync(string token,string channelName)
        {

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"search/name?name={Uri.EscapeDataString(channelName)}"
            );
            request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

            return await _httpClient.SendAsync(request);
        }
        public async Task ExitAsync(string token, Guid chnnelId )
        {
            _httpClient.DefaultRequestHeaders.Authorization
            = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.DeleteAsync($"exit/{chnnelId}");
            
        }
        
    }

}