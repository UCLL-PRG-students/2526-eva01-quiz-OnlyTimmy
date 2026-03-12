using Microsoft.AspNetCore.Mvc;
using PRG.EVA.Quiz.Models;

namespace PRG.EVA.Quiz.Controllers;

public class QuizController : Controller
{
    private static List<Question> _questions = new List<Question> {
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
    };

    public IActionResult ShowAllQuestions()
    {
        return View(_questions);
    }

    public IActionResult AnswerOneQuestion(int questionId, int answerId)
    {
        Question? question = _questions.Find(q => q.Id == questionId);

        if (question == null)
        {
            return View();
        }

        Answer? answer = question.Answers.Find(a => a.Id == answerId);

        if (answer == null)
        {
            return View();
        }

        if (answer.IsCorrect)
        {
            question.IsAnsweredCorrect = true;
            return RedirectToAction("Congratulations");
        }

        return RedirectToAction("Miss");
    }

    public IActionResult Congratulations()
    {
        return View();
    }

    public IActionResult Miss()
    {
        return View();
    }
}