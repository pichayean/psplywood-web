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

builder.Services.AddSignalR();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
else
{
    app.UseExceptionHandler("/error/handle-exception");
    app.UseStatusCodePagesWithReExecute("/error/{0}");
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    try
    {
        var _dataService = context.RequestServices.GetRequiredService<IPSPlywoodService>();
        var setting = await _dataService.GetSettingsAsync();
        var contact = await _dataService.GetContactUsAsync();
        var siteVIsit = await _dataService.GetSiteVisitCounterAsync();
        var data = new LayoutViewModel
        {
            SiteVisitCounter = siteVIsit,
            Setting = setting,
            Contact = contact
        };
        context.Items["CommonData"] = data;

        // context.Session.SetString("LastAccess", DateTime.Now.ToString());

        // // Check timestamp to determine if the user is still active
        // var lastAccess = DateTime.Parse(HttpContext.Session.GetString("LastAccess"));
        // var timeSinceLastAccess = DateTime.Now - lastAccess;
        // if (timeSinceLastAccess.TotalMinutes < 15) // Consider the user as online within the last 15 minutes
        // {
        //     // User is online
        // }
        await next(context);
    }
    catch (Exception ex)
    {
        // Handle the error within the middleware
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("Internal Server Error: " + ex.Message);
    }
});

// Configure the HTTP request pipeline.
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<OnlineUsersHub>("/onlineUsersHub");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
