using System.Text.RegularExpressions;
using PhoneBook.ViewModels;

namespace PhoneBook.Models
{
    public class Contact : ObservableObject
    {
        private string _name = string.Empty;
        private string _phone = string.Empty;

        public Contact(string name, string phone)
        {
            _name = name;
            _phone = phone;

            if (!Validate())
            {
                throw new ArgumentException("Некорректные данные контакта");
            }
        }

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return false;

            string pattern = @"^(\+7|8)?\d{10}$";

            return Regex.IsMatch(Phone, pattern);
        }
    }
}