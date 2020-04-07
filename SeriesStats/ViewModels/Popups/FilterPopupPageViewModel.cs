using Prism.Commands;
using Prism.Navigation;
using SeriesStats.Core.Models.Core;
using SeriesStats.Core.Util;
using SeriesStats.ViewModels.Base;
using System;
using System.Windows.Input;
using Xamarin.Essentials;

namespace SeriesStats.ViewModels.Popups
{
    public class FilterPopupPageViewModel : ViewModelBase
    {
        public bool IsSort { get; set; } = false;
        public bool IsFilter { get; set; } = true;
        public SortOptions SortOptions { get; set; } = SortOptions.ByLastViewed;
        public FilterOptions FilterOptions { get; set; }

        public ICommand CurrentPageChangedCommand { get; }
        public FilterPopupPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            CurrentPageChangedCommand = new DelegateCommand(ChangePage);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var sortKey = Preferences.Get(PreferenceKeys.SortKey, SortOptions.ByLastViewed.ToString());
            var options = (SortOptions)Enum.Parse(typeof(SortOptions), sortKey);
            var filterKey = Preferences.Get(PreferenceKeys.FilterKey, "");
            FilterOptions = FilterOptions.FromString(filterKey);
            SortOptions = options;
        }

        private void ChangePage()
        {
            IsSort = !IsSort;
            IsFilter = !IsFilter;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            Preferences.Set(PreferenceKeys.SortKey, SortOptions.ToString());
            Preferences.Set(PreferenceKeys.FilterKey, FilterOptions.ToString());
        }
    }
}