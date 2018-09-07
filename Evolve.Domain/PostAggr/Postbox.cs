using Evolve.Domain.UserAggr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Domain.PostAggr
{
    public class Postbox
    {
        public int PostboxId { get; set; }

        public List<Post> Posts { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
