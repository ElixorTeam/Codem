using SharedCore.Enums;
namespace SharedCore.Utils;

public static class BuildUtil
{
    private static BuildConfigurationEnum Config
    {
        get
        {
#if DEVELOP
            return BuildConfigurationEnum.Develop;
#elif RELEASE
            return BuildConfigurationEnum.Release;
#else
            return BuildConfigurationEnum.Develop;
#endif
        }
    }
    
    public static bool IsDevelop => Config is BuildConfigurationEnum.Develop;

    public static bool IsRelease => Config is BuildConfigurationEnum.Release;
}