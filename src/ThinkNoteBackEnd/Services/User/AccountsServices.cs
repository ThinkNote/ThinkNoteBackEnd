using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ThinkNoteBackEnd.DAO;
using ThinkNoteBackEnd.Helper;

namespace ThinkNoteBackEnd.Services.User
{
    public interface IAccountsServices
    {
        UserLoginStatus ValidateLoginAccount(string Identifier, string Password);
        UserLoginStatus RegisterAccount(UserRegisterInfo registerInfo);
        bool CheckEmailExists(string CheckEmail);
        object GetUserProfile(long Uid);
        UserLoginStatus UpdateUserProfile(UserProfileInfo ProfileInfo);
    }
    public class AccountsServices : IAccountsServices
    {
        public readonly DbDAOContext userContext;
        public readonly IdWorker idWorker;
        public readonly UserJWTTokenModel _JwtInfo;
        public AccountsServices(DbDAOContext user, IdWorker id, IOptions<UserJWTTokenModel> Settings = null)
        {
            userContext = user;
            idWorker = id;
            _JwtInfo = Settings?.Value;
        }
        public UserLoginStatus ValidateLoginAccount(string Identifier, string Password)
        {
            var loginvalidator = userContext.UserLoginInfo.FirstOrDefault(user => user.Email == Identifier);
            if (loginvalidator != null)
            {
                if (Password == loginvalidator.Password)
                {
                    if (_JwtInfo != null)
                    {
                        var AccessTokenObject = GenerateTokenObject(loginvalidator);
                        return new UserLoginStatusWithToken { Message = UserLoginStatusMsg.USER_LOGIN_SUCCESSFUL, Status = 0, Token = AccessTokenObject };
                    }
                    return new UserLoginStatus { Message = UserLoginStatusMsg.USER_LOGIN_SUCCESSFUL, Status = 0 };
                }
                return new UserLoginStatus { Message = UserLoginStatusMsg.USER_LOGIN_WRONG_PASSWD, Status = 1 };
            }
            return new UserLoginStatus { Message = UserLoginStatusMsg.USER_LOGIN_ACCOUNT_NOT_EXIST, Status = 2 };
        }
        public bool CheckEmailExists(string CheckEmail)
        {
            return userContext.UserLoginInfo.Where(x => x.Email == CheckEmail).Count() > 0;
        }
        public UserLoginStatus RegisterAccount(UserRegisterInfo registerInfo)
        {
            var NewAccountUid = idWorker.NextId();
            var IsEmailAddrExist = CheckEmailExists(registerInfo.Email);
            if (IsEmailAddrExist) return new UserLoginStatus(3, UserLoginStatusMsg.USER_REGISTER_EMAIL_ADDR_DUPLICATE);
            try
            {
                userContext.UserLoginInfo.Add(new UserLoginInfo
                {
                    Uid = NewAccountUid,
                    Username = registerInfo.Username,
                    Password = registerInfo.Password,
                    Email = registerInfo.Email
                });
                userContext.SaveChanges();
                return new UserLoginStatus(0, UserLoginStatusMsg.USER_REGISTER_SUCCESSFUL);
            }
            catch (DbUpdateConcurrencyException)
            {
                return new UserLoginStatus(2, UserLoginStatusMsg.USER_REGISTER_DB_UPD_CONCURRENCY);
            }
            catch (DbUpdateException)
            {
                return new UserLoginStatus(1, UserLoginStatusMsg.USER_REGISTER_DB_UPD_ERROR);
            }
        }
        public object GenerateTokenObject(UserLoginInfo LoginInfo)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, LoginInfo.UserType.ToString()) };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtInfo.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var ExpireTime = DateTime.Now.AddSeconds(_JwtInfo.ExpireSpan);
            var token = new JwtSecurityToken(
                issuer: _JwtInfo.Issuer,
                audience: _JwtInfo.Audience,
                claims: claims,
                expires: ExpireTime,
                signingCredentials: creds);
            return new { token = new JwtSecurityTokenHandler().WriteToken(token), expire = ExpireTime.ToString(new CultureInfo("zh-cn")) };
        }
        public object GetUserProfile(long Uid)
        {
            var Profile = userContext.UserProfileInfo.FirstOrDefaultAsync(x => x.Uid == Uid).Result;
            if (Profile == null) return new UserLoginStatus(3, UserProfileStatusMsg.USER_PROFILE_NOT_FOUND);
            return Profile;
        }
        public UserLoginStatus UpdateUserProfile(UserProfileInfo ProfileInfo)
        {
            try
            {
                var Prof = userContext.UserProfileInfo.SingleOrDefault(x => x.Uid == ProfileInfo.Uid);
                if (Prof != null)
                {
                    Prof.DeepUpdateItem(ProfileInfo);
                    userContext.SaveChanges();
                    return new UserLoginStatus(0, UserProfileStatusMsg.USER_PROFILE_UPDATE_SUCCESSFUL);
                }
                return new UserLoginStatus(3, UserProfileStatusMsg.USER_PROFILE_NOT_FOUND);
            }
            catch (DbUpdateConcurrencyException)
            {
                return new UserLoginStatus(2, UserProfileStatusMsg.USER_PROFILE_DB_UPD_CONCURRENCY);
            }
            catch (DbUpdateException)
            {
                return new UserLoginStatus(1, UserProfileStatusMsg.USER_PROFILE_DB_UPD_ERROR);
            }
        }
    }
}
