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

        public ObservableCollection<Contact> Contacts { get; set; }

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

        public ContactsListViewModel(IDialogService dialogService, INavigationService navigationService, PhoneBookDbContext dbContext)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            _dbContext = dbContext;
            Contacts = new ObservableCollection<Contact>(_dbContext.Contacts.ToList());

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

            if (_dbContext.Contacts.Any(c => c.Phone == Phone))
            {
                _dialogService.ShowWarning("Контакт с таким номером уже существует.");
                return;
            }

            Contact contact = new Contact { Name = Name, Phone = Phone };

            if (!contact.Validate())
            {
                _dialogService.ShowError("Неверный формат телефона.");
                return;
            }

            _dbContext.Contacts.Add(contact);
            _dbContext.SaveChanges();

            Contacts.Add(contact);
            _dialogService.ShowInfo("Контакт успешно сохранен в базу данных.");

            Name = string.Empty;
            Phone = string.Empty;
        }

        private void DeleteContact()
        {
            if (SelectedContact == null)
                return;

            bool result = _dialogService.ShowConfirmation($"Удалить {SelectedContact.Name} из базы данных?");

            if (result)
            {
                _dbContext.Contacts.Remove(SelectedContact);
                _dbContext.SaveChanges();

                Contacts.Remove(SelectedContact);
                _dialogService.ShowInfo("Контакт удалён из базы данных.");
            }
        }
    }
}