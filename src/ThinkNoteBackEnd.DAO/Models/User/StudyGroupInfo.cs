using System;
using System.Collections.Generic;

namespace ThinkNoteBackEnd.DAO.User
{
    public partial class StudyGroupInfo
    {
        public int Id { get; set; }
        public string Groupname { get; set; }
        public long CreatorUid { get; set; }
    }
}
