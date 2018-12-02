using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ThinkNoteBackEnd.Persistence.User;
using ThinkNoteBackEnd.Services.User;

namespace ThinkNoteBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccountsServices _accountsServices;

        public UserController(IAccountsServices accountsServices)
        {
            _accountsServices = accountsServices;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult ValidateLogin([FromForm][Required,EmailAddress] string identifier, [FromForm][Required] string password)
        {
            return Ok(_accountsServices.ValidateLoginAccount(identifier, password));
        }
        [AllowAnonymous]
        [HttpGet("CheckEmail")]
        public ActionResult CheckUniqueEmail([FromQuery][Required,EmailAddress]  string email)
        {
            return Ok(new { unique = _accountsServices.CheckEmailExists(email) });
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult RegisterUserAccount([FromBody] UserRegisterInfo registerInfo)
        {
            return Ok(_accountsServices.RegisterAccount(registerInfo));
        }
        [HttpGet("protect")]
        [Authorize]
        public ActionResult TestProtectedInterface()
        {
            return Ok("Now it's accessing protected api with jwt.");
        }
    }
}
