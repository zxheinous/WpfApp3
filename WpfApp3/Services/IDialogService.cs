namespace PhoneBook.Services
{
    // Интерфейс сервиса диалоговых окон
    // ViewModel работает только с интерфейсом,
    // а не с MessageBox напрямую
    public interface IDialogService
    {
        void ShowInfo(
            string message,
            string title = "Информация");

        void ShowWarning(
            string message,
            string title = "Предупреждение");

        void ShowError(
            string message,
            string title = "Ошибка");

        bool ShowConfirmation(
            string message,
            string title = "Подтверждение");
    }
}