using System.Configuration;
using GlobalImpact.Data;
using GlobalImpact.Interfaces;
using GlobalImpact.Models;
using GlobalImpact.Services;
using GlobalImpact.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using EmailService = GlobalImpact.Utils.EmailService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    //options.UseSqlServer(builder.Configuration.GetConnectionString("AzureConnection")));
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddSingleton<IHttpContextAccessor>((provider) => new HttpContextAccessor());

// Add the Google Maps API key to the ViewData dictionary
builder.Services.AddScoped<GoogleMapsApiKeyService>((provider) =>
{
    var httpContextAccessor = provider.GetService<IHttpContextAccessor>();
    var actionContext = new ActionContext(httpContextAccessor.HttpContext, new RouteData(), new ActionDescriptor());
    var viewDataDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), actionContext.ModelState);

    viewDataDictionary["GoogleMapsApiKey"] = "AIzaSyC_rB7yqusDIvp90Qeyap_j3NBKLLDYWys";

    return new GoogleMapsApiKeyService(viewDataDictionary);
});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // SignIn settings.
    //options.SignIn.RequireConfirmedAccount = true;
});

//Google Authentication
builder.Services.AddAuthentication().AddGoogle(options =>
{
    options.ClientId = "642040797482-jni3mbf6oi4ucuocnqme1hb5d3bi5g9o.apps.googleusercontent.com";
    options.ClientSecret = "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";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class Program { }