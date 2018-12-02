using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using ThinkNoteBackEnd.DAO;
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
        public string ValidateLogin([FromForm][Required,EmailAddress] string identifier, [FromForm][Required] string password)
        {
            return JsonConvert.SerializeObject(_accountsServices.ValidateLoginAccount(identifier, password));
        }
        [AllowAnonymous]
        [HttpGet("CheckEmail")]
        public ActionResult CheckUniqueEmail([FromQuery][Required,EmailAddress] string email)
        {
            return Ok(JsonConvert.SerializeObject(new { unique = _accountsServices.CheckEmailExists(email) }));
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult RegisterUserAccount([FromBody] UserRegisterInfo registerInfo)
        {
            return Ok(JsonConvert.SerializeObject(_accountsServices.RegisterAccount(registerInfo)));
        }
        [HttpGet("Profile")]
        [Authorize]
        public ActionResult QueryUserProfile([FromQuery]long Uid)
        {
            var userProf = _accountsServices.GetUserProfile(Uid);
            if(userProf is UserProfileInfo)
            {
                var JsonObj = JObject.FromObject(userProf);
                JsonObj.Remove("U");   //Remove field "U" of UserProfileInfo because U is a mapper described fk.
                return Ok(JsonConvert.SerializeObject(JsonObj));
            }
            return NotFound(JsonConvert.SerializeObject(userProf));
        }
        [HttpPost("Profile/Update")]
        [Authorize]
        public ActionResult UpdateUserProfile([FromBody][Required] UserProfileInfo Profile)
        {
            return Ok(JsonConvert.SerializeObject(_accountsServices.UpdateUserProfile(Profile)));
        }
    }
}
