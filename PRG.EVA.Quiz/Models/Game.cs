namespace PRG.EVA.Quiz.Models;
using System.Linq;
using System.Net;

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
    public int GetNumberOfCorrectAnswers()
    {
        return Questions.Count(q => q.IsAnsweredCorrect);
    }
    public async Task LoadQuestionsAsync(string level = "")
    {
        using HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://mgp32-api.azurewebsites.net/");

        Questions.Clear();
        int questionId = 1;

        while (Questions.Count < 10)
        {
            Task<HttpResponseMessage> task1 = client.GetAsync("questions");
        
            HttpResponseMessage[] responses = await Task.WhenAll(task1);

            foreach (HttpResponseMessage response in responses)
            {
                QuizResponse? quizResponse = await response.Content.ReadFromJsonAsync<QuizResponse>();

                if (quizResponse == null)
                {
                    continue;
                }

                foreach (Question question in quizResponse.Questions)
                {
                    if (level != "" && question.Level.ToString() != level)
                    {
                        continue;
                    }

                    question.Id = questionId++;
                    question.Description = WebUtility.HtmlDecode(question.Description);

                    int answerId = 1;
                    foreach (Answer answer in question.Answers)
                    {
                        answer.Id = answerId++;
                        answer.Description = WebUtility.HtmlDecode(answer.Description);
                    }

                    Questions.Add(question);

                    if (Questions.Count == 10)
                    {
                        return;
                    }
                }
            }
        }
    }    
}