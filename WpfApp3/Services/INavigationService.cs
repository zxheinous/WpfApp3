namespace PhoneBook.Services
{
    public interface INavigationService
    {
        object? CurrentViewModel { get; }
        void NavigateTo<TViewModel>(object? parameter = null) where TViewModel : class;
    }
    public interface INavigationAware
    {
        void OnNavigatedTo(object? parameter);
    }
}