using Evolve.Application.Models;
using Evolve.Domain.PostAggr;
using Evolve.Domain.UserAggr;
using Evolve.Infrastructure.DB.EF.LinqSpec;
using Evolve.Infrastructure.DB.EF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Application.Services
{
    public class SearchService : ISearchService
    {
        IRepository<User> _userRepository;
        IRepository<Post> _postRepository;

        public SearchService(IRepository<User> userRepository, IRepository<Post> postRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        public async Task<List<Post>> Search(string searchParam, int page)
        {
            searchParam = searchParam.ToLower();
            var spec = new Specification<Post>(x => x.Title.ToLower().Contains(searchParam));
            return await _postRepository.GetListAsync(new QueryParams<Post>(spec, new IncludeSpec<Post>(x => x.User), new Pagination<Post>(x => x.PostId, page - 1, 1, 10)));
        }
    }
}
