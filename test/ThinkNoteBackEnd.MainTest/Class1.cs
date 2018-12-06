using Microsoft.EntityFrameworkCore;
using ThinkNoteBackEnd.Helper;
using ThinkNoteBackEnd.Services.User;
using Xunit;
using Microsoft.Extensions.Options;
using ThinkNoteBackEnd.DAO;
using ThinkNoteBackEnd.Services.User.Model;
using ThinkNoteBackEnd.Model;

namespace ThinkNoteBackEnd.MainTest
{
    public class Class1
    {
        [Fact]
        public void Test_IdWorker()
        {
            //it shoult generate an id.
            var worker = new IdWorker();
            Assert.True(worker.NextId() > 0);
            Assert.True(worker.NextId().ToString().Length == 18);
        }
        [Fact]
        public void Test_User_Controller()
        {
            //Mock test run env.
            var optionsBuilder = new DbContextOptionsBuilder<DbDAOContext>();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseInMemoryDatabase();
            var FakeDbContext = new DbDAOContext(optionsBuilder.Options);
            // Add sample data
            var FakeData = new UserLoginInfo[] {
                new UserLoginInfo {Uid=1, Email = "fakestudent@scut.edu.cn",Password="12345678"},
                new UserLoginInfo {Uid=2, Email="relaxStudent@sysu.edu.cn",Password="qwertygfdas" }
            };
            FakeDbContext.AddRange(FakeData);
            FakeDbContext.SaveChanges();

            var accountActions = new AccountsServices(FakeDbContext, new IdWorker(1, 1));

            //It should return login success
            var Result1 = accountActions.ValidateLoginAccount("fakestudent@scut.edu.cn", "12345678") as UserLoginStatus;
            Assert.True(Result1.Status == 0,"Status != 0 when pass correct email and password");
            //It should return password wrong
            var Result2 = accountActions.ValidateLoginAccount("fakestudent@scut.edu.cn", "1234567") as UserLoginStatus;
            Assert.True(Result2.Status == 1, "Status != 1 when pass correct email but password is wrong");
            var Result5 = accountActions.ValidateLoginAccount("fakestudent@scut.edu.cn", "") as UserLoginStatus;
            Assert.True(Result5.Status == 1, "Status != 1 when pass correct email but password is wrong");
            //It should return account not exist.
            var Result3 = accountActions.ValidateLoginAccount("student@scut.edu.cn", "1234567") as UserLoginStatus;
            Assert.True(Result3.Status == 1, "Status != 2 when account is not exist.");
            var Result4 = accountActions.ValidateLoginAccount("", "") as UserLoginStatus;
            Assert.True(Result4.Status == 1, "Status != 2 when account is not exist.") ;
            //It should report email address exists.
            Assert.True(accountActions.CheckEmailExists("fakestudent@scut.edu.cn"));
            Assert.True(accountActions.CheckEmailExists("relaxStudent@sysu.edu.cn"));
            Assert.False(accountActions.CheckEmailExists("123456@scut.edu.cn"));
            Assert.False(accountActions.CheckEmailExists(""));


            //Test register
            var NormalRegister = new UserRegisterInfo{
                Username = "SuxiShuaitong",
                Password = "Suxishuaitong Password",
                Email = "Suxishuaitong@qq.com"
            };
            var NormalResult = accountActions.RegisterAccount(NormalRegister);
            Assert.IsType(typeof(BaseStatus),NormalResult);
            Assert.True(NormalResult.Status == 0);

            var DuplicatedResult = accountActions.RegisterAccount(NormalRegister);
            Assert.IsType(typeof(BaseStatus),DuplicatedResult);
            Assert.True(DuplicatedResult.Status == 3);

            //Test Login with new account

            var LoginResult = accountActions.ValidateLoginAccount(NormalRegister.Email,NormalRegister.Password);
            Assert.IsType(typeof(UserLoginStatus),LoginResult);
            Assert.True(LoginResult.Status == 0);
        }
    }
}
