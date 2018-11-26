using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ThinkNoteBackEnd.DAO.Helper;
using ThinkNoteBackEnd.DAO.User;
using System.Linq;
namespace ThinkNoteBackEnd.DAO
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new UserContext())
            {
                var found = context.UserLoginInfo.First(x => x.Uid == 119588986812907520);
                Console.WriteLine(found.Username);
            }
        }
    }
}
