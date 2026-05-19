using Application.DTOs;

namespace Application.Interfaces.Channels
{
    public interface IChannels
    {
        Task<HttpResponseMessage> CreateAsync(string token, CreateChannelDto dto);
        Task<HttpResponseMessage> GetMyChannelsAsync(string token);
        Task<HttpResponseMessage> SearchByNameAsync(string token,string channelName);
        Task ExitAsync(string token, Guid chnnelId );
        Task<HttpResponseMessage> GetChannelById(string token, Guid id);


    }

}