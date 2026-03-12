using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PRG.EVA.Quiz.Models;

namespace PRG.EVA.Quiz.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("ShowAllQuestions", "Quiz");
    }
}