using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NuGet.Protocol;
using Preparation.Interfaces;
using Preparation.Models;
using System.Diagnostics;

namespace Preparation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ISubject _subjectRepository;

        public HomeController(ILogger<HomeController> logger, ISubject subjectRepository)
        {
            _logger = logger;
            _subjectRepository = subjectRepository;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetQuestions(string subject)
        {
            List<Subject> items = await _subjectRepository.GetItemsAsync("name", SortOrder.Ascending, "", 1, 1000);
            Subject item = items.Where(x => x.Name == subject).FirstOrDefault();

            return View(item);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
