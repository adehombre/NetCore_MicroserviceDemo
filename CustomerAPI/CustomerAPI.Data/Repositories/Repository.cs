using CustomerAPI.Data.DataContext;
using CustomerAPI.Data.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : EntityBase, new()
    {
        protected readonly CustomerDataContext _customerDataContext;

        public Repository(CustomerDataContext customerContext)
        {
            _customerDataContext = customerContext;
        }

        public IQueryable<T> GetAll()
        {
            try
            {
              return _customerDataContext.Set<T>();
            }
            catch (Exception ex)
            {
              throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }
        public async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _customerDataContext.Set<T>().FindAsync(id);
                if (entity == null)
                    throw new KeyNotFoundException($"Entity {nameof(T)} was not found.");
                _customerDataContext.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception)
            {
                throw new Exception($"{typeof(T).Name} could not retrieved");
            }
        }
        public async Task<T> AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{nameof(entity)} should not be null");

            try
            {
                _customerDataContext.Add(entity);
                await _customerDataContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw new Exception($"{typeof(T).Name} could not be saved");
            }
        }
        public async Task<T> UpdateAsync(T entity, Guid id)
        {
            try {
                var ent = await GetByIdAsync(id);
                entity.Id = ent.Id;
                _customerDataContext.Update(entity);
                await _customerDataContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw new Exception($"{typeof(T).Name} could not be updated");
            }
        }
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var entity = _customerDataContext.Set<T>().FirstOrDefault(entity => entity.Id == id);
                if (entity == null)
                {
                    throw new KeyNotFoundException($"{nameof(T)} could not be deleted, id not found");
                }
                _customerDataContext.Remove(entity);
                await _customerDataContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception($"{typeof(T).Name} could not be deleted");
            }
        }
    }
}
