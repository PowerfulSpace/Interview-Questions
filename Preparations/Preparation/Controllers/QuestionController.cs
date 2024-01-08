using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Preparation.Interfaces;
using Preparation.Models;

namespace Preparation.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestion _questionRepository;
        private readonly ISubject _subjectRepository;
        public QuestionController(IQuestion questionRepository, ISubject subjectRepository)
        {
            _questionRepository = questionRepository;
            _subjectRepository = subjectRepository;
        }


        public async Task<IActionResult> Index(string sortExpression = "", string searchText = "", int currentPage = 1, int pageSize = 5)
        {

            SortModel sortModel = new SortModel();
            sortModel.AddColumn("name");
            sortModel.AddColumn("answer");
            sortModel.AddColumn("subject");
            sortModel.ApplySort(sortExpression);

            PaginatedList<Question> items = await _questionRepository.GetItemsAsync(sortModel.SortedProperty, sortModel.SortedOrder, searchText, currentPage, pageSize);

            var pager = new PagerModel(items.TotalRecords, currentPage, pageSize);
            pager.SortExpression = sortExpression;
            pager.SearchText = searchText;

            ViewBag.Pager = pager;

            ViewData["SortModel"] = sortModel;
            ViewBag.SearchText = searchText;

            TempData["SearchText"] = searchText;
            TempData.Keep("SearchText");

            TempData["PageSize"] = pageSize;
            TempData.Keep("PageSize");

            TempData["CurrentPage"] = currentPage;
            TempData.Keep("CurrentPage");

            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var item = new Question();
            PopulateViewBagsAsync().GetAwaiter().GetResult();

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
            await PopulateViewBagsAsync();
            return View(question);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var item = await _questionRepository.GetItemAsync(id);
            await PopulateViewBagsAsync();

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
            await PopulateViewBagsAsync();
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


        private async Task PopulateViewBagsAsync()
        {
            ViewBag.Subjects = await GetSubjectsAsync();
        }

        private async Task<List<SelectListItem>> GetSubjectsAsync()
        {
            List<SelectListItem> listIItems = new List<SelectListItem>();

            List<Subject> items = await _subjectRepository.GetItemsAsync("name", SortOrder.Ascending, "", 1, 1000);

            listIItems = items.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            SelectListItem defItem = new SelectListItem()
            {
                Text = "---Select Subject---",
                Value = ""
            };

            listIItems.Insert(0, defItem);
            return listIItems;
        }

    }
}
