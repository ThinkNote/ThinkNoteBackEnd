using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThinkNoteBackEnd.DAO.Actions.User;
using ThinkNoteBackEnd.Persistence.User;

namespace ThinkNoteBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccountsAction _accountsAction;
        private readonly PersistUserFileServices _persistUserFileServices;
        public UserController(IAccountsAction accountsAction, PersistUserFileServices persistUserFileServices)
        {
            _accountsAction = accountsAction;
            _persistUserFileServices = persistUserFileServices;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult ValidateLogin([FromForm] string identifier, [FromForm] string password)
        {
            return Ok(_accountsAction.ValidateLoginAccount(identifier, password));
        }
        [HttpGet("CheckEmail")]
        public ActionResult CheckUniqueEmail([FromQuery] string email)
        {
            var c = _accountsAction.CheckEmailExists(email);
            return Ok(new { unique = c });
        }
        [HttpGet("testPersistence")]
        public ActionResult PersistTest([FromQuery] string Uid)
        {
            return Ok(_persistUserFileServices.persistUserSyncFile.SayFoo(Uid));
        }
    }
}
