using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ThinkNoteBackEnd.DAO.Actions.User;
using ThinkNoteBackEnd.DAO.Helper;
using ThinkNoteBackEnd.DAO.User;
using Xunit;

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
            var FakeServiceCollection = new ServiceCollection();
            var optionsBuilder = new DbContextOptionsBuilder<UserContext>();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseInMemoryDatabase();
            var FakeDbContext = new UserContext(optionsBuilder.Options);
            // Add sample data
            var FakeData = new UserLoginInfo[] {
                new UserLoginInfo {Uid=1, Email = "fakestudent@scut.edu.cn",Password="12345678"},
                new UserLoginInfo {Uid=2, Email="relaxStudent@sysu.edu.cn",Password="qwertygfdas" }
            };
            FakeDbContext.AddRange(FakeData);
            FakeDbContext.SaveChanges();
            FakeServiceCollection.AddSingleton(_ => FakeDbContext);
            FakeServiceCollection.AddSingleton(new IdWorker(1, 1));
            var accountActions = new AccountsAction(FakeServiceCollection.BuildServiceProvider());

            //It should return login success
            var Result1 = accountActions.ValidateLoginAccount("fakestudent@scut.edu.cn", "12345678");
            Assert.True(Result1.Status == 0,"Status != 0 when pass correct email and password");
            //It should return password wrong
            var Result2 = accountActions.ValidateLoginAccount("fakestudent@scut.edu.cn", "1234567");
            Assert.True(Result2.Status == 1, "Status != 1 when pass correct email but password is wrong");
            var Result5 = accountActions.ValidateLoginAccount("fakestudent@scut.edu.cn", "");
            Assert.True(Result5.Status == 1, "Status != 1 when pass correct email but password is wrong");
            //It should return account not exist.
            var Result3 = accountActions.ValidateLoginAccount("student@scut.edu.cn", "1234567");
            Assert.True(Result3.Status == 2, "Status != 2 when account is not exist.");
            var Result4 = accountActions.ValidateLoginAccount("", "");
            Assert.True(Result4.Status == 2, "Status != 2 when account is not exist.");
            //It should report email address exists.
            Assert.True(accountActions.CheckEmailExists("fakestudent@scut.edu.cn"));
            Assert.True(accountActions.CheckEmailExists("relaxStudent@sysu.edu.cn"));
            Assert.False(accountActions.CheckEmailExists("123456@scut.edu.cn"));
            Assert.False(accountActions.CheckEmailExists(""));
        }
        
    }
}
