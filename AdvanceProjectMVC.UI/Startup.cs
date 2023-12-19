using AdvanceProjectMVC.ConnectService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvanceProjectMVC.UI
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
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddHttpClient<EmployeeConnectService>(conf =>
            {

                // conf.BaseAddress = new Uri("http://localhost:23586");
                conf.BaseAddress = new Uri(Configuration["myBaseUri"]);

            });
            services.AddHttpClient<TitleConnectService>(conf =>
            {

                // conf.BaseAddress = new Uri("http://localhost:23586");
                conf.BaseAddress = new Uri(Configuration["myBaseUri"]);

            });
            services.AddHttpClient<BusinessUnitConnectService>(conf =>
            {

                // conf.BaseAddress = new Uri("http://localhost:23586");
                conf.BaseAddress = new Uri(Configuration["myBaseUri"]);

            });
            services.AddHttpClient<AdvanceConnectService>(conf =>
            {

                // conf.BaseAddress = new Uri("http://localhost:23586");
                conf.BaseAddress = new Uri(Configuration["myBaseUri"]);

            });
            services.AddHttpClient<ProjectConnectService>(conf =>
            {

                // conf.BaseAddress = new Uri("http://localhost:23586");
                conf.BaseAddress = new Uri(Configuration["myBaseUri"]);

            });

            services.AddAuthentication(a =>
            {
                a.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(a =>
            {
                a.LoginPath = "/Auth/Login";
                a.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
                a.Cookie.HttpOnly = true;
            });
            services.AddAuthorization();
            services.AddSession(a => a.IdleTimeout = TimeSpan.FromMinutes(10));

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
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Auth}/{action=Login}/{id?}");
            });
        }
    }
}
