using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PSPlywoodWeb.Common;
using PSPlywoodWeb.Models;
using PSPlywoodWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();

string baseAddress = builder.Configuration.GetValue<string>("BaseAddress");
builder.Services.AddHttpClient<IPSPlywoodService, PSPlywoodHttpClient>(httpClient =>
{
    httpClient.BaseAddress = new Uri(baseAddress);
});

var app = builder.Build();


app.Use(async (context, next) =>
{
    var _dataService = context.RequestServices.GetRequiredService<IPSPlywoodService>();
    var setting = await _dataService.GetSettingsAsync();
    var contact = await _dataService.GetContactUsAsync();
    var data = new LayoutViewModel
    {
        Setting = setting,
        Contact = contact
    };
    context.Items["CommonData"] = data;
    // Call the next delegate/middleware in the pipeline.
    await next(context);
});

// Configure the HTTP request pipeline.
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
