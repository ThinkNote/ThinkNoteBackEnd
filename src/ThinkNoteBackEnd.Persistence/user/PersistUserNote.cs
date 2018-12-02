using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ThinkNoteBackEnd.DAO;

namespace ThinkNoteBackEnd.Persistence.User
{
    public interface IPersistUserNotes : IPersistUserResources
    {
        Task<int> SaveUserNoteFileAsync(NoteFileTracker NoteInfo, List<IFormFile> FList);
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
        public async Task<int> SaveUserNoteFileAsync(NoteFileTracker NoteInfo, List<IFormFile> FList)
        {
            var path = Path.Combine(ResolveUserPath(NoteInfo.OwnerUid.ToString()), NoteInfo.Guid + NoteFileExtension);
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
    }
}