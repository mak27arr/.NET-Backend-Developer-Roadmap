using API.Helper.Extention;
using Core.Helper;
using Identity.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterType();
builder.Services.AuthConfig(new Identity.Helper.AuthConfig());

var app = builder.Build();
await SeedDB(app);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


static async Task SeedDB(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        try
        {
            var userservice = services.GetRequiredService<IUserService>();
            await userservice.SetInitialData(null);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger(nameof(Program));
            logger.LogError(ex, "An error occurred seeding the DB.");
        }
    }
}
