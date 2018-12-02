using Microsoft.Extensions.Options;
using System.IO;
using System.Linq;
using ThinkNoteBackEnd.DAO;
using ThinkNoteBackEnd.Persistence.Config;

namespace ThinkNoteBackEnd.Persistence.User
{
    public interface IPersistUserResources
    {
        string ResolveUserPath(string Uid);
    }
    public class PersistUserFileServices : IPersistenceService
    {
        public readonly IPersistUserFile persistUserSyncFile;
        public readonly IPersistUserNotes persistUserNote;
        public readonly PersistenceConfigurationModel config;
        public readonly DbDAOContext dbContext;
        public PersistUserFileServices(IOptions<PersistenceConfigurationModel> options,DbDAOContext DbContext)
        {
            config = options.Value;
            var CombineUserPath = Path.Combine(config.RootPath, config.UserPath);
            persistUserNote = new PersistUserNote(CombineUserPath,config.NoteFileExtension,DbContext);
            persistUserSyncFile = new PersistUserSyncFile(CombineUserPath,DbContext);
            dbContext = DbContext;
        }
        public NoteStatus CheckNoteSyncStatus(NoteFileTracker note)
        {
            var NoteTracker = dbContext.NoteFileTracker.SingleOrDefault(x => x.Guid == note.Guid);
            if (NoteTracker == null)
            {
                //说明这个note不存在，可以新建
                return new NoteStatus { Exist = 0, SyncStatus = -2 };
            }
            else
            {
                //read the head of the note and judge if it needs sync with server


                //line below is only for test.
                return new NoteStatus { Exist = 1, SyncStatus = 1 };
            }
        }
    }
}
