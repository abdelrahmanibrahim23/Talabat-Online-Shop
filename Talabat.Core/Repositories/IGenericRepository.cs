using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Specification;

namespace Talabat.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllSpecAsync(ISpecification<T> spec);
        Task<T> GetAllSpecByIdAsync(ISpecification<T> specification);
        Task<int> GetCountAsync(ISpecification<T> specification);
        Task CreateAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity); 
    }
}
