using Microsoft.AspNetCore.Mvc;
using PRG.EVA.Quiz.Models;

namespace PRG.EVA.Quiz.Controllers;

public class QuizController : Controller
{
    private static Game _quiz = new Game(
        1,
        "Player1",
        new List<Question> {
        new Question(1,"Wat is de kleur van de lucht?",
            new List<Answer> {
                new Answer{Id=1,IsCorrect=false,Description="Groen"},
                new Answer{Id=2,IsCorrect=true,Description="Blauw"},
                new Answer{Id=3,IsCorrect=false,Description="Rood"},
                new Answer{Id=4,IsCorrect=false,Description="Geel"}
            },
            Difficulty.Easy),

        new Question(2,"Hoeveel is 2 + 2?",
            new List<Answer> {
                new Answer{Id=1,IsCorrect=false,Description="3"},
                new Answer{Id=2,IsCorrect=true,Description="4"},
                new Answer{Id=3,IsCorrect=false,Description="5"},
                new Answer{Id=4,IsCorrect=false,Description="22"}
            },
            Difficulty.Easy),

        new Question(3,"Wat is de hoofdstad van Frankrijk?",
            new List<Answer> {
                new Answer{Id=1,IsCorrect=false,Description="Londen"},
                new Answer{Id=2,IsCorrect=false,Description="Berlijn"},
                new Answer{Id=3,IsCorrect=true,Description="Parijs"},
                new Answer{Id=4,IsCorrect=false,Description="Madrid"}
            },
            Difficulty.Medium),

        new Question(4,"Hoeveel zijden heeft een driehoek?",
            new List<Answer> {
                new Answer{Id=1,IsCorrect=false,Description="2"},
                new Answer{Id=2,IsCorrect=true,Description="3"},
                new Answer{Id=3,IsCorrect=false,Description="4"},
                new Answer{Id=4,IsCorrect=false,Description="5"}
            },
            Difficulty.Easy)
    });

    public IActionResult ShowAllQuestions()
    {
        return View(_quiz);
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
        int correctAnswers = _quiz.Questions.Count(q => q.IsAnsweredCorrect);

        ViewBag.GameId = _quiz.Id;
        ViewBag.PlayerName = _quiz.PlayerName;
        ViewBag.QuestionCount = _quiz.Questions.Count;
        ViewBag.NumberOfTrials = _quiz.NumberOfTrials;
        ViewBag.CorrectAnswers = correctAnswers;

        ViewBag.QuestionId = question.Id;
        ViewBag.QuestionDescription = question.Description;
        ViewBag.IsCorrect = isCorrect;

        TempData["LastAnsweredQuestion"] = question.Description;
        TempData["LastAnswerFeedback"] = isCorrect ? "Juist beantwoord" : "Fout beantwoord";

        return View();
    }

}