using LabWork_pt2.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace LabWork_pt2
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<DataBaseContext>(config =>
            config.UseNpgsql("server=localhost; Port = 5432; Database=postgres; username=postgres; password=LEDsa300"));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie("Cookies", config =>
                {   
                    config.LoginPath = "/Account/Login";
                    config.AccessDeniedPath = "/Home/AccessDenied";
                    
                });
            services.AddScoped<FileService>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "ROLE_ADMIN");
                });
                options.AddPolicy("DefaultUser", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "ROLE_USER");
                });
            });

            services.AddControllersWithViews();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseAuthorization();

        

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }

}
