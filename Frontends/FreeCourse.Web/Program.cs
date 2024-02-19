 using FreeCourse.Shared.Services;
using FreeCourse.Web.Handler;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


var serviceApiSettings = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

builder.Services.AddHttpClient<IUserService, UserService>(opt =>
{
    opt.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUri);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<ICatalogService, CatalogService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();





builder.Services.AddScoped<ClientCredentialTokenHandler>();
builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));//option pattern
builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));
builder.Services.AddHttpContextAccessor();
builder.Services.AddAccessTokenManagement();
builder.Services.AddScoped<ISharedIdentityService , SharedIdentityService>();
builder.Services.AddHttpClient<IIdentityService, IdentityService>();
builder.Services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();
builder.Services.AddControllersWithViews();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
     opts =>
     {
         opts.LoginPath = "/Auth/SignIn";
         opts.ExpireTimeSpan=TimeSpan.FromDays(60);
         opts.SlidingExpiration = true;
         opts.Cookie.Name = "udemywebcookie";
     });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
