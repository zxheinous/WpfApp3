using System.Windows.Controls; // Важно: UserControl находится здесь

namespace PhoneBook.Views
{
    // Обязательно должен быть : UserControl
    public partial class ContactsListView : UserControl
    {
        public ContactsListView()
        {
            InitializeComponent();
        }
    }
}