using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ThinkNoteBackEnd.DAO.Helper;
using ThinkNoteBackEnd.DAO.User;
using System.Linq;
using System.Data.Common;

namespace ThinkNoteBackEnd.DAO
{
    class Program
    {
        public static void Main(string[] args)
        {
            var worker = new IdWorker(1,1);
            Console.WriteLine(worker.NextId());
        }
    }
}
