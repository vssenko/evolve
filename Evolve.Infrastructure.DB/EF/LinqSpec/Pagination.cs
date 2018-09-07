using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Infrastructure.DB.EF.LinqSpec
{
    public class Pagination<TEntity>
    {
        public int Skip { get; private set; }
        public int Take { get; private set; }
        public Expression<Func<TEntity,int>> FuckingEFHackExprToIntKey { get; private set; } //EF Can't SKIP & TAKE ON NOT-ORDERBYED COLLECTIONS!!! FUFCKING HACK
        public Pagination(Expression<Func<TEntity, int>> orderByClause, int skipPages, int takePages, int countPerPage)
        {
            FuckingEFHackExprToIntKey = orderByClause;
            Skip = skipPages * countPerPage;
            Take = takePages * countPerPage;
        }

    }
}
