using Сodem.Shared.Enums;

namespace Сodem.Shared.Utils;

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
            return BuildConfigurationEnum.Local;
#endif
        }
    }
    
    public static bool IsDevelop => Config is BuildConfigurationEnum.Develop or BuildConfigurationEnum.Local;

    public static bool IsRelease => Config is BuildConfigurationEnum.Release;
}