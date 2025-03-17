using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Repositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoryContext _context;
        Hashtable _repository;
        public UnitOfWork(StoryContext context)
        {
            _context = context;
        }
        
        public async Task<int> Complete()
        => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repository == null) 
                _repository = new Hashtable();
            var type = typeof(TEntity).Name;
            if (!_repository.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_context);
                _repository.Add(type, repository);
            }
            return (IGenericRepository<TEntity>) _repository[type];
            
        }
    }
}
