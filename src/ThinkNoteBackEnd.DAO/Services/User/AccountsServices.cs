using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using ThinkNoteBackEnd.DAO.Helper;
using ThinkNoteBackEnd.DAO.User;

namespace ThinkNoteBackEnd.DAO.Actions.User
{
    public interface IAccountsServices
    {
        UserLoginStatus ValidateLoginAccount(string Identifier, string Password);
        bool CheckEmailExists(string CheckEmail);
    }
    public class AccountsServices : IAccountsServices
    {
        public readonly UserContext userContext;
        public readonly IdWorker idWorker;
        public AccountsServices(UserContext user,IdWorker id)
        {
            userContext = user;
            idWorker = id;
        }
        public UserLoginStatus ValidateLoginAccount(string Identifier, string Password)
        {
            var loginvalidator = userContext.UserLoginInfo.FirstOrDefault(user => user.Email == Identifier);
            if (loginvalidator != null)
            {
                if (Password == loginvalidator.Password)
                {
                    return new UserLoginStatus { Message = "Login successful.", Status = 0 };
                }
                return new UserLoginStatus { Message = "Login failed. Wrong password.", Status = 1 };
            }
            return new UserLoginStatus { Message = "Login failed. Account doesn't exist.", Status = 2 };
        }
        public bool CheckEmailExists(string CheckEmail)
        {
            return userContext.UserLoginInfo.Where(x => x.Email == CheckEmail).Count() > 0;
        }
    }
}
