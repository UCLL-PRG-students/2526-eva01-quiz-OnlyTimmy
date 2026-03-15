using System.Text.Json.Serialization;

namespace PRG.EVA.Quiz.Models;

public class Question
{
    public int Id { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    public List<Answer> Answers { get; set; } = new();

    [JsonPropertyName("level")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Difficulty Level { get; set; }

    public bool IsAnsweredCorrect { get; set; }

    public Question()
    {
    }

    public Question(int id, string description, List<Answer> answers, Difficulty level)
    {
        Id = id;
        Description = description;
        Answers = answers;
        Level = level;
        IsAnsweredCorrect = false;
    }
}