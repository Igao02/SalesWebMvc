using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWebMVC.Data;
using SalesWebMVC.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace SalesWebMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<SalesWebMVCContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SalesWebMVCContext") ?? throw new InvalidOperationException("Connection string 'SalesWebMVCContext' not found.")));


            //Inje��o de Dependencias.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<SeedingService>();
            builder.Services.AddScoped<SellerService>();
            builder.Services.AddScoped<DepartmentService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingService>().Seed();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

            var enUS = new CultureInfo("en-US");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = new List<CultureInfo> { enUS },
                SupportedUICultures = new List<CultureInfo> { enUS }
            };

            app.UseRequestLocalization(localizationOptions);
        }
    }
}