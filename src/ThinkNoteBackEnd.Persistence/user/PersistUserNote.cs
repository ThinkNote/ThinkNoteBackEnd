using System.IO;
namespace ThinkNoteBackEnd.Persistence.User
{
    public interface IPersistUserNotes
    {

    }
    public class PersistUserNote : IPersistUserNotes, IPersistUserResources
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