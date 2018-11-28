using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThinkNoteBackEnd.DAO.Actions.User;

namespace ThinkNoteBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccountsAction _accountsAction;
        public UserController(IAccountsAction accountsAction)
        {
            _accountsAction = accountsAction;
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
    }
}
