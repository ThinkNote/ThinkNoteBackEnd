using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ThinkNoteBackEnd.Persistence.Model
{
    public class NoteStatus
    {
        public int Exist;
        public int SyncStatus;
    }
    public class NoteProviderStatus
    {
        public int Status;
        public FileStream Stream;
    }
    public class NoteProviderStatusMsg
    {
        public const string FILE_NOT_FOUND = "Cannot find the file specified.";
        public const string FILE_IO_EXCEPTION = "An IOException occured while preparing File request";
    }
}


/*

    0  不Exist   1  Exist
    0的情况下SyncStatus = -2
    1的情况下  如果客户端新，服务器旧-1
    如果一样，0
    如果客户端旧，服务器新，1
*/


    /*
     
    NoteProvider
    Status = 0 正常
    1 IO错误
    2 找不到文件
    3 路径错误
     
     */