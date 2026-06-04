using System.Windows.Controls;

namespace PhoneBook.Views
{
    public partial class ContactEditView : UserControl
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public ContactEditView()
        {
            System.Windows.Application.LoadComponent(this, new System.Uri("/PhoneBook;component/Views/ContactEditView.xaml", System.UriKind.Relative));
        }
    }
}