using Evolve.Application.Services;
using Evolve.Domain.UserAggr;
using Evolve.Infrastructure.DB.EF.LinqSpec;
using Evolve.Infrastructure.DB.EF.Repository;
using Evolve.Models;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Modules
{
    public class UserInfoModule : NancyModule
    {
        IRepository<User> _userRepository;
        IPostService _postService;

        public UserInfoModule(IRepository<User> userRepository, IPostService postService, IUserService userService)
        {
            _userRepository = userRepository;
            _postService = postService;

            Get["/user/{id}"] = _ =>
            {
                var id = (int) _.id;
                return GetUserPosts(id, 1);
            };

            Get["/user/{id}/{page}"] = _ =>
            {
                var id = (int)_.id;
                var page = (int)_.page;
                return GetUserPosts(id, page);
            };

            Get["/myposts"] = _ =>
            {
                try
                {
                    var id = userService.GetUserByUsername(Context.CurrentUser.UserName).UserId;
                    return GetUserPosts(id,1);
                }
                catch
                {
                    return null;
                }
            };

            Post["/subscribe"] = _ =>
            {
                int userId = this.Request.Form.UserId;
                var currentUser = userService.GetUserByUsername(this.Context.CurrentUser.UserName);
                var pageUser = userRepository.GetBySpec(new QueryParams<User>(new Specification<User>(x => x.UserId == userId), new IncludeSpec<User>(x => x.Subscribers)));
                pageUser.Subscribers.Add(currentUser);
                userRepository.Update(pageUser);
                return HttpStatusCode.OK;
            };
            Post["/unsubscribe"] = _ =>
            {
                int userId = this.Request.Form.UserId;
                var currentUser = userService.GetUserByUsername(this.Context.CurrentUser.UserName);
                var pageUser = userRepository.GetBySpec(new QueryParams<User>(new Specification<User>(x => x.UserId == userId), new IncludeSpec<User>(x => x.Subscribers)));
                pageUser.Subscribers.Remove(currentUser);
                userRepository.Update(pageUser);
                return HttpStatusCode.OK;
            };
           
        }

         private dynamic GetUserPosts(int userId,int page)
        {
            var user = _userRepository.GetBySpec(new QueryParams<User>(new Specification<User>(x => x.UserId == userId),
                                                         new IncludeSpec<User>(x => x.UserDetails, x => x.Subscribers)));
             if (user == null)
             {
                 return Response.AsRedirect("/notfound");
             }
            var posts = _postService.GetUserPosts(userId, page);
            var model = new UserInfoModel();
            model.FirstName = user.UserDetails.Firstname;
            model.LastName = user.UserDetails.Lastname;
            model.UserName = user.Username;
            model.CreatedDate = user.UserDetails.CreatedDate.ToString("yyyy-MM-dd");
            model.Summary = user.UserDetails.Summary;
            model.Image = user.UserDetails.ImagePath;
            model.UserId = user.UserId;
             if (Context.CurrentUser != null)
             {
                 model.NeedSubscribe = true;
             }
            if (this.Context.CurrentUser != null && this.Context.CurrentUser.UserName == user.Username)
            {
                model.IsOwner = true;
                model.NeedSubscribe = false;
            }
            if (model.NeedSubscribe)
            {
                var currentUserName = this.Context.CurrentUser.UserName;
                if (user.Subscribers.Any(x => x.Username == currentUserName))
                {
                    model.IsSubscribed = true;
                }
                else
                {
                    model.IsSubscribed = false;
                }
            }
            model.ShowNext = posts.Count() > 10;
            model.Posts = posts.Select(x => new PostViewModel()
            {
                PostId = x.PostId,
                Title = x.Title,
                CreatedDate = x.CreatedDate.ToString("yyyy-MM-dd"),
                IsOwner = model.IsOwner,
                User = x.User
            }).ToList();

            return View["/User/UserInfo.sshtml", model];
        }
    }
}
