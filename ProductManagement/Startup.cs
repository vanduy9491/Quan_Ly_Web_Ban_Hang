using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductManagement.DBContexts;
using ProductManagement.Entities;
using ProductManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement
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
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddDbContext<ProductDBContext>(option => option.UseSqlServer(Configuration.GetConnectionString("ProductDBConnect")));
            services.AddIdentity<AppIdentityUser, IdentityRole>().AddEntityFrameworkStores<ProductDBContext>();
            services.AddScoped<ICategoryService, CategoryServices>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddDistributedMemoryCache();
            services.AddSession(cfg =>
            {
                cfg.Cookie.Name = "ShopCongNghe";
                cfg.IdleTimeout = new TimeSpan(0, 30, 0);
            });
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
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseExceptionHandler("/Errors/");
            app.UseAuthorization();
            app.UseStatusCodePagesWithRedirects("/Errors/NotFound");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
