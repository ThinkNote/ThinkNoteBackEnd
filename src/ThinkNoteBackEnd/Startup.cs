using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThinkNoteBackEnd.DAO.Actions.User;
using ThinkNoteBackEnd.DAO.Helper;
using ThinkNoteBackEnd.DAO.User;
using ThinkNoteBackEnd.Persistence;
using ThinkNoteBackEnd.Persistence.Config;

namespace ThinkNoteBackEnd
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<PersistenceConfigurationModel>(Configuration.GetSection("StatiicFilePath"));
            var workerId = Configuration["SnowflakeConfiguration:WorkerId"];
            var datacenterId = Configuration["SnowflakeConfiguration:DatacenterId"];
            services.AddDbContext<UserContext>(opt => opt.UseMySQL(Configuration.GetConnectionString("MySQLConnectionString")));
            services.AddSingleton(new IdWorker(int.Parse(workerId), int.Parse(datacenterId)));
            services.AddSingleton<IAccountsAction, AccountsAction>(factory => new AccountsAction(factory.CreateScope().ServiceProvider));
            //Add persistence services.
            services.AddPersistenceServices();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
