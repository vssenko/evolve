using Evolve.Domain.UserAggr;
using Evolve.Infrastructure.DB.EF.LinqSpec;
using Evolve.Infrastructure.DB.EF.Repository;
using Nancy.Authentication.Forms;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Models
{
    public class UserMapper : IUserMapper
    {
        IRepository<User> _userRepository;

        public UserMapper(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public IUserIdentity GetUserFromIdentifier(Guid identifier, Nancy.NancyContext context)
        {
            var user = _userRepository.GetBySpec(new QueryParams<User>(new Specification<User>(x => x.UserCredentials.UniqueNumber == identifier )));
            if (user == null)
                return null;
            return new UserIdentity()
            {
                UserName = user.Username,
                Claims = new List<string>()
            };
        }
    }
}
