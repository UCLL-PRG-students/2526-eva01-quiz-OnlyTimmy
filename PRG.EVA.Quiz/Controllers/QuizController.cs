using Microsoft.AspNetCore.Mvc;
using PRG.EVA.Quiz.Models;

namespace PRG.EVA.Quiz.Controllers;

public class QuizController : Controller
{
    private static Game _quiz = new Game(1, "Player1", new List<Question>());

    public QuizController()
    {
        if (_quiz.Questions.Count == 0)
        {
            _quiz.LoadQuestionsAsync().Wait();
        }
    }

    public IActionResult ShowAllQuestions()
    {
        return View(_quiz);
    }

    public IActionResult LoadNewGame()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LoadNewGame(string level = "")
    {
        _quiz = new Game(2, "Beta", new List<Question>());

        await _quiz.LoadQuestionsAsync(level);

        string moeilijkheid = level switch
        {
            "Easy" => "makkelijke",
            "Medium" => "gemiddelde",
            "Hard" => "moeilijke",
            _ => "alle"
        };
        
        TempData["NewGameMessage"] =
            $"We hebben een nieuwe game aangemaakt voor speler {_quiz.PlayerName} met {moeilijkheid} vragen.";

        return RedirectToAction(nameof(ShowAllQuestions));
    }

    public IActionResult AnswerOneQuestion(int questionId, int answerId)
    {
        Question? question = _quiz.Questions.Find(q => q.Id == questionId);
        if (question == null)
        {
            ViewBag.ErrorMessage = "Vraag niet gevonden.";
            return View();
        }

        Answer? answer = question.Answers.Find(a => a.Id == answerId);
        if (answer == null)
        {
            ViewBag.ErrorMessage = "Antwoord niet gevonden.";
            return View();
        }

        _quiz.NumberOfTrials++;
        bool isCorrect = answer.IsCorrect;

        if (isCorrect)
        {
            question.IsAnsweredCorrect = true;
        }

        ViewBag.GameId = _quiz.Id;
        ViewBag.PlayerName = _quiz.PlayerName;
        ViewBag.QuestionCount = _quiz.Questions.Count;
        ViewBag.NumberOfTrials = _quiz.NumberOfTrials;
        ViewBag.CorrectAnswers = _quiz.GetNumberOfCorrectAnswers();

        ViewBag.QuestionId = question.Id;
        ViewBag.QuestionDescription = question.Description;
        ViewBag.IsCorrect = isCorrect;

        TempData["LastAnsweredQuestion"] = question.Description;
        TempData["LastAnswerFeedback"] = isCorrect ? "Juist beantwoord" : "Fout beantwoord";

        return View();
    }
}