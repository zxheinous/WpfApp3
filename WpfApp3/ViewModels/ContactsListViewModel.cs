using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PhoneBook.Models;
using PhoneBook.Services;

namespace PhoneBook.ViewModels
{
    public class ContactsListViewModel : ObservableObject
    {
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private readonly PhoneBookDbContext _dbContext;

        private ObservableCollection<Contact> _contacts;
        public ObservableCollection<Contact> Contacts
        {
            get => _contacts;
            set => Set(ref _contacts, value);
        }

        private Contact? _selectedContact;
        public Contact? SelectedContact
        {
            get => _selectedContact;
            set => Set(ref _selectedContact, value);
        }

        public ICommand GoToAddCommand { get; }
        public ICommand GoToEditCommand { get; }
        public ICommand DeleteCommand { get; }

        public ContactsListViewModel(IDialogService dialogService, INavigationService navigationService, PhoneBookDbContext dbContext)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            _dbContext = dbContext;

            try
            {
                Contacts = new ObservableCollection<Contact>(_dbContext.Contacts.ToList());
            }
            catch (Exception ex)
            {
                _dialogService.ShowError($"Ошибка загрузки данных из БД: {ex.Message}");
                Contacts = new ObservableCollection<Contact>();
            }

            GoToAddCommand = new RelayCommand(() => _navigationService.NavigateTo<ContactEditViewModel>(null));

            GoToEditCommand = new RelayCommand(() =>
            {
                if (SelectedContact != null)
                    _navigationService.NavigateTo<ContactEditViewModel>(SelectedContact);
            });

            DeleteCommand = new RelayCommand(DeleteContact);
        }

        private void DeleteContact()
        {
            if (SelectedContact == null) return;

            bool confirm = _dialogService.ShowConfirmation($"Удалить контакт {SelectedContact.Name} из базы данных?");
            if (!confirm) return;

            try
            {
                _dbContext.Contacts.Remove(SelectedContact);

                _dbContext.SaveChanges();

                Contacts.Remove(SelectedContact);
                _dialogService.ShowInfo("Контакт успешно удален.");
            }
            catch (Exception ex)
            {
                _dialogService.ShowError($"Не удалось удалить запись: {ex.Message}");

                _dbContext.Entry(SelectedContact).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            }
        }
    }
}