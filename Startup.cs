using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineStore.AutomapperProfiles;
using OnlineStore.AutoMapperProfiles;
using OnlineStore.DB;
using OnlineStore.Models;
using OnlineStore.Models.IdentityModels;
using OnlineStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnlineStore
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
            services.AddAutoMapper(config =>
            {
                config.AddProfile<TemplateAutoMapperProfile>();
                config.AddProfile<CharacteristicAutoMapperProfile>();
            });

            services.AddDistributedMemoryCache();

            services.AddDbContext<OnlineStoreContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            /////////////////////////////////////////////////////////////////////
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddMemoryCache();
            /////////////////////////////////////////////////////////////////////

            services.AddRazorPages()
                .AddRazorRuntimeCompilation();

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 1;  
                options.Password.RequireNonAlphanumeric = false;  
                options.Password.RequireLowercase = false; 
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            })
                .AddEntityFrameworkStores<OnlineStoreContext>()
                .AddDefaultTokenProviders();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(2); // тестим
                options.Cookie.IsEssential = true;
            });

            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IOrderCategoriesServise, OrderCategoriesServise>();


            services.AddControllersWithViews();
                //.AddJsonOptions(options =>
                //{
                //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                //    options.JsonSerializerOptions.WriteIndented = true;
                //});
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

            app.UseSession(); 

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //   name: "default",
                //   pattern: "ShopCart/AjaxEditOrderDataUser/{userId}",
                //   defaults: new { controller = "ShopCart", action = "AjaxEditOrderDataUser" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"//,
                    /*constraints: new { controller = "$[^s]*" }*/);
            });
        }
    }
}
