using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkNoteBackEnd.Persistence.Config;

namespace ThinkNoteBackEnd.Persistence.User
{
    public interface IPersistUserResources
    {
        string ResolveUserPath(string Uid);
    }
    public class PersistUserFileServices:IPersistence
    {
        public readonly IPersistUserFile persistUserSyncFile;
        public readonly IPersistUserNotes persistUserNote;
        public PersistUserFileServices(IOptions<PersistenceConfigurationModel> options)
        {
            persistUserNote = new PersistUserNote(options.Value.UserPath);
            persistUserSyncFile = new PersistUserSyncFile(options.Value.UserPath);
        }
    }
}
