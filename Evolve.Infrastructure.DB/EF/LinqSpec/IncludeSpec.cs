using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Infrastructure.DB.EF.LinqSpec
{
    public class IncludeSpec<TEntity> where TEntity : class
    {
        public IEnumerable<Expression<Func<TEntity, object>>> ExprList { get; private set; }

        public IncludeSpec(params Expression<Func<TEntity, object>>[] _list)
        {
            ExprList = _list;
        }
    }
}
