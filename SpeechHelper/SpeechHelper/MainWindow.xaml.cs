using System.Windows;

namespace SpeechHelper
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static User user;
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(User user)
        {
            InitializeComponent();
            inputLogin.Text = user.Login;
        }

        private void LoginBtnClick(object sender, RoutedEventArgs e)
        {
            foreach (var user in TechnoGuideEntities.GetContext().User)
            {
                if (user.Login == inputLogin.Text)
                {
                    if (user.Password == inputPassword.Password)
                    {
                        MainWindow.user = user;
                        var workingWindow = new WorkingWindow(MainWindow.user);
                        workingWindow.Show();
                        Close();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль!",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }
            if (user == null)
                MessageBox.Show("Пользователя с таким логином не существует!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ExitBtnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
