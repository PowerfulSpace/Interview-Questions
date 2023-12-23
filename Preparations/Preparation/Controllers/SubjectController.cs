using Microsoft.AspNetCore.Mvc;
using Preparation.Interfaces;
using Preparation.Models;

namespace Preparation.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubject _subjectRepository;
        public SubjectController(ISubject subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _subjectRepository.GetItemsAsync();

            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var item = new Subject();

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Subject subject)
        {
            if(ModelState.IsValid)
            {
                await _subjectRepository.GreateAsync(subject);
                return RedirectToAction(nameof(Index));
            }

            return View(subject);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var item = await _subjectRepository.GetItemAsync(id);

            if(item != null)
            {
                return View(item);
            }

            return NotFound();          
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Subject subject)
        {
            if (ModelState.IsValid)
            {
                await _subjectRepository.EditAsync(subject);
                return RedirectToAction(nameof(Index));
            }

            return View(subject);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _subjectRepository.GetItemAsync(id);

            if (item != null)
            {
                return View(item);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Subject subject)
        {
            if (ModelState.IsValid)
            {
                await _subjectRepository.DeleteAsync(subject);
                return RedirectToAction(nameof(Index));
            }

            return View(subject);
        }


        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var item = await _subjectRepository.GetItemAsync(id);

            if (item != null)
            {
                return View(item);
            }

            return NotFound();
        }
    }
}
