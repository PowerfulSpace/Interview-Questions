using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Preparation.Data;
using Preparation.Interfaces;
using Preparation.Models;

namespace Preparation.Repositories
{
    public class QuestionRepository : IQuestion
    {
        private readonly ApplicationContext _context;

        public QuestionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Question>> GetItemsAsync(string sortProperty, SortOrder order, string searchText, int pageIndex, int pageSize)
        {
            List<Question> items;

            if (searchText != "" && searchText != null)
            {
                items = _context.Questions
                    .Include(x => x.Subject)
                    .Where(x => x.Name.Contains(searchText) || x.Answer.Contains(searchText))
                    .ToList();
            }
            else
            {
                items = _context.Questions
                    .Include(x => x.Subject)
                    .ToList();
            }

            items = await DoSortAsync(items, sortProperty, order);
            PaginatedList<Question> retUnits = new PaginatedList<Question>(items, pageIndex, pageSize);
            return retUnits;
        }

        public async Task<Question> GetItemAsync(Guid id)
        {
            var item = await _context.Questions.Include(x => x.Subject).FirstOrDefaultAsync(x => x.Id == id);

            return item;
        }


        public async Task<Question> GreateAsync(Question item)
        {
            await _context.Questions.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Question> EditAsync(Question item)
        {
            _context.Questions.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Question> DeleteAsync(Question item)
        {
            item = await GetItem_NoDownload_FG_Async(item.Id);

            _context.Questions.Attach(item);
            _context.Entry(item).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return item;
        }

        private async Task<Question> GetItem_NoDownload_FG_Async(Guid id) => _context.Questions.FirstOrDefault(x => x.Id == id);




        private async Task<List<Question>> DoSortAsync(List<Question> items, string sortProperty, SortOrder order)
        {
            if (sortProperty.ToLower() == "name")
            {
                if (order == SortOrder.Ascending)
                    items = items.OrderBy(x => x.Name).ToList();
                else
                    items = items.OrderByDescending(x => x.Name).ToList();
            }
            else if (sortProperty.ToLower() == "answer")
            {
                if (order == SortOrder.Ascending)
                    items = items.OrderBy(x => x.Answer).ToList();
                else
                    items = items.OrderByDescending(x => x.Answer).ToList();
            }
            else if (sortProperty.ToLower() == "subject")
            {
                if (order == SortOrder.Ascending)
                    items = items.OrderBy(x => x.Subject?.Name).ToList();
                else
                    items = items.OrderByDescending(x => x.Subject?.Name).ToList();
            }
            return items;
        }



    }
}
