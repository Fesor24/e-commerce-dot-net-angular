using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class SpecificationEvaluator<TEntity> where TEntity: BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> entity, ISpecification<TEntity> spec)
        {
            IQueryable<TEntity> query = entity;
            
            if(spec.Criteria is not null) query = query.Where(spec.Criteria);

            if (spec.OrderByExpression is not null) query = query.OrderBy(spec.OrderByExpression);

            if(spec.OrderByDescendingExpression is not null) query = query.OrderByDescending(spec.OrderByDescendingExpression);


            //we set isPagingEnabled to true in our BaseSpec so paging will be enabled as the condition is true
            if (spec.IsPagingEnabled) query = query.Skip(spec.Skip).Take(spec.Take);

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
         
        }   
    }
}
