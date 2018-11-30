using System.IO;
namespace ThinkNoteBackEnd.Persistence.User
{
    public interface IPersistUserNotes:IPersistUserResources
    {

    }
    public class PersistUserNote : IPersistUserNotes
    {
        public readonly string UserNotePathTemplate;
        public PersistUserNote(string userNotePath)
        {
            UserNotePathTemplate = userNotePath;
        }
        public string ResolveUserPath(string Uid)
        {
            return Path.Combine(UserNotePathTemplate, Uid);
        }
    }
}