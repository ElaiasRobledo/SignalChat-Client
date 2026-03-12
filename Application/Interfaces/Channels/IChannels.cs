using Application.DTOs;

namespace Application.Interfaces.Channels
{
    public interface IChannels
    {
        Task<HttpResponseMessage> CreateAsync(string token, CreateChannelDto dto);


    }

}