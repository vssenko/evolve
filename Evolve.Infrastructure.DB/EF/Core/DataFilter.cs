using Evolve.Infrastructure.DB.EF.LinqSpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Evolve.Infrastructure.DB.EF.Core
{
    static class DataFilter<TEntity> where TEntity : class
    {
        public static IQueryable<TEntity> Apply(IQueryable<TEntity> data, Specification<TEntity> spec, IncludeSpec<TEntity> includeSpec)
        {
            if (data == null)
                throw new ArgumentNullException("Data is null.");
            return ApplyEagerLoading(ApplySpecification(data, spec), includeSpec);
        }



        private static IQueryable<TEntity> ApplyEagerLoading(IQueryable<TEntity> data, IncludeSpec<TEntity> conf)
        {
            if (conf != null && conf.ExprList != null && conf.ExprList.Any())
            {
                foreach (var eagerLoadSpecification in conf.ExprList)
                {
                    data = data.Include(eagerLoadSpecification);
                }
            }
            return data;
        }

        private static IQueryable<TEntity> ApplySpecification(IQueryable<TEntity> data, Specification<TEntity> spec)
        {
            if (spec != null && spec.Expression != null)
            {
                data = data.Where(spec.Expression);
            }
            return data;
        }
    }
}
