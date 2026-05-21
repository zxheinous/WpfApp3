using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Services;
using PhoneBook.ViewModels;
using System;
using System.Windows;

namespace PhoneBook
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ServiceCollection services = new ServiceCollection();

            // Регистрация инфраструктурных сервисов (Lifetimes)
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<INavigationService, NavigationService>();

            // Регистрация экранных модулей (ViewModels)
            services.AddTransient<ContactsListViewModel>(); // Пересоздается при открытии заново
            services.AddTransient<AboutViewModel>();
            services.AddSingleton<MainWindowViewModel>(); // Shell ViewModel живет постоянно

            // Регистрация главного окна
            services.AddSingleton<MainWindow>(sp =>
            {
                MainWindow window = new MainWindow();
                window.DataContext = sp.GetRequiredService<MainWindowViewModel>();
                return window;
            });

            ServiceProvider provider = services.BuildServiceProvider();

            // Ручной запуск окна Shell из контейнера зависимостей
            MainWindow mainWindow = provider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}