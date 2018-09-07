using Evolve.Application.Services;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Security;
using Evolve.Domain.PostAggr;
using Evolve.Infrastructure.DB.EF.Repository;

namespace Evolve.Modules
{
    public class EditPostModule : NancyModule
    {
        public EditPostModule(IPostService postService,IRepository<Post> postRepository)
        {
            this.RequiresAuthentication();

            Get["/newpost"] = _ => View["/Post/NewPost.sshtml"];

            Post["/newpost"] = _ =>
            {
                var userName = (string)this.Context.CurrentUser.UserName;
                var title = (string)this.Request.Form.title;
                var body = (string)this.Request.Form.body;
                var post = postService.CreatePost(title, body, userName);
                return Response.AsRedirect("/post/" + post.PostId);
            };

            Post["/deletepost"] = _ =>
            {
                var postId = (int) this.Request.Form.PostId;
                var post = postService.GetPost(postId);
                if (post.User.Username != this.Context.CurrentUser.UserName)
                {
                    return HttpStatusCode.Unauthorized;
                }
                else
                {
                    postRepository.Delete(post);
                    return HttpStatusCode.OK;
                }
            };
        }
    }
}
