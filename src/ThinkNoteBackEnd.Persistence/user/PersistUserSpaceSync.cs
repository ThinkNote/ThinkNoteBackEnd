using System;
using System.IO;
using ThinkNoteBackEnd.Persistence;
namespace ThinkNoteBackEnd.Persistence.User
{
    public interface IPersistUserFile
    {
        string SayFoo(string Uid);
    }
    public class PersistUserSyncFile : IPersistUserFile,IPersistUserResources
    {
        public readonly string UserSyncPathTemplate;
        public PersistUserSyncFile(string userSyncPath)
        {
            UserSyncPathTemplate = userSyncPath;
        }
        public string ResolveUserPath(string Uid)
        {
            return Path.Combine(UserSyncPathTemplate, Uid);
        }

        public string SayFoo(string Uid)
        {
            return UserSyncPathTemplate+"  "+Uid;
        }
    }
}