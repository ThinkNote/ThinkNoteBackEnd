using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ThinkNoteBackEnd.DAO;
using System.Linq;

namespace ThinkNoteBackEnd.Persistence.User
{
    public interface IPersistUserNotes : IPersistUserResources
    {
        Task<int> SaveUserNoteFileAsync(NoteFileTracker NoteInfo, List<IFormFile> FList);
        NoteProviderStatus ProvideUserNoteFile(NoteFileTracker NoteInfo);
    }
    public class PersistUserNote : IPersistUserNotes
    {
        public readonly string UserNotePathTemplate;
        public readonly string NoteFileExtension;
        public readonly DbDAOContext dbContext;
        public PersistUserNote(string userNotePath, string NoteExt, DbDAOContext context)
        {
            UserNotePathTemplate = userNotePath;
            NoteFileExtension = NoteExt;
            dbContext = context;
        }
        public string ResolveUserPath(string Uid)
        {
            var path = Path.Combine(UserNotePathTemplate, Uid);
            FileInfo file = new FileInfo(path);
            if (!file.Exists) file.Directory.CreateSubdirectory(Uid);
            return path;
        }
        public string GetNotePath(NoteFileTracker NoteInfo) => Path.Combine(ResolveUserPath(NoteInfo.OwnerUid.ToString()), NoteInfo.Guid + NoteFileExtension);
        public async Task<int> SaveUserNoteFileAsync(NoteFileTracker NoteInfo, List<IFormFile> FList)
        {
            var path = GetNotePath(NoteInfo);
            dbContext.NoteFileTracker.Update(NoteInfo);
            var DbResult = dbContext.SaveChangesAsync();
            foreach (var file in FList)
            {
                using (var fs = new FileStream(path,FileMode.Create))
                {
                    file.CopyTo(fs);
                }
            }
            return await DbResult;
        }
        public NoteProviderStatus ProvideUserNoteFile(NoteFileTracker NoteInfo)
        {
            var QueryNoteTracker = dbContext.NoteFileTracker.FirstOrDefault(x => x.Guid == NoteInfo.Guid 
                                                                                && x.OwnerUid == NoteInfo.OwnerUid 
                                                                                && x.Visibility==NoteInfo.Visibility);    
            if(QueryNoteTracker == null) 
            {
                return new NoteProviderStatus { Status = 2 };
            }//cannot find record in database note_file_tracker
            try
            {
                var path = GetNotePath(NoteInfo);
                var fs = new FileStream(path, FileMode.Open);
                return new NoteProviderStatus { Stream = fs, Status = 0 };
            }
            catch (FileNotFoundException)
            {
                return new NoteProviderStatus { Status = 2 };
            }
            catch (IOException)
            {
                return new NoteProviderStatus { Status = 1 };
            }
        }
    }
}