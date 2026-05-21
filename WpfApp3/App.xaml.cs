using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Services;
using PhoneBook.ViewModels;
using System;
using System.Windows;

namespace PhoneBook
{
    public partial class App : Application
    {
        protected override void OnStartup(
            StartupEventArgs e)
        {
            base.OnStartup(e);

            // Создание контейнера DI
            ServiceCollection services =
                new ServiceCollection();

            // Singleton:
            // один экземпляр на всё приложение
            services.AddSingleton<IDialogService,
                                  DialogService>();

            // Transient:
            // новый экземпляр при запросе
            services.AddTransient<MainViewModel>();

            // Главное окно
            services.AddSingleton<MainWindow>(sp =>
            {
                MainWindow window =
                    new MainWindow();

                // Передача ViewModel через DI
                window.DataContext =
                    sp.GetRequiredService<MainViewModel>();

                return window;
            });

            // Создание ServiceProvider
            ServiceProvider provider =
                services.BuildServiceProvider();

            // Запуск главного окна
            MainWindow mainWindow =
                provider.GetRequiredService<MainWindow>();

            mainWindow.Show();
        }
    }
}