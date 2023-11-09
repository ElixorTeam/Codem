using Blazored.LocalStorage;
using Blazored.Toast;
using Codem.Api;
using Codem.Infrastructure;
using CodeMirror6;
namespace WebClient;


public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddScoped<CodeMirrorJsInterop>();
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddBlazoredToast();
        builder.Services.AddApi();
        
        #region Local
        
        builder.Services.AddNhibernate();
        builder.Services.AddApi();
        
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

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}