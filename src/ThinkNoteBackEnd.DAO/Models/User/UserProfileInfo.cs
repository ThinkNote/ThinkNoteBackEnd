using System;
using System.Collections.Generic;

namespace ThinkNoteBackEnd.DAO
{
    public partial class UserProfileInfo:JsonUserProfileInfo
    {
        public UserLoginInfo U { get; set; }
    }
    public class JsonUserProfileInfo
    {
        public long Uid { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public int? SchoolCode { get; set; }
        public string Signature { get; set; }
        public byte? Sex { get; set; }
        public DateTime? Birthday { get; set; }
        public string SchoolId { get; set; }
        public string RealName { get; set; }
        public string Email { get; set; }
        public int UserType { get; set; }

    }
    public static class UserProfileInfoExtension
    {
        public static UserProfileInfo DeepUpdateItem(this UserProfileInfo self,UserProfileInfo update)
        {
            self.Username = update.Username;
            self.Phone = update.Phone;
            self.SchoolCode = update.SchoolCode;
            self.Signature = update.Signature;
            self.Sex = update.Sex;
            self.Birthday = update.Birthday;
            self.SchoolId = update.SchoolId;
            self.RealName = update.RealName;
            self.Email = update.Email;
            self.UserType = update.UserType;
            return self;
        }
    }
}
