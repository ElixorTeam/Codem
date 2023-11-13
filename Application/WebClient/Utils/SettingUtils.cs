using System.Reflection;
using WebClient.Models;

namespace WebClient.Utils;

public static class SettingUtils
{
    public static AuthSettingsModel LoadJsonConfig()
    {
        IConfigurationRoot sqlConfiguration = new ConfigurationBuilder()
            .AddJsonFile("sqlconfig.json", optional: false, reloadOnChange: false)
            .Build();
        
        AuthSettingsModel authSettings = new();
        sqlConfiguration.GetSection("AuthSettings").Bind(authSettings);
        return authSettings;
    }
}