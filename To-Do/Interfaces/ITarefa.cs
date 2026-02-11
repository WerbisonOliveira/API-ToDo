using System.Collections.Generic;
using System.Threading.Tasks;

namespace To_Do.Interfaces
{
    internal interface ITarefa<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> PatchAsync(T entity);
        Task<T> DeleteAsync(int id);
    }
}
