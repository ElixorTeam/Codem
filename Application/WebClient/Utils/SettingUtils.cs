using System.Reflection;
using WebClient.Models;

namespace WebClient.Utils;

public static class SettingUtils
{
    public static AuthSettingsModel LoadJsonConfig()
    {
        IConfigurationRoot sqlConfiguration = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? string.Empty)
            .AddJsonFile("sqlconfig.json", optional: false, reloadOnChange: false)
            .Build();
        
        AuthSettingsModel authSettings = new();
        sqlConfiguration.GetSection("AuthSettings").Bind(authSettings);
        return authSettings;
    }
}