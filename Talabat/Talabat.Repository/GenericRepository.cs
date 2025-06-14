using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.RepositoriesContaract;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
            {
                return (IEnumerable<T>) await _dbContext.Set<Product>().Include(p => p.Brand).Include(p => p.Category).ToListAsync();   
            }
            return await _dbContext.Set<T>().ToListAsync();
        }   
        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Product))
            {
                return await _dbContext.Set<Product>().Where(p => p.Id == id).Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync() as T;
            }
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public IQueryable<T> ApplySpecifications(ISpecifications<T> specifications)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), specifications);
        }
        public async Task<T?> GetByIdWithSpecAsync( ISpecifications<T> specifications)
        {
            return await ApplySpecifications(specifications).FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> specifications)
        {
            return await ApplySpecifications(specifications).ToListAsync();
        }

        public Task<int> GetCountAsync(ISpecifications<T> specifications)
        {
            return ApplySpecifications(specifications).CountAsync();
        }
    }
}
