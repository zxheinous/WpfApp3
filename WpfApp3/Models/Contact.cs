using System.Text.RegularExpressions;
using PhoneBook.ViewModels;

namespace PhoneBook.Models
{
    public class Contact : ObservableObject
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _phone;

        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        public Contact(string name, string phone)
        {
            _name = name;
            _phone = phone;
        }

        public bool Validate()
        {
            string pattern = @"^(\+7|8)?\d{10}$";

            return Regex.IsMatch(Phone, pattern);
        }
    }
}