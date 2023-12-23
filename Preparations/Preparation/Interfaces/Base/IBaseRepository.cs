namespace Preparation.Interfaces.Base
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetItemsAsync();
        Task<T> GetItemAsync(Guid id);
        Task<T> GreateAsync(T item);
        Task<T> EditAsync(T item);
        Task<T> DeleteAsync(T item);
    }
}
