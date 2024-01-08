using Humanizer.Localisation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Preparation.Data;
using Preparation.Interfaces;
using Preparation.Models;

namespace Preparation.Repositories
{
    public class SubjectRepository : ISubject
    {
        private readonly ApplicationContext _context;

        public SubjectRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Subject>> GetItemsAsync(string sortProperty, SortOrder order, string searchText, int pageIndex, int pageSize)
        {
            List<Subject> items;

            if (searchText != "" && searchText != null)
            {
                items = _context.Subjects
                    .Include(x => x.Questions)
                    .Where(x => x.Name.Contains(searchText))
                    .ToList();
            }
            else
            {
                items = _context.Subjects
                    .Include(x => x.Questions)
                    .ToList();
            }

            items = await DoSortAsync(items, sortProperty, order);
            PaginatedList<Subject> retUnits = new PaginatedList<Subject>(items, pageIndex, pageSize);
            return retUnits;
        }

        public async Task<Subject> GetItemAsync(Guid id)
        {
            var item = await _context.Subjects.Include(x => x.Questions).FirstOrDefaultAsync(x => x.Id == id);

            return item;
        }


        public async Task<Subject> GreateAsync(Subject item)
        {
            await _context.Subjects.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Subject> EditAsync(Subject item)
        {
            _context.Subjects.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Subject> DeleteAsync(Subject item)
        {
            item = await GetItem_NoDownload_FG_Async(item.Id);

            _context.Subjects.Attach(item);
            _context.Entry(item).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return item;
        }

        private async Task<Subject> GetItem_NoDownload_FG_Async(Guid id) => _context.Subjects.FirstOrDefault(x => x.Id == id);


        private async Task<List<Subject>> DoSortAsync(List<Subject> items, string sortProperty, SortOrder order)
        {
            if (sortProperty.ToLower() == "name")
            {
                if (order == SortOrder.Ascending)
                    items = items.OrderBy(x => x.Name).ToList();
                else
                    items = items.OrderByDescending(x => x.Name).ToList();
            }
            return items;
        }

    }
}
