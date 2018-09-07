using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Domain.PostAggr
{
    public class PostBody
    {
        public int PostId { get; set; }

        public string Body { get; set; }

        public Post Post { get; set; }
    }
}
