using Globomantics.Repositories;
using Globomantics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IConferenceRepository, ConferenceRepository>();
builder.Services.AddScoped<IProposalRepository, ProposalRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //o.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;           
})
    .AddCookie()
    .AddCookie(ExternalAuthenticationDefaults.AuthenticationScheme)
    //.AddTwitter( ..)
    .AddGoogle(o =>
    {
        o.SignInScheme = 
            ExternalAuthenticationDefaults.AuthenticationScheme;
        //Please get your Google ClientId and Secret here: https://4sh.nl/googleclient
        o.ClientId = "";
        o.ClientSecret = "";
    });

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Conference}/{action=Index}/{id?}");

app.Run();
