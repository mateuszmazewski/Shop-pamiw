using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Infrastructure.Repositories;
using System.Globalization;

namespace Shop.WebApp
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
            CultureInfo[] cultures = {
            new CultureInfo("pl-PL"),
            new CultureInfo("en")
        };
            // Set decimal separator as dot globally
            // In order to fix using decimal values in ASP.NET Core MVC data binding
            cultures[0].NumberFormat = cultures[1].NumberFormat;
            CultureInfo.DefaultThreadCurrentCulture = cultures[0];

            services.Configure<RequestLocalizationOptions>(opt =>
            {
                opt.DefaultRequestCulture = new RequestCulture(cultures[0]);
                opt.SupportedCultures = cultures;
                opt.SupportedUICultures = cultures;
            });

            services.AddControllersWithViews();

            services.AddProgressiveWebApp();

            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("ShopConnectionString")
                    ));

            // Mechanizm SignInManager
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            // Logowanie wyj¹tków
            services.AddControllersWithViews(p => p.Filters.Add(new LogFilterAttribute()));
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

            app.UseAuthentication(); // Uwierzytelnianie - mechanizm Identity

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
