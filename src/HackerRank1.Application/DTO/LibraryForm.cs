using System.Text.Json.Serialization;

namespace HackerRank1.Application.DTO;

public class LibraryForm
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("location")]
    public string Location { get; set; } = string.Empty;
}
