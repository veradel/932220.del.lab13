using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using webLab2._1.Models;

namespace webLab2._1.Controllers
{
    public class CalculatorServiceController : Controller
    {
        private readonly Random _random = new Random();                

        private readonly ILogger<CalculatorServiceController> _logger;

        public CalculatorServiceController(ILogger<CalculatorServiceController> logger)
        {
            _logger = logger;                    
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Quiz()
        {
            QuizModel qModel = QuizModel.Instance;
            qModel.Reset();
            qModel.Start();


            return View(qModel);
        }

        [HttpPost]
        public IActionResult Quiz(QuizModel qModel, string action)
        {
            qModel = QuizModel.Instance;

            if (Request.Form["Answer"] != "") qModel.UserAnswer = Int32.Parse(Request.Form["Answer"]);
            else qModel.UserAnswer = 0;
            qModel.Questions();



            if (action == "Next")
            {
                QuizModel quModel = QuizModel.Instance;
                quModel.Start();

                return View(quModel);
            }
            return RedirectToAction("QuizResult");            

        }
        public IActionResult QuizResult()
        {
            QuizModel qModel = QuizModel.Instance;
            ViewBag.Result = qModel.AllAnswers;
            ViewData["Всего"] = "" + qModel.Count;
            ViewData["Правильно"] = "" + qModel.CountOfRightAnswers;
            return View();
        }        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}