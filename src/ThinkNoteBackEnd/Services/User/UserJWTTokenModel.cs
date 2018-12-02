namespace ThinkNoteBackEnd.Services.User
{
    public class UserJWTTokenModel
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecurityKey { get; set; }

        public int ExpireSpan { get; set; }
    }
}
