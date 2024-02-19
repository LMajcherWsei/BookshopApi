using System.Linq.Expressions;

namespace BookshopApi.Repository
{
    public interface IBookshopRepository<T>
    {
        Task<ICollection<T>> GetAllAsync();
        //Task<T> GetBookDetailsAsync(Expression<Func<T, bool>> filter);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false);
        //Task<T> GetBookByTitleAsync(Expression<Func<T, bool>> filter);
        Task<T> CreateAsync(T dbRecord);
        Task<T> UpdateAsync(T dbRecord);
        Task<bool> DeleteAsync(T dbRecord);
    }
}
