using Duende.Bff;
using Duende.Bff.AccessTokenManagement;
using Duende.Bff.Yarp;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Duende.Bff.DynamicFrontends;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddBff(o => o.ManagementBasePath = "/account")
    .ConfigureOpenIdConnect(options =>
     {
         options.Authority = "https://localhost:5000";

         options.ClientId = "globomantics_web";
         //Store in application secrets
         options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
         options.Scope.Clear();
         options.Scope.Add("openid");
         options.Scope.Add("profile");
         options.Scope.Add("email");
         options.Scope.Add("globomantics");
         options.Scope.Add("globomanticsapi");
         options.SaveTokens = true;
         options.ResponseType = "code";
         options.GetClaimsFromUserInfoEndpoint = true;

         options.ClaimActions.MapAll();
     })
    .ConfigureCookies(options =>
    {
        options.Cookie.Name = "__Host-spa";
        options.Cookie.SameSite = SameSiteMode.Strict;
    })
    .AddRemoteApis()
    .AddServerSideSessions();

builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = BffAuthenticationSchemes.BffCookie;
    o.DefaultChallengeScheme = BffAuthenticationSchemes.BffOpenIdConnect;
    o.DefaultSignOutScheme = BffAuthenticationSchemes.BffOpenIdConnect;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();
app.UseBff();
app.UseAuthorization();

app.MapRazorPages();
app.MapDefaultControllerRoute();
app.MapBffManagementEndpoints();
app.MapRemoteBffApiEndpoint("/api", new Uri("https://localhost:5002"))
        .WithAccessToken();

app.MapFallbackToFile("index.html");

app.Run();
