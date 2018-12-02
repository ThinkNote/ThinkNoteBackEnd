using System;

namespace ThinkNoteBackEnd.Helper
{
    public class InvalidSystemClock : Exception
    {      
        public InvalidSystemClock(string message) : base(message) { }
    }
}