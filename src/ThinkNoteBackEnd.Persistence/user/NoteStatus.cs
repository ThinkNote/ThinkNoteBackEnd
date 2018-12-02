using System;
using System.Collections.Generic;
using System.Text;

namespace ThinkNoteBackEnd.Persistence
{
    public class NoteStatus
    {
        public int Exist;
        public int SyncStatus;
    }
}


/*

    0  不Exist   1  Exist
    0的情况下SyncStatus = -2
    1的情况下  如果客户端新，服务器旧-1
    如果一样，0
    如果客户端旧，服务器新，1
*/