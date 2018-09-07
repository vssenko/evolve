using Evolve.Domain.UserAggr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolve.Domain.PostAggr
{
    public class Comment
    {
        public int CommentId { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }

        public string Body { get; set; }

        public int? RelatedCommentId { get; set; }

        public Comment RelatedComment { get; set; }

        public List<Comment> Answers { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
