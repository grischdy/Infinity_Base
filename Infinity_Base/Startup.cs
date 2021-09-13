using Infinity_Base.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Azure;
using Azure.Storage.Queues;
using Azure.Storage.Blobs;
using Azure.Core.Extensions;

namespace Infinity_Base
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(Configuration["DefaultEndpointsProtocol=https;AccountName=infinitybaseblobstorage;AccountKey=4kVd/hwjx603AJVT9GpmuxgM5eJQr76cRwFBUVbooS6+37WqJU22T1H6LUA/u6VSRHYL42KPvWRm9AjwAEM7nQ==;EndpointSuffix=core.windows.net:blob"], preferMsi: true);
                builder.AddQueueServiceClient(Configuration["DefaultEndpointsProtocol=https;AccountName=infinitybaseblobstorage;AccountKey=4kVd/hwjx603AJVT9GpmuxgM5eJQr76cRwFBUVbooS6+37WqJU22T1H6LUA/u6VSRHYL42KPvWRm9AjwAEM7nQ==;EndpointSuffix=core.windows.net:queue"], preferMsi: true);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });


            CreateRole(serviceProvider).Wait();
            CreateDefaultUser(serviceProvider).Wait();

        }

        public async Task CreateRole(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            var adminRoleExists = await RoleManager.RoleExistsAsync("Admin");
            if (adminRoleExists == false)
            {
                await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }
        }

        public async Task CreateDefaultUser(IServiceProvider serviceProvider)
        {
            var UserManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            var adminUser = await UserManager.FindByNameAsync("admin@pa-ats.com");
            if (adminUser == null)
            {
                var user = new IdentityUser()
                {
                    Email = "admin@pa-ats.com",
                    UserName = "admin@pa-ats.com",
                };

                await UserManager.CreateAsync(user, "Admin1!");
                adminUser = await UserManager.FindByNameAsync("admin@pa-ats.com");

            }

            await UserManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
    internal static class StartupExtensions
    {
        public static IAzureClientBuilder<BlobServiceClient, BlobClientOptions> AddBlobServiceClient(this AzureClientFactoryBuilder builder, string serviceUriOrConnectionString, bool preferMsi)
        {
            if (preferMsi && Uri.TryCreate(serviceUriOrConnectionString, UriKind.Absolute, out Uri serviceUri))
            {
                return builder.AddBlobServiceClient(serviceUri);
            }
            else
            {
                return builder.AddBlobServiceClient(serviceUriOrConnectionString);
            }
        }
        public static IAzureClientBuilder<QueueServiceClient, QueueClientOptions> AddQueueServiceClient(this AzureClientFactoryBuilder builder, string serviceUriOrConnectionString, bool preferMsi)
        {
            if (preferMsi && Uri.TryCreate(serviceUriOrConnectionString, UriKind.Absolute, out Uri serviceUri))
            {
                return builder.AddQueueServiceClient(serviceUri);
            }
            else
            {
                return builder.AddQueueServiceClient(serviceUriOrConnectionString);
            }
        }
    }
}
