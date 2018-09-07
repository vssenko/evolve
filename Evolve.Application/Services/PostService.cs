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
    public class PostService : IPostService
    {
        IRepository<Post> _postRepository;

        IRepository<User> _userRepository;

        public PostService(IRepository<Post> postRepository, IRepository<User> userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public Post GetPost(int postId)
        {
            return _postRepository.GetBySpec(
                new QueryParams<Post>(new Specification<Post>(x => x.PostId == postId),new IncludeSpec<Post>(x => x.User, x => x.PostBody,x => x.Comments.Select(y => y.User.UserDetails))));
        }

        public Post CreatePost(string title, string body,string userName)
        {
            var user = _userRepository.GetBySpec(new QueryParams<User>(new Specification<User>(x => x.Username == userName)));
            if (user == null)
                throw new EntityExistException("Can't find user");
            var post = new Post()
            {
                PostBody = new PostBody() { Body = body },
                Title = title,
                CreatedDate = DateTime.Now,
                User = user
            };
            return _postRepository.Insert(post);
        }

        public List<Post> FindPostsByTitle(string title)
        {
            return _postRepository.GetList(
                new QueryParams<Post>(new Specification<Post>(x => x.Title.Contains(title))));

        }
    
        public List<Post> GetUserPosts(int userId, int page)
        {
            return _postRepository.GetList(new QueryParams<Post>(new Specification<Post>(x => x.UserId == userId),new Pagination<Post>(x => (int) x.PostId, page - 1,1,10)));
        }


        public List<Post> LoadUserLastSubscriptions(string username)
        {
            return _postRepository.GetList(new QueryParams<Post>(new Specification<Post>(x => x.User.Subscribers.Any(y => y.Username == username)),new IncludeSpec<Post>(x => x.User))).OrderBy(x => x.CreatedDate).ToList();
        }
    }
}
