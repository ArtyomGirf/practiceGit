using System.Windows;

namespace SpeechHelper
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow(User user)
        {
            InitializeComponent();
            if ((user.RoleID == 1) || (user.RoleID == 3))
            {
                databaseSettings.Visibility = Visibility.Visible;

                if (user.RoleID == 1) 
                {
                    usersSettings.Visibility = Visibility.Visible;
                }
            }
        }
        private void SettingsWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WorkingWindow.SelectStatusForWindow(ref WorkingWindow.OpennedSettings);
        }
        private void MainSettingsClick(object sender, RoutedEventArgs e)
        {
            SettingsFrame.Navigate(new MainSettingsPage());
        }
        private void CloseSettingsBtnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void UsersSettingsClick(object sender, RoutedEventArgs e)
        {
            SettingsFrame.Navigate(new UsersSettingsPage());
        }
        private void DatabaseSettingsClick(object sender, RoutedEventArgs e)
        {
            SettingsFrame.Navigate(new DBSettingsPage());
        }

        private void SaveSettingsBtnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}