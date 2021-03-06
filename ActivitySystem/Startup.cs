using ActivitySystem.Common;
using ActivitySystem.Models;
using ActivitySystem.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem
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

            services.AddDbContext<AppDbContext>(options =>
                  options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<IOrganizerRepository, OrganizerRepository>();
            services.AddScoped<IActivityImageRepository, ActivityImageRepository>();
            services.AddScoped<IEnrollRepository, EnrollRepository>();
            services.AddTransient<IEmailSender, EmailSender>(i => new EmailSender(
                   Configuration["EmailSenderService:Host"],
                   Configuration.GetValue<int>("EmailSenderService:Port"),
                   Configuration.GetValue<bool>("EmailSenderService:IsEnableSSL"),
                   Configuration["EmailSenderService:UserName"],
                   Configuration["EmailSenderService:Password"]
               ));

            services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultUI()
        .AddDefaultTokenProviders();

            services.AddHttpContextAccessor();
            services.AddRouting();
            services.AddControllersWithViews();
            services.AddRazorPages();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{activityId?}");

                endpoints.MapControllerRoute(       //?? asp-area ?i?H???? URI ???D????
                    name: "mvcAreaRoute",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
