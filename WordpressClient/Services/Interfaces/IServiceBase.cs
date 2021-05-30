using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordpressClient.Services.Interfaces
{
    public interface IServiceBase<T>
    {
        IQueryable<T> GetQueryableNoTracking();

        Task<List<T>> GetAllAsync();

        T GetByIdAsync(ulong id);

        Task Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Update(T entity);

        Task DeleteAsync(ulong id);

        Task<bool> IsEntityExistAsync(ulong id);

        void UpdateRange(IEnumerable<T> entities);

        void RemoveRange(IEnumerable<T> entities);
    }
}
