using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Infrastructure.DB.EF.LinqSpec
{
    public class QueryParams<T> where T : class
    {
        public QueryParams()
        {

        }

        public QueryParams(Specification<T> specification)
        {
            Specification = specification;
        }

        public QueryParams(IncludeSpec<T> includeSpec)
        {
            IncludeSpec = includeSpec;
        }

        public QueryParams(Specification<T> specification, IncludeSpec<T> includeSpec)
            : this(specification)
        {
            IncludeSpec = includeSpec;
        }

        public QueryParams(Specification<T> specification, IncludeSpec<T> includeSpec, Pagination<T> pagination)
            :this(specification,includeSpec)
        {
            Pagination = pagination;
        }

        public QueryParams(Specification<T> specification, Pagination<T> pagination)
            : this(specification)
        {
            Pagination = pagination;
        }
        public Specification<T> Specification { get; set; }

        public IncludeSpec<T> IncludeSpec { get; set; }

        public Pagination<T> Pagination { get; set; }
    }
}
