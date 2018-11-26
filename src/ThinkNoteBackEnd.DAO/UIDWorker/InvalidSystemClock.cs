using System;

namespace ThinkNoteBackEnd.DAO.Helper
{
    public class InvalidSystemClock : Exception
    {      
        public InvalidSystemClock(string message) : base(message) { }
    }
}