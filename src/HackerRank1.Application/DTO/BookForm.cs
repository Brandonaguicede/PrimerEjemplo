using System.Text.Json.Serialization;

namespace HackerRank1.Application.DTO;

public class BookForm
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    [JsonPropertyName("libraryId")]
    public int LibraryId { get; set; }
}
