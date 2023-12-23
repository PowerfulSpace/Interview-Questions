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

        public async Task<List<Question>> GetItemsAsync()
        {
            var items = await _context.Questions.AsNoTracking().Include(x => x.Subject).ToListAsync();

            return items;
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

    }
}
