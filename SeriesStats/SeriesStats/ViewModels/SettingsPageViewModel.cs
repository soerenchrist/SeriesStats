using Prism.Navigation;
using SeriesStats.Util;
using SeriesStats.ViewModels.Base;
using System.Collections.ObjectModel;

namespace SeriesStats.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        public ObservableCollection<string> ThemeOptions { get; } = new ObservableCollection<string>();

        public SettingsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            ThemeOptions.Add("Light");
            ThemeOptions.Add("Dark");
            if (Settings.HasDefaultThemeOption)
                ThemeOptions.Insert(0, "Device Default");


            if (ThemeOptions.Count == 3)
                SelectedTheme = (int)Settings.ThemeOption;
            else
                SelectedTheme = (int)Settings.ThemeOption - 1;
        }

        private int _selectedTheme;
        public int SelectedTheme {
            get => _selectedTheme;
            set {
                if (value < 0) return;

                if (SetProperty(ref _selectedTheme, value))
                {
                    if (ThemeOptions.Count == 3)
                        Settings.ThemeOption = (Theme)value;
                    else
                        Settings.ThemeOption = (Theme)(value + 1);

                    ThemeHelper.ChangeTheme(Settings.ThemeOption, true);
                }
            }
        }
    }
}