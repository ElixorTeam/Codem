using System.Reflection;
using Auth0.AspNetCore.Authentication;
using Blazored.LocalStorage;
using Blazored.Toast;
using Codem.Api;
using CodeMirror6;
using Mapster;
using WebClient.Common;
using WebClient.Models;
using WebClient.Services;
using WebClient.Utils;

namespace WebClient;


public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddBlazoredToast();
        
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        
        #region Local

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<CodeMirrorJsInterop>();
        builder.Services.AddApi();

        #endregion
        
        #region Codem

        AuthSettingsModel authSettings = SettingUtils.LoadJsonConfig();
        builder.Services.AddAuth0WebAppAuthentication(options => {
            options.Domain = authSettings.Domain;
            options.ClientId = authSettings.ClientId;
            options.ClientSecret = authSettings.ClientSecret;
            options.Scope = "openid profile email picture";
        }).WithAccessToken(options => { ;
            options.UseRefreshTokens = true;
        });
        
        #endregion
        
        WebApplication app = builder.Build();
        
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        
        
        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}