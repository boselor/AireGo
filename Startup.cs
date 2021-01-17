using FreeSql;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AireGo
{
    public class Startup
    {
        private IFreeSql _sql;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _sql = new FreeSqlBuilder()
                .UseAutoSyncStructure(true)
                .UseConnectionString(DataType.Sqlite, connectionString: Configuration["App:ConnectStrings:SQLite"])
                .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_sql);
            services.AddAuthentication(authoption =>
            {
                authoption.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                authoption.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                authoption.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                authoption.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
           .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, option =>
           {
               option.Cookie.Name = "AireGo.Identity";
               option.Cookie.HttpOnly = true;
               option.ExpireTimeSpan = TimeSpan.FromDays(3);
               option.SlidingExpiration = true;
               option.LoginPath = "/Identity/Index";
               option.LogoutPath = "/Identity/LoginOut";
               option.AccessDeniedPath = "/Identity/Error";
           });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "defaultArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
