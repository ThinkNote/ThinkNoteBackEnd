using System;
using System.Collections.Generic;

namespace ThinkNoteBackEnd.DAO
{
    public partial class UserJoinGroups
    {
        public int Id { get; set; }
        public long JoinUserUid { get; set; }
        public int JoinGroupId { get; set; }

        public UserLoginInfo JoinUserU { get; set; }
    }
}
