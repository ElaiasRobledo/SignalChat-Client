namespace Application.DTOs
{
    public record CreateChannelDto
    (
        string Name,
        string Description,
        List<string> Tags,
        bool IsPublic,
        bool IsPublicName,
        bool IsVisible

    );


}