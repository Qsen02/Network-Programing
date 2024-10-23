using FDMC.Models;
using Microsoft.EntityFrameworkCore;

namespace FDMC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<CatContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            using (var scope = app.Services.CreateScope()) 
            {
                using (var context = scope.ServiceProvider.GetRequiredService<CatContext>())
                {
                    context.Database.EnsureCreated();
                }
            }
            app.Run();
        }
    }
}