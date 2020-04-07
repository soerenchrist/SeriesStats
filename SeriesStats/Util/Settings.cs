using System;
using Xamarin.Essentials;

namespace SeriesStats.Util
{
    public static class Settings
    {
        public static Theme ThemeOption {
            get => (Theme)Preferences.Get(nameof(ThemeOption), HasDefaultThemeOption ? (int)Theme.Default : (int)Theme.Light);
            set => Preferences.Set(nameof(ThemeOption), (int)value);
        }

        public static bool HasDefaultThemeOption {
            get {
                var minDefaultVersion = new Version(13, 0);
                if (DeviceInfo.Platform == DevicePlatform.UWP)
                    minDefaultVersion = new Version(10, 0, 17763, 1);
                else if (DeviceInfo.Platform == DevicePlatform.Android)
                    minDefaultVersion = new Version(10, 0);

                return DeviceInfo.Version >= minDefaultVersion;
            }
        }
    }
}