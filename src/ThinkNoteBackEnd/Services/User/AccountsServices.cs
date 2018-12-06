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
using ThinkNoteBackEnd.Model;
using ThinkNoteBackEnd.Services.User.Model;

namespace ThinkNoteBackEnd.Services.User
{
    public interface IAccountsServices
    {
        UserLoginStatus ValidateLoginAccount(string Identifier, string Password);
        BaseStatus RegisterAccount(UserRegisterInfo registerInfo);
        bool CheckEmailExists(string CheckEmail);
        object GetUserProfile(long Uid);
        BaseStatus UpdateUserProfile(UserProfileInfo ProfileInfo);
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
            var loginvalidator = userContext.UserLoginInfo.FirstOrDefault(user => user.Email == Identifier && user.Password == Password);
            if (loginvalidator != null)
            {
                return new UserLoginStatus { Message = UserLoginStatusMsg.USER_LOGIN_SUCCESSFUL, Status = 0 };
            }
            return new UserLoginStatus { Message = UserLoginStatusMsg.USER_LOGIN_FAILED, Status = 1 };
        }
        public bool CheckEmailExists(string CheckEmail)
        {
            return userContext.UserLoginInfo.Where(x => x.Email == CheckEmail).Count() > 0;
        }
        public BaseStatus RegisterAccount(UserRegisterInfo registerInfo)
        {
            var NewAccountUid = idWorker.NextId();
            var IsEmailAddrExist = CheckEmailExists(registerInfo.Email);
            if (IsEmailAddrExist) return new BaseStatus(3, UserLoginStatusMsg.USER_REGISTER_EMAIL_ADDR_DUPLICATE);
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
                return new BaseStatus(0, UserLoginStatusMsg.USER_REGISTER_SUCCESSFUL);
            }
            catch (DbUpdateConcurrencyException)
            {
                return new BaseStatus(2, UserLoginStatusMsg.USER_REGISTER_DB_UPD_CONCURRENCY);
            }
            catch (DbUpdateException)
            {
                return new BaseStatus(1, UserLoginStatusMsg.USER_REGISTER_DB_UPD_ERROR);
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
        public BaseStatus UpdateUserProfile(UserProfileInfo ProfileInfo)
        {
            try
            {
                var Prof = userContext.UserProfileInfo.SingleOrDefault(x => x.Uid == ProfileInfo.Uid);
                if (Prof != null)
                {
                    Prof.DeepUpdateItem(ProfileInfo);
                    userContext.SaveChanges();
                    return new BaseStatus(0, UserProfileStatusMsg.USER_PROFILE_UPDATE_SUCCESSFUL);
                }
                return new BaseStatus(3, UserProfileStatusMsg.USER_PROFILE_NOT_FOUND);
            }
            catch (DbUpdateConcurrencyException)
            {
                return new BaseStatus(2, UserProfileStatusMsg.USER_PROFILE_DB_UPD_CONCURRENCY);
            }
            catch (DbUpdateException)
            {
                return new BaseStatus(1, UserProfileStatusMsg.USER_PROFILE_DB_UPD_ERROR);
            }
        }
    }
}
