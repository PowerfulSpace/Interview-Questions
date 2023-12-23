using Microsoft.AspNetCore.Mvc;
using Preparation.Interfaces;
using Preparation.Models;

namespace Preparation.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestion _questionRepository;
        public QuestionController(IQuestion questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _questionRepository.GetItemsAsync();

            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var item = new Question();

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Question question)
        {
            if (ModelState.IsValid)
            {
                await _questionRepository.GreateAsync(question);
                return RedirectToAction(nameof(Index));
            }

            return View(question);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var item = await _questionRepository.GetItemAsync(id);

            if (item != null)
            {
                return View(item);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Question question)
        {
            if (ModelState.IsValid)
            {
                await _questionRepository.EditAsync(question);
                return RedirectToAction(nameof(Index));
            }

            return View(question);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _questionRepository.GetItemAsync(id);

            if (item != null)
            {
                return View(item);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Question question)
        {
            if (ModelState.IsValid)
            {
                await _questionRepository.DeleteAsync(question);
                return RedirectToAction(nameof(Index));
            }

            return View(question);
        }


        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var item = await _questionRepository.GetItemAsync(id);

            if (item != null)
            {
                return View(item);
            }

            return NotFound();
        }
    }
}
