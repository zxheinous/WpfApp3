namespace PhoneBook.Services
{
    // Контракт сервиса навигации
    public interface INavigationService
    {
        object? CurrentViewModel { get; }
        void NavigateTo<TViewModel>(object? parameter = null) where TViewModel : class;
    }

    // Интерфейс для поддержки передачи параметров при навигации
    public interface INavigationAware
    {
        void OnNavigatedTo(object? parameter);
    }
}