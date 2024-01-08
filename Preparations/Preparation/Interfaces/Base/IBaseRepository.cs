using Microsoft.Data.SqlClient;
using Preparation.Models;

namespace Preparation.Interfaces.Base
{
    public interface IBaseRepository<T>
    {
        Task<PaginatedList<T>> GetItemsAsync(string sortProperty, SortOrder order, string searchText, int pageIndex, int pageSize);
        Task<T> GetItemAsync(Guid id);
        Task<T> GreateAsync(T item);
        Task<T> EditAsync(T item);
        Task<T> DeleteAsync(T item);
    }
}
