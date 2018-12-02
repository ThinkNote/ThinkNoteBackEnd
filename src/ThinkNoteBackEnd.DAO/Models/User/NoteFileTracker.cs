using System;
using System.Collections.Generic;

namespace ThinkNoteBackEnd.DAO
{ 
    public partial class NoteFileTracker
    {
        public int Id { get; set; }
        public long? OwnerUid { get; set; }
        public int Visibility { get; set; }
        public string Guid { get; set; }
        public string FileName { get; set; }
        public int GenerateType { get; set; }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            if(obj is NoteFileTracker)
            {
                var c = obj as NoteFileTracker;
                if (Id == c.Id &&
                    OwnerUid == c.OwnerUid &&
                    Visibility == c.Visibility &&
                    Guid == c.Guid &&
                    GenerateType == c.GenerateType) return true;
                
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
