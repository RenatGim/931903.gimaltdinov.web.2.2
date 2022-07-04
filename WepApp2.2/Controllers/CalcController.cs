using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp2._2.Models;


namespace WebApp2._2.Controllers
{
    public class CalcController : Controller
    {
        private Random _random { get; set; } = new Random();

        private readonly ILogger<CalcController> _logger;

        public CalcController(ILogger<CalcController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ActionName("Manual")]
        public IActionResult ManualParsingSingle()
        {
            if (Request.Method == "POST")
            {
                try
                {
                    var calc = new CalculatorViewModel()
                    {
                        X = Int32.Parse(HttpContext.Request.Form["x"]),
                        Action = HttpContext.Request.Form["action"],
                        Y = Int32.Parse(HttpContext.Request.Form["y"])
                    };
                    ViewBag.Result = calc.Calc();
                }
                catch
                {
                    ViewBag.Result = "Invalid input";
                }
                return View("Result");
            }
            return View("Manual");
        }

        //-----------------------------------------

        [HttpGet]
        [ActionName("ManualWithParsingSeparate")]
        public IActionResult ManualWithParsingSeparateGet()
        {
            return View("Manual");
        }
        [HttpPost]
        [ActionName("ManualWithParsingSeparate")]
        public IActionResult ManualWithParsingSeparatePost()
        {
            try
            {
                var calc = new CalculatorViewModel()
                {
                    X = Int32.Parse(HttpContext.Request.Form["x"]),
                    Action = HttpContext.Request.Form["action"],
                    Y = Int32.Parse(HttpContext.Request.Form["y"])
                };
                ViewBag.Result = calc.Calc();
            }
            catch
            {
                ViewBag.Result = "Invalid input";
            }
            return View("Result");
        }

        //-----------------------------------------

        [HttpGet]
        [ActionName("ModelBindingInParameters")]
        public IActionResult ModelBindingParametersGet()
        {
            return View("Manual");
        }

        [HttpPost]
        [ActionName("ModelBindingInParameters")]
        public IActionResult ModelBindingParametersPost(int x, string action, int y)
        {
            if (ModelState.IsValid)
            {
                var calc = new CalculatorViewModel
                {
                    X = x,
                    Y = y,
                    Action = action
                };
                ViewBag.Result = calc.Calc();
            }
            else
            {
                ViewBag.Result = "Invalid input";
            }
            return View("Result");
        }

        //-----------------------------------------

        [HttpGet]
        [ActionName("ModelBindingInSeparateModels")]
        public IActionResult ModelBindingSeparateGet()
        {
            return View("Manual");
        }

        [HttpPost]
        [ActionName("ModelBindingInSeparateModels")]
        public IActionResult ModelBindingSeparatePost(CalculatorViewModel model)
        {
            ViewBag.Result = ModelState.IsValid
                ? model.Calc()
                : "Invalid input";

            return View("Result");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}