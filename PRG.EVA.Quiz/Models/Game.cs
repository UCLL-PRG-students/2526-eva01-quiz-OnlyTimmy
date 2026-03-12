namespace PRG.EVA.Quiz.Models;

public class Game
{    
    public int Id { get; set; }
    public string PlayerName { get; set; }
    public int NumberOfTrials { get; set; }
    public List<Question> Questions { get; set; }

    public Game(int id, string playerName, List<Question> questions)
    {
        Id = id;
        PlayerName = playerName;
        Questions = questions;
        NumberOfTrials = 0;
    }
}