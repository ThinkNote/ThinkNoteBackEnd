using System.IO;
using ThinkNoteBackEnd.DAO;
namespace ThinkNoteBackEnd.Persistence.User
{
    public interface IPersistUserFile:IPersistUserResources
    {

    }
    public class PersistUserSyncFile : IPersistUserFile
    {
        public readonly string UserSyncPathTemplate;
        public PersistUserSyncFile(string userSyncPath, DbDAOContext context)
        {
            UserSyncPathTemplate = userSyncPath;
        }
        public string ResolveUserPath(string Uid)
        {
            return Path.Combine(UserSyncPathTemplate, Uid);
        }
    }
}