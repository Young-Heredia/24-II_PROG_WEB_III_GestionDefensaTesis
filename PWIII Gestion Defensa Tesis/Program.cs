using Microsoft.EntityFrameworkCore;
using PWIII_Gestion_Defensa_Tesis.Data;

namespace PWIII_Gestion_Defensa_Tesis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<DbtesisContext>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );







            var app = builder.Build();

            
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
