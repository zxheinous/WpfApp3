using System.Windows.Input;
using PhoneBook.Services;

namespace PhoneBook.ViewModels
{
    // ViewModel для главного окна (Shell), управляющая глобальным меню
    public class MainWindowViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        public INavigationService NavigationService => _navigationService;

        public ICommand ShowContactsCommand { get; }
        public ICommand ShowAboutCommand { get; }

        public MainWindowViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            // Инициализация команд перехода между экранами
            ShowContactsCommand = new RelayCommand(() => _navigationService.NavigateTo<ContactsListViewModel>());
            ShowAboutCommand = new RelayCommand(() => _navigationService.NavigateTo<AboutViewModel>());

            // Установка стартового экрана по умолчанию
            _navigationService.NavigateTo<ContactsListViewModel>();
        }
    }
}