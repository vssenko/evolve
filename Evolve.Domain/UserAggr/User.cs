using Evolve.Domain.PostAggr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Domain.UserAggr
{
    public class User
    {
        public int UserId { get; set; }

        public UserCredentials UserCredentials { get; set; }

        public UserDetails UserDetails { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public List<Post> Posts { get; set; }

        public List<Comment> Comments { get; set; }

        public List<Postbox> Postboxes { get; set; }

        public List<User> Subscribers { get; set; }

        public List<User> Subscribtions { get; set; }
    }
}
