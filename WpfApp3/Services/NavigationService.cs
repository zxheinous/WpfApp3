using System;
using PhoneBook.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace PhoneBook.Services
{
    // Реализация сервиса навигации, управляющая свойством CurrentViewModel
    public class NavigationService : ObservableObject, INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private object? _currentViewModel;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object? CurrentViewModel
        {
            get => _currentViewModel;
            private set => Set(ref _currentViewModel, value);
        }

        public void NavigateTo<TViewModel>(object? parameter = null) where TViewModel : class
        {
            // Получаем нужную ViewModel из DI-контейнера
            var vm = _serviceProvider.GetRequiredService<TViewModel>();

            // Если ViewModel поддерживает прием параметров, передаем их
            if (vm is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedTo(parameter);
            }

            // Обновляем текущую модель, вызывая OnPropertyChanged
            CurrentViewModel = vm;
        }
    }
}