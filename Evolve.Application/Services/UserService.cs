using Evolve.Domain.UserAggr;
using Evolve.Infrastructure;
using Evolve.Infrastructure.DB.EF.LinqSpec;
using Evolve.Infrastructure.DB.EF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Application.Services
{
    public class UserService : IUserService
    {
        IRepository<User> _userRepository;
        IRepository<UserCredentials> _userCredentialsRepository;
        IRepository<UserDetails> _userDetailsRepository;
        IHashProvider _hashProvider;

        public UserService(IRepository<User> userRepository, IRepository<UserCredentials> userCredentialsRepository, IRepository<UserDetails> userDetailsRepository, IHashProvider hashProvider)
        {
            _userRepository = userRepository;
            _userCredentialsRepository = userCredentialsRepository;
            _userDetailsRepository = userDetailsRepository;
            _hashProvider = hashProvider;
        }

        public User GetUser(string email, string password)
        {
            var hash = _hashProvider.GenerateHash(password);
            return _userRepository.GetBySpec(new QueryParams<User>(
                new Specification<User>(x => x.Email.ToLower() == email.ToLower() && x.UserCredentials.PasswordHash == hash),
                new IncludeSpec<User>(x => x.UserCredentials)));
        }

        public User CreateUser(string email, string username, string password)
        {
            var existeduser = _userRepository.GetBySpec(new QueryParams<User>(new Specification<User>(x => x.Username == username.ToLower() || x.Email == email.ToLower())));
            if (existeduser != null)
            {
                if (existeduser.Email == email)
                    throw new EntityExistException("There is already user with such email.");
                else
                {
                    throw new EntityExistException("There is already user with such username.");
                }
            }
            var hashProvider = new HashProvider();
            var newUser = new User()
            {
                Email = email,
                Username = username,
                UserCredentials = new UserCredentials()
                {
                    PasswordHash = hashProvider.GenerateHash(password),
                    UniqueNumber = Guid.NewGuid()
                },
                UserDetails = new UserDetails()
                {
                    Firstname ="",
                    Lastname = "",
                    CreatedDate = DateTime.Today,
                    Rating = 0,
                }
            };

            try
            {
                _userRepository.Insert(newUser);
                return newUser;
            }
            catch (Exception e)
            {
                throw new EntityExistException("Failed to insert new user.", e);
            }
        }


        public User ChangeUsername(string currentUserName, string newUserName)
        {
            var user = GetUserByUsername(currentUserName);

            var existingUser = GetUserByUsername(newUserName);
            if (existingUser != null)
            {
                throw new EntityExistException("There is user with such username.");
            }
            user.Username = newUserName;
            return _userRepository.Update(user);
        }

        public User ChangeUserData(string currentUserName, string firstname,string lastname,string imagePath,string summary)
        {
            var user = GetUserByUsername(currentUserName);
            user.UserDetails.Firstname = firstname;
            user.UserDetails.Lastname = lastname;
            user.UserDetails.ImagePath = imagePath;
            user.UserDetails.Summary = summary;
            _userDetailsRepository.Update(user.UserDetails);
            return user;
        }

        public User ChangePassword(string currentUserName, string oldPassword, string newPassword)
        {
            var user = GetUserByUsername(currentUserName);
            if (user.UserCredentials.PasswordHash != _hashProvider.GenerateHash(oldPassword))
            {
                throw new EntityExistException("Password must matches.");
            }
            user.UserCredentials.PasswordHash = _hashProvider.GenerateHash(newPassword);
            _userCredentialsRepository.Update(user.UserCredentials);
            return user;
        }

        public User GetUserByUsername(string userName)
        {
            var user = _userRepository.GetBySpec(new QueryParams<User>(new Specification<User>(x => x.Username == userName),new IncludeSpec<User>(x => x.UserCredentials,x => x.UserDetails)));
            if (user == null)
            {
                throw new EntityExistException("Not found.");
            }
            return user;
        }
    }
}
