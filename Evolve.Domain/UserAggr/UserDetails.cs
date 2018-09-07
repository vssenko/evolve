using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Domain.UserAggr
{
    public class UserDetails
    {
        public int UserId { get; set; }

        public User User { get;set;}

        public string ImagePath { get; set; }

        public string Summary { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Rating { get; set; }
    }
}
