using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using ThinkNoteBackEnd.Persistence.Config;

namespace ThinkNoteBackEnd.Persistence.User
{
    public interface IPersistUserResources
    {
        string ResolveUserPath(string Uid);
    }
    public class PersistUserFileServices
    {
        public readonly IPersistUserFile persistUserSyncFile;
        public readonly IPersistUserNotes persistUserNote;
        public PersistUserFileServices(IOptions<PersistenceConfigurationModel> options)
        {
            persistUserSyncFile = new PersistUserSyncFile(options.Value.UserPath);
            persistUserNote = new PersistUserNote(options.Value.UserPath);
        }
    }
}
