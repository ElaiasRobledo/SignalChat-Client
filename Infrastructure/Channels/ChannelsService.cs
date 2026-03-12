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

        
    }

}