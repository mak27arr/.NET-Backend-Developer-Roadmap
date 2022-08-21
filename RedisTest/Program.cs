using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using RedisTest.Data;

var builder = WebApplication.CreateBuilder(args);
var conf = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false)
        .Build();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddStackExchangeRedisCache(o => {
    o.Configuration = conf.GetConnectionString("Redis");
    o.InstanceName = "RedisTest_";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
