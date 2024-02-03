using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SpeechHelper
{
    /// <summary>
    /// Логика взаимодействия для UsersSettingsPage.xaml
    /// </summary>
    public partial class UsersSettingsPage : Page
    {
        public UsersSettingsPage()
        {
            InitializeComponent();
        }
        private void ReloadUsers()
        {
            DGridUsers.ItemsSource = TechnoGuideEntities.GetContext().User.ToList();
        }
        private void EditUserButtonClick(object sender, RoutedEventArgs e)
        {
            var message = MessageBox.Show("Вы действительно хотите внести изменнения?", "Изменение данных пользователя", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (message == MessageBoxResult.Yes)
            {
                var editWindow = new AddEditWindow((sender as Button).DataContext as User);
                editWindow.Show();
            }
            else
            {
                MessageBox.Show("Внесение изменений отменено!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void AddUserClick(object sender, RoutedEventArgs e)
        {
            var userWindow = new AddEditWindow(new User());
            userWindow.Show();
        }
        public void ReplaceDGridClick(object sender, RoutedEventArgs e)
        {
            ReloadUsers();
        }
        private void DeleteUserButtonClick(object sender, RoutedEventArgs e)
        {
            var user = (sender as Button).DataContext as User;

            var message = MessageBox.Show("Вы действительно хотите удалить пользователя " + user.Surename + " " + user.Name + "?", "Удаление пользователя", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (message == MessageBoxResult.Yes)
            {
                TechnoGuideEntities.GetContext().User.Remove(user);
                TechnoGuideEntities.GetContext().SaveChanges();
                MessageBox.Show("Пользователь " + user.Surename + " " + user.Name + " удален.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                ReloadUsers();
            }
            else
            {
                MessageBox.Show("Удаление пользователя отменено!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void PageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                TechnoGuideEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                ReloadUsers();
            }
        }
    }
}