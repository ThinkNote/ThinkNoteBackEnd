using System;
using System.Collections.Generic;

namespace ThinkNoteBackEnd.DAO
{
    public partial class StudyGroupInfo
    {
        public int Id { get; set; }
        public string Groupname { get; set; }
        public long CreatorUid { get; set; }
    }
}
