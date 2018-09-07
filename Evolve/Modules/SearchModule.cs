using Evolve.Application.Services;
using Evolve.Models;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Modules
{
    public class SearchModule : NancyModule
    {
        public SearchModule(ISearchService searchService)
        {

            Get["/search", true] = async (_, ct) =>
            {
                var searchParam = (string)this.Request.Query["searchParam"];
                var page = (int)this.Request.Query["page"];
                var posts = await searchService.Search(searchParam, page);
                var postViewModel = posts.Select(x => new PostViewModel()
                {
                    UserId = x.UserId,
                    PostId = x.PostId,
                    Title = x.Title,
                    CreatedDate = x.CreatedDate.ToString("yyyy-MM-dd"),
                    IsOwner = false,
                    User = x.User
                }).ToList();
                return View["/Post/SearchResult.sshtml", new { Posts = postViewModel, ShowNext = posts.Count() > 10 }];
            };
        }
    }
}
