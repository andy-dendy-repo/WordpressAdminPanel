using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordpressClient.Data;
using WordpressClient.Services.Interfaces;

namespace WordpressClient.Services
{
    public class ServiceBase<T> : IServiceBase<T> where T : class
    {
        protected readonly AdminDbContext _context;

        private DbSet<T> _entities;

        protected virtual ulong GetKey(T entity)
        {
            var keyName = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties
                .Select(x => x.Name).Single();

            return (ulong)entity.GetType().GetProperty(keyName).GetValue(entity, null);
        }

        public ServiceBase(AdminDbContext applicationContext)
        {
            _context = applicationContext;
            _entities = applicationContext.Set<T>();
        }

        public IQueryable<T> GetQueryableNoTracking()
        {
            return _entities.AsNoTracking();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public virtual T GetByIdAsync(ulong id)
        {
            return _entities.AsEnumerable().FirstOrDefault(entity => GetKey(entity) == id);
        }

        public virtual async Task Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            _entities.Add(entity);

            await _context.SaveChangesAsync();
        }

        public void Update(T updatedEntity)
        {
            _entities.Update(updatedEntity);
        }

        public async Task DeleteAsync(ulong id)
        {
            var found = _entities.AsEnumerable().FirstOrDefault(entity => GetKey(entity) == id);

            if (found != null)
            {
                _entities.Remove(found);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsEntityExistAsync(ulong id)
        {
            return await _entities.AnyAsync(x => GetKey(x) == id);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _entities.AddRange(entities);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _entities.UpdateRange(entities);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _entities.RemoveRange(entities);
        }
    }
}
