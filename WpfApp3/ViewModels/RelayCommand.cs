using System.Collections.ObjectModel;
using System.Windows.Input;
using PhoneBook.Models;

namespace PhoneBook.ViewModels
{
    public class MainViewModel : ObservableObject
    {
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

        public MainViewModel()
        {
            Contacts = new ObservableCollection<Contact>();

            AddCommand = new RelayCommand(
                AddContact,
                CanAddContact);

            DeleteCommand = new RelayCommand(
                DeleteContact,
                CanDeleteContact);
        }

        private void AddContact()
        {
            Contact contact = new Contact(Name, Phone);

            Contacts.Add(contact);

            Name = string.Empty;
            Phone = string.Empty;
        }

        private bool CanAddContact()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return false;

            if (string.IsNullOrWhiteSpace(Phone))
                return false;

            return true;
        }

        private void DeleteContact()
        {
            if (SelectedContact != null)
            {
                Contacts.Remove(SelectedContact);
            }
        }

        private bool CanDeleteContact()
        {
            return SelectedContact != null;
        }
    }
}