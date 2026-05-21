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

        public ObservableCollection<Contact> Contacts { get; }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _phone = string.Empty;
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        private Contact? _selectedContact;
        public Contact? SelectedContact
        {
            get => _selectedContact;
            set => Set(ref _selectedContact, value);
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public ContactsListViewModel(IDialogService dialogService, INavigationService navigationService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;

            Contacts = new ObservableCollection<Contact>();
            AddCommand = new RelayCommand(AddContact);
            DeleteCommand = new RelayCommand(DeleteContact);
        }

        private void AddContact()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Phone))
            {
                _dialogService.ShowWarning("Заполните все поля.");
                return;
            }

            if (Contacts.Any(c => c.Phone == Phone))
            {
                _dialogService.ShowWarning("Контакт уже существует.");
                return;
            }

            Contact contact = new Contact(Name, Phone);

            if (!contact.Validate())
            {
                _dialogService.ShowError("Неверный формат телефона.");
                return;
            }

            Contacts.Add(contact);
            _dialogService.ShowInfo("Контакт добавлен.");

            Name = string.Empty;
            Phone = string.Empty;
        }

        private void DeleteContact()
        {
            if (SelectedContact == null)
                return;

            bool result = _dialogService.ShowConfirmation($"Удалить {SelectedContact.Name}?");

            if (result)
            {
                Contacts.Remove(SelectedContact);
                _dialogService.ShowInfo("Контакт удалён.");
            }
        }
    }
}