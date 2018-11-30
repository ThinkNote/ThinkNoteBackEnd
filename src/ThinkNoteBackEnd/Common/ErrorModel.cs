using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThinkNoteBackEnd.Common
{
    public class ErrorModel
    {
        public int Status;
        public string Message;
        public ErrorModel(string message,int status)
        {
            Status = status;
            Message = message;
        }
    }
}
