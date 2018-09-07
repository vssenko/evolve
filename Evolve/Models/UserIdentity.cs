using Evolve.Domain.UserAggr;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Models
{
    class UserIdentity : IUserIdentity
    {
        public UserIdentity()
        {
        }

        public IEnumerable<string> Claims
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }
    }
}
