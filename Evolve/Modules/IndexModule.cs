

namespace Evolve.Modules
{
    using Evolve.Application.Services;
    using Nancy;
    using System.Threading.Tasks;
    using Nancy.Security;


    public class IndexModule : NancyModule
    {
        public IndexModule(IPostService postService)
        {
            Get["/"] = _ =>
            {
                object model = null;
                if (Context.CurrentUser != null)
                {

                    model = new
                    {
                        Posts = postService.LoadUserLastSubscriptions(Context.CurrentUser.UserName)
                    };
                }

                return View["Index",model];
            };

            Get["/notfound"] = _ =>
            {
                return View["NotFound"];
            };
        }
    }
}