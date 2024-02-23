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

var _userVisit = new List<UserOlnineViewModel>();
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

        string name = context?.Request?.Cookies["BrowserIdentifier"];
        if (name != null)
        {
            var ls = _userVisit.Where(_ => _.exep < DateTime.Now);
            foreach (var item in ls)
            {
                _userVisit.Remove(item);
            }

            var current = _userVisit.FirstOrDefault(_ => _.name == name);
            if (current == null)
            {
                _userVisit.Add(new UserOlnineViewModel { exep = DateTime.Now.AddMinutes(5), name = name });
            }
        }
        var siteVIsit = await _dataService.GetSiteVisitCounterAsync();
        var data = new LayoutViewModel
        {
            SiteVisitCounter = siteVIsit,
            Setting = setting,
            UserOnlineCnt = _userVisit.Count,
            Contact = contact
        };
        context.Items["CommonData"] = data;
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
