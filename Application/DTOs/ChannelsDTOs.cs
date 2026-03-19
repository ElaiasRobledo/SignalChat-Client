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

// public record MyChannelsDto
// (
//     Guid Id,
//     string Name,
//     string Description,
//     string? PublicId,
//     bool IsPublic,
//     int TotalMembers,
//     DateTime CreatedAt
// );


}
public class MyChannelsDto
{
    public string id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string? publicId { get; set; }
    public bool isPublic { get; set; }
    public int totalMembers { get; set; }
    public DateTime createdAt { get; set; }
}