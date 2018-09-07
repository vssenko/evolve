using Evolve.Application.Services;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Security;
using Evolve.Infrastructure.DB.EF.Repository;
using Evolve.Domain.PostAggr;
using Evolve.Infrastructure.DB.EF.LinqSpec;
using Evolve.Models;

namespace Evolve.Modules
{
    public class PostModule : NancyModule
    {
        IPostService _postService;

        public PostModule(IPostService postService, IRepository<Post> postRepository
             ,IRepository<Comment> commentRepository)
        {
            _postService = postService;

            Get["/post"] = _ =>
            {
                var id =(int) this.Request.Query["id"];
                return GetPost(id);
            };
            Get["/post/{id}"] = _ =>
            {
                return GetPost(_.id);
            };
            
        }

        protected dynamic GetPost(int postId)
        {
            var post = _postService.GetPost(postId);
            if (post == null)
            {
                return this.Response.AsRedirect("/notfound");
            }
            return View["/Post/Post.sshtml", post];
        }
    }
}
