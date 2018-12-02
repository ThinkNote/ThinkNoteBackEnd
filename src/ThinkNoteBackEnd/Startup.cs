using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ThinkNoteBackEnd.DAO;
using ThinkNoteBackEnd.Helper;
using ThinkNoteBackEnd.Persistence;
using ThinkNoteBackEnd.Persistence.Config;
using ThinkNoteBackEnd.Services.User;

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
            var JWTInfoObj = Configuration.GetSection("JwtSignatureInfo");
            services.Configure<UserJWTTokenModel>(JWTInfoObj);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//配置JWT服务
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       NameClaimType = ClaimTypes.Name,
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidAudience = Configuration["JwtSignatureInfo:Audience"],
                       ValidIssuer = Configuration["JwtSignatureInfo:Issuer"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSignatureInfo:SecurityKey"]))//拿到SecurityKey
                   };
                   options.Events = new JwtBearerEvents
                   {
                       OnAuthenticationFailed = context =>
                       {
                           if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                           {
                               context.Response.Headers.Add("Token-Expired", "true");
                           }
                           return Task.CompletedTask;
                       }
                   };
               });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDirectoryBrowser();
            services.Configure<PersistenceConfigurationModel>(Configuration.GetSection("StaticFilePath"));
            services.Configure<SnowflakeConfigurationModel>(Configuration.GetSection("SnowflakeConfiguration"));
            services.AddDbContext<DbDAOContext>(opt => opt.UseMySQL(Configuration.GetConnectionString("MySQLConnectionString")), ServiceLifetime.Singleton);
            services.AddSingleton<IdWorker>();
            services.AddPersistenceServices(typeof(IPersistenceService));
            services.AddScoped<IAccountsServices, AccountsServices>();
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
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "../..", Configuration["StaticFilePath:RootPath"])),
                RequestPath = "/persistence"
            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "../..", Configuration["StaticFilePath:RootPath"])),
                RequestPath = "/persistence"
            });
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
