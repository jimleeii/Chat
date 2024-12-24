using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Identity.Web;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointDefinitions(typeof(IEndpointDefinition));
builder.Services.ConfigureHttpJsonOptions(options => options.SerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals);

// Add services to the container.
builder.Services.AddRazorPages();

// Add Authentication.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
	.EnableTokenAcquisitionToCallDownstreamApi()
	.AddInMemoryTokenCaches();

builder.Services.Configure<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme, options => options.LoginPath = "/Account/Login");

builder.Services.AddAuthorization();

builder.Services.AddCors(options => options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpointDefinitions();
app.MapRazorPages();

app.UseCors("AllowAll");

//app.MapGet("/", () => "Hello Chat!");

await app.RunAsync();