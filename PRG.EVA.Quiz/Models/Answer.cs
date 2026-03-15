using System.Text.Json.Serialization;

namespace PRG.EVA.Quiz.Models;

public class Answer
{
    public int Id { get; set; }

    [JsonPropertyName("text")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("isCorrect")]
    public bool IsCorrect { get; set; }

    public Answer()
    {
    }
}