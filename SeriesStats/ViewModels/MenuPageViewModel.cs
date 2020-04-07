using Prism.Commands;
using Prism.Navigation;
using SeriesStats.Controls;
using SeriesStats.Core.Models.Trakt.User;
using SeriesStats.Core.Services.Trakt.Abstractions;
using SeriesStats.ViewModels.Base;
using SeriesStats.Views;
using SeriesStats.Views.Auth;
using SeriesStats.Views.Explore;
using SeriesStats.Views.MyShows;
using SeriesStats.Views.Stats;
using System.Collections.Generic;
using System.Windows.Input;

namespace SeriesStats.ViewModels
{
    public class MenuPageViewModel : ViewModelBase
    {
        private readonly ITraktUserService _traktUserService;
        public TraktUser TraktUser { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        public ICommand MenuItemSelectedCommand { get; }
        public ICommand GotoAuthCommand { get; }
        public MenuPageViewModel(INavigationService navigationService,
            ITraktUserService traktUserService) : base(navigationService)
        {
            _traktUserService = traktUserService;
            InitMenuItems();
            MenuItemSelectedCommand = new DelegateCommand<MenuItem>(MenuItemSelected);
            GotoAuthCommand = new DelegateCommand(GotoAuth);
        }

        private void GotoAuth()
        {
            var navParam = new NavigationParameters { { nameof(TraktUser), TraktUser } };
            NavigationService.NavigateAsync(nameof(AuthenticationPage), navParam, true);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            TraktUser = await _traktUserService.GetTraktUser();
        }

        private void MenuItemSelected(MenuItem menuItem)
        {
            NavigationService.NavigateAsync($"{nameof(MyNavigationPage)}/{menuItem.Route}");
        }

        private void InitMenuItems()
        {
            MenuItems = new List<MenuItem>();
            MenuItems.Add(new MenuItem
            {
                Icon = "\U000f01fd",
                Name = "Explore",
                Route = $"{nameof(ExplorePage)}"
            });

            MenuItems.Add(new MenuItem
            {
                Icon = "\uf429",
                Name = "Your shows",
                Route = $"{nameof(MyShowsPage)}"
            });
            MenuItems.Add(new MenuItem
            {
                Icon = "\uf128",
                Name = "Show Stats",
                Route = $"{nameof(ShowStatsPage)}"
            });
            MenuItems.Add(new MenuItem
            {
                Icon = "\uf493",
                Name = "Settings",
                Route = $"{nameof(SettingsPage)}"
            });
        }
    }

    public class MenuItem
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Route { get; set; }
    }
}