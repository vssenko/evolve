using Evolve.Domain.PostAggr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Application.Services
{
    public interface IPostService
    {
        Post GetPost(int postId);

        Post CreatePost(string title, string body, string userName);

        List<Post> FindPostsByTitle(string title);

        List<Post> GetUserPosts(int userId, int page);

        List<Post> LoadUserLastSubscriptions(string username);
    }
}
