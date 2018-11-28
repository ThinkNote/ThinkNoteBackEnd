using System;
using System.Collections.Generic;

namespace ThinkNoteBackEnd.DAO.User
{
    public partial class NoteFileTracker
    {
        public int Id { get; set; }
        public long? OwnerUid { get; set; }
        public int Visibility { get; set; }
        public string Guid { get; set; }
        public string FileName { get; set; }
        public int GenerateType { get; set; }
    }
}
