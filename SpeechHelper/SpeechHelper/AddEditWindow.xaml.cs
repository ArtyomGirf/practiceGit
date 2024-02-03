using System.Windows;

namespace SpeechHelper
{
    /// <summary>
    /// Логика взаимодействия для AddEditUserWindow.xaml
    /// </summary>
    public partial class AddEditWindow : Window
    {
        public AddEditWindow(User user)
        {
            InitializeComponent();

            addEditPage.Navigate(new AddEditUserPage(user, this));
        }
        public AddEditWindow(Answer answer)
        {
            InitializeComponent();

            addEditPage.Navigate(new AddEditAnswerPage(answer, this));
        }
        public AddEditWindow(Question question)
        {
            InitializeComponent();

            addEditPage.Navigate(new AddEditQuestionPage(question, this));
        }
        public AddEditWindow(Appeal appeal)
        {
            InitializeComponent();

            addEditPage.Navigate(new AddEditAppealPage(appeal, this));
        }
        public AddEditWindow(Client client)
        {
            InitializeComponent();

            addEditPage.Navigate(new AddEditClientPage(client, this));
        }
    }
}