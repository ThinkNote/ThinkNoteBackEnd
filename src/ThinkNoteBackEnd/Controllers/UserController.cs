using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ThinkNoteBackEnd.Common;
using ThinkNoteBackEnd.DAO.Actions.User;
using ThinkNoteBackEnd.Persistence.User;

namespace ThinkNoteBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccountsServices _accountsServices;
        private readonly PersistUserFileServices _persistUserFileServices;

        public UserController(IAccountsServices accountsServices, PersistUserFileServices persistUserFileServices)
        {
            _accountsServices = accountsServices;
            _persistUserFileServices = persistUserFileServices;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult ValidateLogin([FromForm][Required,EmailAddress] string identifier, [FromForm][Required]  string password)
        {
            return Ok(_accountsServices.ValidateLoginAccount(identifier, password));
        }
        [HttpGet("CheckEmail")]
        public ActionResult CheckUniqueEmail([FromQuery][Required,EmailAddress]  string email)
        {
            return Ok(new { unique = _accountsServices.CheckEmailExists(email) });
        }
    }
}
