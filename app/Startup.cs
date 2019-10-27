using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using asp_identity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcMovie.Data;
using MvcMovie.Models;

namespace asp_mvc {

    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            Boolean isProduction = !String.IsNullOrEmpty (Environment.GetEnvironmentVariable ("DB"));

            services.AddDbContext<ApplicationDbContext> (options =>
                options.UseMySql (isProduction ? Environment.GetEnvironmentVariable ("DB") : Configuration.GetConnectionString ("HerokuJawsDB")));
            services.AddDefaultIdentity<IdentityUser> (options => {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 15;
                })
                .AddRoles<IdentityRole> ()
                .AddEntityFrameworkStores<ApplicationDbContext> ();

            services.AddDbContext<MvcMovieContext> (options =>
                options.UseMySql (isProduction ? Environment.GetEnvironmentVariable ("UNOEURO_DB") : Configuration.GetConnectionString ("UnoEuroDb")));

            services.AddControllersWithViews ();
            services.AddRazorPages ();

            string key1;
            string key2;

            if (isProduction) {
                key1 = Environment.GetEnvironmentVariable ("AES_KEY1");
                key2 = Environment.GetEnvironmentVariable ("AES_KEY2");
            } else {
                var keys = Configuration.GetSection ("Passwords");
                key1 = keys.GetSection ("encryptionKey").Value;
                key2 = keys.GetSection ("encryptionIV").Value;
            }
            services.AddSingleton<IGetKeys> (opt => new GetKeys (Configuration, key1, key2));
            services.AddSingleton<IKeys, Keys> ();

            services.AddCors (o => o.AddPolicy ("MyPolicy", builder => {
                builder.AllowAnyOrigin ()
                    .AllowAnyMethod ()
                    .AllowAnyHeader ();
            }));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env, UserManager<IdentityUser> userManager) {

            if (env.IsDevelopment ()) {
                ApplicationDbInitializer.SeedUsers (userManager, Configuration.GetSection ("Passwords").GetSection ("adminpass").Value);
                app.UseDeveloperExceptionPage ();
                app.UseDatabaseErrorPage ();
            } else {
                app.UseExceptionHandler ("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }
            app.UseHttpsRedirection ();
            app.UseStaticFiles ();

            app.UseRouting ();
            app.UseCors("MyPolicy");

            app.UseAuthentication ();
            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllerRoute (
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages ();
            });
        }
    }
}