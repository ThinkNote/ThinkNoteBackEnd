﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ThinkNoteBackEnd.DAO;
using ThinkNoteBackEnd.Persistence;
using ThinkNoteBackEnd.Persistence.User;

namespace ThinkNoteBackEnd.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class PersistenceController : ControllerBase
    {
        private readonly PersistUserFileServices _user;
        public PersistenceController(PersistUserFileServices persistUserFileServices)
        {
            _user = persistUserFileServices;
        }
        [HttpGet("query/note")]
        [Authorize]
        public ActionResult CheckNoteSyncInfo([FromForm][Required] NoteFileTracker NoteInfoFromClient)
        {
            return Ok(_user.CheckNoteSyncStatus(NoteInfoFromClient));
        }
        [HttpPost("upload/note")]
        [Authorize]
        public ActionResult UploadNoteToServer([FromForm][Required] NoteFileTracker NoteInfo,[FromForm][Required]List<IFormFile> NoteFile)
        {
            int result = _user.persistUserNote.SaveUserNoteFileAsync(NoteInfo, NoteFile).Result;
            return Ok(result);
        }
        [HttpPost("download/note")]
        [Authorize]
        public ActionResult ProvideNoteToClient([FromForm][Required] NoteFileTracker NoteInfo)
        {
            var QueryNoteFileStatus = _user.persistUserNote.ProvideUserNoteFile(NoteInfo);
            switch (QueryNoteFileStatus.Status)
            {
                case 0:
                    return File(QueryNoteFileStatus.Stream,"application/think-note",NoteInfo.FileName+_user.config.NoteFileExtension);
                case 2:
                    return NotFound(new {Status = QueryNoteFileStatus.Status,Message = NoteProviderStatusMsg.FILE_NOT_FOUND});
                case 1:
                    return BadRequest(new {Status = QueryNoteFileStatus.Status,Message = NoteProviderStatusMsg.FILE_IO_EXCEPTION});
            }
            return NotFound();
        }
    }
}
