using System;
using System.Linq;
using System.Windows.Input;
using PhoneBook.Models;
using PhoneBook.Services;

namespace PhoneBook.ViewModels
{
    public class ContactEditViewModel : ObservableObject, INavigationAware
    {
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private readonly PhoneBookDbContext _dbContext;

        private Contact? _editableContact;
        private bool _isEditMode;

        private string _nameInput = string.Empty;
        public string NameInput
        {
            get => _nameInput;
            set => Set(ref _nameInput, value);
        }

        private string _phoneInput = string.Empty;
        public string PhoneInput
        {
            get => _phoneInput;
            set => Set(ref _phoneInput, value);
        }

        private string _titleText = "Добавление контакта";
        public string TitleText
        {
            get => _titleText;
            set => Set(ref _titleText, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ContactEditViewModel(IDialogService dialogService, INavigationService navigationService, PhoneBookDbContext dbContext)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            _dbContext = dbContext;

            SaveCommand = new RelayCommand(SaveChanges);
            CancelCommand = new RelayCommand(() => _navigationService.NavigateTo<ContactsListViewModel>());
        }

        public void OnNavigatedTo(object? parameter)
        {
            if (parameter is Contact contact)
            {
                _editableContact = contact;
                _isEditMode = true;
                TitleText = "Редактирование контакта";
                NameInput = contact.Name;
                PhoneInput = contact.Phone;
            }
            else
            {
                _editableContact = null;
                _isEditMode = false;
                TitleText = "Добавление нового контакта";
                NameInput = string.Empty;
                PhoneInput = string.Empty;
            }
        }

        private void SaveChanges()
        {
            if (string.IsNullOrWhiteSpace(NameInput) || string.IsNullOrWhiteSpace(PhoneInput))
            {
                _dialogService.ShowWarning("Заполните все текстовые поля.");
                return;
            }

            try
            {
                if (_isEditMode && _editableContact != null)
                {
                    if (_dbContext.Contacts.Any(c => c.Phone == PhoneInput && c.Id != _editableContact.Id))
                    {
                        _dialogService.ShowWarning("Контакт с таким номером телефона уже существует.");
                        return;
                    }

                    _editableContact.Name = NameInput;
                    _editableContact.Phone = PhoneInput;

                    if (!_editableContact.Validate())
                    {
                        _dialogService.ShowError("Неверный формат телефонного номера.");
                        return;
                    }

                    _dbContext.SaveChanges();
                    _dialogService.ShowInfo("Изменения успешно сохранены.");
                }
                else
                {
                    if (_dbContext.Contacts.Any(c => c.Phone == PhoneInput))
                    {
                        _dialogService.ShowWarning("Контакт с таким номером телефона уже существует.");
                        return;
                    }

                    Contact newContact = new Contact { Name = NameInput, Phone = PhoneInput };
                    if (!newContact.Validate())
                    {
                        _dialogService.ShowError("Неверный формат телефонного номера.");
                        return;
                    }

                    _dbContext.Contacts.Add(newContact);

                    _dbContext.SaveChanges();
                    _dialogService.ShowInfo("Новый контакт успешно создан.");
                }

                _navigationService.NavigateTo<ContactsListViewModel>();
            }
            catch (Exception ex)
            {
                _dialogService.ShowError($"Ошибка сохранения изменений в базу данных: {ex.Message}");
            }
        }
    }
}