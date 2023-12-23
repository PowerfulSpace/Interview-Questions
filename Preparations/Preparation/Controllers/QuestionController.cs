using Microsoft.AspNetCore.Mvc;

namespace Preparation.Controllers
{
    public class QuestionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
