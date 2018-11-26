using System;

namespace ThinkNoteBackEnd.User
{
    public class InvalidSystemClock : Exception
    {      
        public InvalidSystemClock(string message) : base(message) { }
    }
}