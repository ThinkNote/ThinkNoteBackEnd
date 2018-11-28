using System;
using ThinkNoteBackEnd.DAO.Helper;

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
