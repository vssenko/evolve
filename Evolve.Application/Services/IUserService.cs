
using Evolve.Domain.UserAggr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Application.Services
{
    public interface IUserService
    {
        User GetUser(string email, string password);

        User CreateUser(string username, string email, string password);

        User ChangeUsername(string currentUserName, string NewUserName);

        User ChangePassword(string currentUserName, string oldPassword, string newPassword);

        User ChangeUserData(string currentUserName, string firstname, string lastname, string imagePath, string summary);

        User GetUserByUsername(string userName);
    }
}
