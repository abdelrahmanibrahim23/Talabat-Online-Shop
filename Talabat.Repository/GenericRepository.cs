using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Repositories;
using Talabat.Core.Specification;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoryContext _context;

        public GenericRepository(StoryContext context) 
        {
            _context = context;
        }


        public async Task<IReadOnlyList<T>> GetAllAsync()
        => await _context.Set<T>().ToListAsync();

        public async Task<IReadOnlyList<T>> GetAllSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetAllSpecByIdAsync(ISpecification<T> specification)
        {
      return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        =>await _context.Set<T>().FindAsync(id);

        public async Task<int> GetCountAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> specification) 
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), specification);
        }
        public async Task CreateAsync(T entity)
        =>await _context.Set<T>().AddAsync(entity);
        public void UpdateAsync(T entity)
        =>_context.Set<T>().Update(entity);
        public void DeleteAsync(T entity)
        =>_context.Set<T>().Remove(entity);


    }
}
