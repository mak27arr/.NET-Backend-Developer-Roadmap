using ASPNetCoreMVC.Areas.Admin.Models.User;
using ASPNetCoreMVC.Data;
using ASPNetCoreMVC.Data.Indentity;
using ASPNetCoreMVC.Filters.ResourceFilter;
using ASPNetCoreMVC.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ASPNetCoreMVC;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console());

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
        })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

        builder.Services.AddControllersWithViews(options =>
        {
            options.Filters.Add(typeof(SimpleResourceFilter));
        });
        builder.Services.AddMvc();
        builder.Services.AddScoped<LoggerResourceFilter>();
        var app = builder.Build();
        await SeedDB(app);

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<TokenMiddleware>();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=RoleManager}/{action=Index}/{id?}"
            );
        });
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapFallbackToController(
            action: "Index",
            controller: "Home");
        app.MapRazorPages();
        app.Run();
    }

    private static async Task SeedDB(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await ContextSeed.SeedRolesAsync(userManager, roleManager);
                await ContextSeed.SeedSuperAdminAsync(userManager, roleManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger(nameof(Program));
                logger.LogError(ex, "An error occurred seeding the DB.");
            }
        }
    }
}