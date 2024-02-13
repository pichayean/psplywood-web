using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PSPlywoodWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string baseAddress = builder.Configuration.GetValue<string>("BaseAddress");
builder.Services.AddHttpClient<IPSPlywoodService, PSPlywoodHttpClient>(httpClient =>
{
    httpClient.BaseAddress = new Uri(baseAddress);
});

var app = builder.Build();

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
