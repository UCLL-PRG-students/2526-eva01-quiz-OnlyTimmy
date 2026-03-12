namespace PRG.EVA.Quiz.Models;

public class Answer
{
    public int Id { get; set; }
    public string Description { get; set; } =  string.Empty;
    public bool IsCorrect { get; set; }
}