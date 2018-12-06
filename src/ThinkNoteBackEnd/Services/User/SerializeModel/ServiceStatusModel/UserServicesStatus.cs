using ThinkNoteBackEnd.Model;
namespace ThinkNoteBackEnd.Services.User.Model
{
    public class UserLoginStatus : BaseStatus
    {
        public UserLoginStatus()
        {

        }
        public UserLoginStatus(int status,string message):base(status,message)
        {

        }
    }
    public class UserLoginStatusWithToken : UserLoginStatus
    {
        public object Token { get; set; }
    }
    public static class UserLoginStatusMsg
    {
        public const string USER_LOGIN_SUCCESSFUL = "User login successful";
        public const string USER_LOGIN_FAILED= "User login failed. Email address or password wrong.";
        public const string USER_REGISTER_SUCCESSFUL = "User register successful.";
        public const string USER_REGISTER_DB_UPD_ERROR = "User register failed. Db update error.";
        public const string USER_REGISTER_DB_UPD_CONCURRENCY = "User register failed. Db update error.";
        public const string USER_REGISTER_EMAIL_ADDR_DUPLICATE = "User register failed. Email address is duplicated.";
    }

    public static class UserProfileStatusMsg
    {
        public const string USER_PROFILE_UPDATE_SUCCESSFUL = "User profile updated.";
        public const string USER_PROFILE_DB_UPD_CONCURRENCY = "User register failed. Db update error.";
        public const string USER_PROFILE_DB_UPD_ERROR = "User register failed. Db update error.";

        public const string USER_PROFILE_NOT_FOUND = "Cannot find the specified user profile.";
    }
}
//On Login
//0 successful
//1 用户名密码错误，不暴露具体原因，增加安全性

//On Register
//0 successful
//1 db update error
//2 db update lock detected.

//On Profile
//0 successful
//1 db update error
//2 db update lock detected.
//3 user profile not found.