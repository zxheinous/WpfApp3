using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Models;
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

            services.AddDbContext<PhoneBookDbContext>(options =>
                options.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=PhoneBookDB_ИВАНОВ_2307А1;Integrated Security=True;TrustServerCertificate=True"));

            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddTransient<ContactsListViewModel>();
            services.AddTransient<AboutViewModel>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddTransient<ContactsListViewModel>();
            services.AddTransient<AboutViewModel>();
            services.AddTransient<ContactEditViewModel>();
            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton<MainWindow>(sp =>
            {
                MainWindow window = new MainWindow();
                window.DataContext = sp.GetRequiredService<MainWindowViewModel>();
                return window;
            });

            ServiceProvider provider = services.BuildServiceProvider();

            MainWindow mainWindow = provider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}