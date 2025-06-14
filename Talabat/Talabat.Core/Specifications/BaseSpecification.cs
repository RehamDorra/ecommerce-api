using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecification<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; } = null;
        public List<Expression<Func<T, object>>> Includes { get ; set ; } = new List<Expression<Func<T, object>>> ();
        public Expression<Func<T, object>> OrderByAscending { get; set; } = null;
        public Expression<Func<T, object>> OrderByDscending { get; set; } = null;
        public int Take { get ; set ; } = 0;
        public int Skip { get; set; } = 0;
        public bool IsPaginationEnable { get; set; } = false;

        public BaseSpecification()
        { 
            
        }
        public BaseSpecification(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
        }
        public void AddOrderByAscending(Expression<Func<T, object>> OrderByAscExpression)
        {
            OrderByAscending = OrderByAscExpression;
        }
        public void AddOrderByDescending(Expression<Func<T, object>> OrderByDescExpression)
        {
            OrderByDscending = OrderByDescExpression;
        }
        public void ApplyPagination(int skip , int take)
        {
            IsPaginationEnable = true;
            Skip = skip;
            Take = take;
        }
    }
}
