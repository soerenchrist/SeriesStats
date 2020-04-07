using Prism.AppModel;
using Prism.Commands;
using Prism.Navigation;
using SeriesStats.Core.Auth.Abstractions;
using SeriesStats.Core.Models.Trakt.User;
using SeriesStats.Core.Services.Trakt.Abstractions;
using SeriesStats.Core.Util;
using SeriesStats.Helpers;
using SeriesStats.ViewModels.Base;
using System;
using System.Windows.Input;
using Xamarin.Essentials;

namespace SeriesStats.ViewModels.Auth
{
    public class AuthenticationPageViewModel : ViewModelBase, IAutoInitialize
    {
        public bool IsLoggedIn { get; set; }
        public TraktUser TraktUser { get; set; }
        private readonly IAuthenticator _authenticator;
        private readonly ITraktUserService _userService;
        public ICommand LogoutCommand { get; }
        public ICommand LoginCommand { get; }
        public AuthenticationPageViewModel(INavigationService navigationService,
            IAuthenticator authenticator,
            ITraktUserService userService) : base(navigationService)
        {
            _authenticator = authenticator;
            _userService = userService;

            LogoutCommand = new DelegateCommand(Logout);
            LoginCommand = new DelegateCommand(Login);
        }

        private async void Login()
        {

            var redirect = Constants.RedirectUri;
            var url =
                $"https://api.trakt.tv/oauth/authorize?response_type=code&client_id={Secrets.TraktClientId}&redirect_uri={redirect}";
            var result = await WebAuthenticator.AuthenticateAsync(new Uri(url), new Uri(redirect));
            if (!result.Properties.ContainsKey("code"))
            {
                return;
            }
            var authCode = result.Properties["code"];
            var isSuccess = await _authenticator.ExchangeOAuthCode(authCode);
            if (isSuccess)
            {
                IsLoggedIn = true;
                TraktUser = await _userService.GetTraktUser();
            }
        }

        private async void Logout()
        {
            var isSuccessful = await _authenticator.Logout();
            if (isSuccessful)
            {
                IsLoggedIn = false;
                TraktUser = null;
            }
        }


        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            IsLoggedIn = await _authenticator.IsAuthenticated();
        }
    }
}