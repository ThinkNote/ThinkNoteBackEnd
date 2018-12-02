using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ThinkNoteBackEnd.DAO;
using ThinkNoteBackEnd.Persistence.User;

namespace ThinkNoteBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersistenceController : ControllerBase
    {
        private readonly PersistUserFileServices _persistUserFileServices;
        public PersistenceController(PersistUserFileServices persistUserFileServices)
        {
            _persistUserFileServices = persistUserFileServices;
        }

        [HttpGet("query/note")]
        [Authorize]
        public ActionResult CheckNoteSyncInfo([FromBody]NoteFileTracker NoteInfoFromClient)
        {
            return Ok(_persistUserFileServices.CheckNoteSyncStatus(NoteInfoFromClient));
        }
        [HttpPost("upload/note")]
        [Authorize]
        public ActionResult UploadNoteToServer([FromForm][Required] NoteFileTracker NoteInfo,[FromForm][Required]List<IFormFile> NoteFile)
        {
            int result = _persistUserFileServices.persistUserNote.SaveUserNoteFileAsync(NoteInfo, NoteFile).Result;
            return Ok(result);
        }
    }
}
