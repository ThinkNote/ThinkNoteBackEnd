using System;
using System.Collections.Generic;

namespace ThinkNoteBackEnd.DAO.User
{
    public partial class UserProfileInfo
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

        public UserLoginInfo U { get; set; }
    }
}
