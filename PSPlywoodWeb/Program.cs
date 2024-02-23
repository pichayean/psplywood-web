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

var _userVisit = new List<UserOlnineViewModel>();
app.Use(async (context, next) =>
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

        var current = _userVisit.FirstOrDefault(_=>_.name == name);
        if (current == null)
        {
            _userVisit.Add(new UserOlnineViewModel { exep = DateTime.Now.AddMinutes(5), name = name });
        }
    }

    var data = new LayoutViewModel
    {
        Setting = setting,
        Contact = contact,
        UserOnlineCnt = _userVisit.Count
    };
    context.Items["CommonData"] = data;
    await next(context);
});

// Configure the HTTP request pipeline.
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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
