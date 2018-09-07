using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Domain.UserAggr
{
    public class UserCredentials
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public string PasswordHash { get; set; }

        public Guid UniqueNumber { get; set; }
    }
}
