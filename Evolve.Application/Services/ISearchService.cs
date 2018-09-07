using Evolve.Application.Models;
using Evolve.Domain.PostAggr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Application.Services
{
    public interface ISearchService
    {
        Task<List<Post>> Search(string searchParam, int page);
    }
}
