namespace ThinkNoteBackEnd.Model
{
    public class BaseStatus
    {
        public BaseStatus()
        {

        }
        public BaseStatus(int status, string message)
        {
            Status = status;
            Message = message;
        }
        public int Status { get; set; }
        public string Message { get; set; }
    }
}