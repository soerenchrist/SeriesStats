using Prism.Mvvm;
using Prism.Navigation;
using SeriesStats.Util;
using SeriesStats.Util.Abstractions;
using Xamarin.Forms;

namespace SeriesStats.ViewModels.Base
{
    public abstract class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; }
        protected IMessages Messages { get; }
        public bool IsBusy { get; set; }
        public bool IsRefreshing { get; set; }
        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
            Messages = DependencyService.Get<IMessages>();
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public virtual void Destroy()
        {
        }
    }
}