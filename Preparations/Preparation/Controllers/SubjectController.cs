using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Preparation.Interfaces;
using Preparation.Models;

namespace Preparation.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubject _subjectRepository;
        private readonly IQuestion _questionRepository;
        public SubjectController(ISubject subjectRepository, IQuestion questionRepository)
        {
            _subjectRepository = subjectRepository;
            _questionRepository = questionRepository;
        }

        public async Task<IActionResult> Index(string sortExpression = "", string searchText = "", int currentPage = 1, int pageSize = 5)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("name");
            sortModel.ApplySort(sortExpression);


            PaginatedList<Subject> items = await _subjectRepository.GetItemsAsync(sortModel.SortedProperty, sortModel.SortedOrder, searchText, currentPage, pageSize);

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
            var item = new Subject();

            PopulateViewBagsAsync().GetAwaiter().GetResult();

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Subject subject)
        {
            if(ModelState.IsValid)
            {
                await _subjectRepository.GreateAsync(subject);
                await PopulateViewBagsAsync();
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
                await PopulateViewBagsAsync();

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

                await PopulateViewBagsAsync();

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


        private async Task PopulateViewBagsAsync()
        {
            ViewBag.Questions = await GetQuestionsAsync();
        }

        private async Task<List<SelectListItem>> GetQuestionsAsync()
        {
            List<SelectListItem> listIItems = new List<SelectListItem>();

            List<Question> items = await _questionRepository.GetItemsAsync("name", SortOrder.Ascending, "", 1, 1000);

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
