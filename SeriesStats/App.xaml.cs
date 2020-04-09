using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using Prism.Ioc;
using Prism.Plugin.Popups;
using Prism.Unity;
using SeriesStats.Controls;
using SeriesStats.Core.Auth;
using SeriesStats.Core.Auth.Abstractions;
using SeriesStats.Core.Repository;
using SeriesStats.Core.Repository.Abstractions;
using SeriesStats.Core.Services.Core;
using SeriesStats.Core.Services.Core.Abstractions;
using SeriesStats.Core.Services.MovieDb;
using SeriesStats.Core.Services.MovieDb.Abstractions;
using SeriesStats.Core.Services.Trakt;
using SeriesStats.Core.Services.Trakt.Abstractions;
using SeriesStats.Core.Util;
using SeriesStats.Core.Util.Abstractions;
using SeriesStats.Respository;
using SeriesStats.Util;
using SeriesStats.Util.Abstractions;
using SeriesStats.Views;
using SeriesStats.Views.Explore;
using Unity;

namespace SeriesStats
{
    [AutoRegisterForNavigation]
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            containerRegistry.Register<IApiConfiguration, ApiConfiguration>();
            containerRegistry.Register<ILocationProvider, LocationProvider>();
            containerRegistry.Register<IAuthenticator, Authenticator>();
            containerRegistry.RegisterSingleton<ITokenRepository, LiteDbTokenRepository>();
            containerRegistry.Register<IShowService, ShowService>();
            containerRegistry.Register<IMovieService, MovieService>();
            containerRegistry.Register<IGenreService, GenreService>();
            containerRegistry.Register<ITraktShowService, TraktShowService>();
            containerRegistry.Register<ITraktStatsService, TraktStatsService>();
            containerRegistry.RegisterSingleton<ICachedHttpHelper, CachedHttpHelper>();
            containerRegistry.Register<IImageService, ImageService>();
            containerRegistry.Register<ITraktUserService, TraktUserService>();
            containerRegistry.Register<ITraktCheckinService, TraktCheckinService>();
            containerRegistry.Register<IConnectivityService, ConnectivityService>();
            containerRegistry.Register<IShowFilterer, ShowFilterer>();
            containerRegistry.Register<IExternalOpener, ExternalOpener>();

            containerRegistry.RegisterForNavigation<MyNavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterPopupNavigationService();
            AppContainer = containerRegistry.GetContainer();
        }

        public static IUnityContainer AppContainer { get; set; }

        protected override void OnInitialized()
        {
            InitializeComponent();
            ThemeHelper.ChangeTheme(Settings.ThemeOption, true);
            AppCenter.Start("android=6731aeed-94a5-4291-81f3-adc025a0dc5b;",
                typeof(Analytics), typeof(Crashes), typeof(Distribute));
            NavigationService.NavigateAsync($"{nameof(MainPage)}/{nameof(MyNavigationPage)}/{nameof(ExplorePage)}");
        }

        protected override void OnResume()
        {
            base.OnResume();

            ThemeHelper.ChangeTheme(Settings.ThemeOption, true);
        }
    }
}
