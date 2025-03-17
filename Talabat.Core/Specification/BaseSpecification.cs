using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;

namespace Talabat.Core.Specification
{
    public class BaseSpecification<T>:ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criateria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set ; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool PaginationExection { get; set; }

        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T,bool>> Criteria)
        {
            Criateria = Criteria;
        }
        public void AddOrderBy(Expression<Func<T, object>> OrderByExpression) 
        {
            OrderBy = OrderByExpression;
        }
        public void AddOrderByDescending(Expression<Func<T, object>> OrderByDescendingExpression)
        {
            OrderByDescending = OrderByDescendingExpression;
        }
        public void AddPagination(int skip,int take)
        {
            PaginationExection = true;
            Skip = skip;
            Take = take;
        }
    }
}
