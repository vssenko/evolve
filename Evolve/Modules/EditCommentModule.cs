using Evolve.Application.Services;
using Evolve.Domain.PostAggr;
using Evolve.Infrastructure.DB.EF.LinqSpec;
using Evolve.Infrastructure.DB.EF.Repository;
using Evolve.Models;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Security;
namespace Evolve.Modules
{
    public class EditCommentModule : NancyModule
    {
        public EditCommentModule(IRepository<Comment> commentRepository, IUserService userService)
        {
            this.RequiresAuthentication();

            Post["/newcomment"] = _ =>
            {
                int postId = this.Request.Form.postId;
                string body = this.Request.Form.comment;
                var user = userService.GetUserByUsername(Context.CurrentUser.UserName);
                commentRepository.Insert(new Comment()
                    {
                        User = user,
                        Body = body,
                        CreatedDate = DateTime.Now,
                        PostId = postId
                    });
                return Response.AsRedirect("/post/" + postId);
            };
        }
    }
}
