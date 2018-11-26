using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ThinkNoteBackEnd.DAO.Helper;
using ThinkNoteBackEnd.DAO.User;

namespace ThinkNoteBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IdWorker _IdWorker;
        private readonly UserContext _UserContext;
        public UserController(IdWorker InjectedWorker,UserContext userContext)
        {
            _IdWorker = InjectedWorker;
            _UserContext = userContext;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult ValidateLogin([FromForm] string identifier, [FromForm] string password)
        {
            Console.WriteLine(identifier);
            var loginvalidator = _UserContext.UserLoginInfo.FirstOrDefault(user => user.Username == identifier || user.Email == identifier);
            if(loginvalidator!=null)
            {
                if (password == loginvalidator.Password)
                {
                    return Ok(new { Message = "登陆成功", ErrorCode = 0 });
                }
                return BadRequest(new { Message = "登陆失败，密码错误", ErrorCode = -1 });
                
            }
            return BadRequest(new { Message = "登陆失败，找不到用户", ErrorCode = -2 });
        }
        [HttpGet("CheckUsername")]
        public ActionResult CheckUniqueUsername([FromQuery] string check)
        {
            var UserNameValidator = _UserContext.UserLoginInfo.FirstOrDefault(user => user.Username == check);
            return UserNameValidator == null ? Ok(new { Unique = 1}) : Ok(new { Unique = 0 });
        }
    }
}
