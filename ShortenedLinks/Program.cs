using ShortenedLinks.Data;

namespace ShortenedLinks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            DbInitializer.Initialize(connectionString);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Маршрут для коротких URL
            app.MapControllerRoute(
                name: "shortUrlRedirect",
                pattern: "{shortUrl}",  // Ловит любые URL вида your-site.com/abc123
                defaults: new { controller = "Home", action = "RedirectUrl" },
                constraints: new { shortUrl = @"^[a-zA-Z0-9]{6}$" });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");            

            app.Run();
        }
    }
}
