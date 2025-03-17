using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Specification;

namespace Talabat.Repository.Data
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery,ISpecification<T> spec ) 
        {
            var query = inputQuery;
            if (spec.Criateria != null)
                query = query.Where(spec.Criateria);
            if(spec.PaginationExection)
                query=query.Skip(spec.Skip).Take(spec.Take);
            if(spec.OrderBy != null)
            query= query.OrderBy(spec.OrderBy);
            if(spec.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);
            query=spec.Includes.Aggregate(query,(currentQuery,IncludeExpression)
                =>currentQuery.Include(IncludeExpression));
            return query;

        }
    }
}
