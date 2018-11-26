using System;
using System.Collections.Generic;

namespace ThinkNoteBackEnd.DAO.User
{
    public partial class UserLoginInfo
    {
        public long Uid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public UserProfileInfo UserProfileInfo { get; set; }
    }
}
