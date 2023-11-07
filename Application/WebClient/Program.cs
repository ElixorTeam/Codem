using Blazored.LocalStorage;
using Blazored.Toast;
using CodeMirror6;
namespace WebClient;


public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddTransient<CodeMirrorJsInterop>();
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddBlazoredToast();

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
