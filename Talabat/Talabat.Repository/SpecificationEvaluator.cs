using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery( IQueryable<T> inputQuery , ISpecifications<T> specifications)
        {
            var query = inputQuery;

            if (specifications.Criteria != null)
            {
                query = query.Where( specifications.Criteria );
            }

            if(specifications.OrderByAscending != null)
            {
                query = query.OrderBy(specifications.OrderByAscending );
            }

            if (specifications.OrderByDscending != null)
            {
                query = query.OrderByDescending( specifications.OrderByDscending );
            }

            if(specifications.IsPaginationEnable)
            {
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            }

            query = specifications.Includes.Aggregate(query, (currentQuery, IncludeExpression) => currentQuery.Include(IncludeExpression));

            return query;

        }
    }
}
