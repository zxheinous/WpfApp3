using System.Windows.Input;
using PhoneBook.Services;

namespace PhoneBook.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        public INavigationService NavigationService => _navigationService;

        public ICommand ShowContactsCommand { get; }
        public ICommand ShowAboutCommand { get; }

        public MainWindowViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowContactsCommand = new RelayCommand(() => _navigationService.NavigateTo<ContactsListViewModel>());
            ShowAboutCommand = new RelayCommand(() => _navigationService.NavigateTo<AboutViewModel>());

            _navigationService.NavigateTo<ContactsListViewModel>();
        }
    }
}