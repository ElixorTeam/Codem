using System.Reflection;
using Blazored.LocalStorage;
using Blazored.Toast;
using Codem.Api;
using Codem.Application.AutoMapper;
using Codem.Infrastructure;
using CodeMirror6;
using Mapster;

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
        
        builder.Services.AddScoped<CodeMirrorJsInterop>();
        builder.Services.AddNhibernate();
        builder.Services.AddApi();
        builder.Services.AddAutoMapper(typeof(ApplicationMappings));
        
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