using Evolve.Domain.PostAggr;
using Evolve.Domain.UserAggr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Models
{
    public class PostViewModel
    {
        public int PostId { get; set; }

        public int? PostboxId { get; set; }

        public Postbox Postbox { get; set; }

        public List<Comment> Comments { get; set; }

        public string Title { get; set; }

        public string CreatedDate { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public PostBody PostBody { get; set; }

        public bool IsOwner { get; set; }
    }
}
