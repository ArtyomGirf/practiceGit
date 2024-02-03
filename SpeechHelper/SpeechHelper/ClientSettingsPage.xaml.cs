using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SpeechHelper
{
    /// <summary>
    /// Логика взаимодействия для ClientSettingsPage.xaml
    /// </summary>
    public partial class ClientSettingsPage : Page
    {
        public ClientSettingsPage()
        {
            InitializeComponent();

            DGridClients.ItemsSource = TechnoGuideEntities.GetContext().Client.ToList();
        }

        private void EditClientClick(object sender, RoutedEventArgs e)
        {
            var client = (sender as Button).DataContext as Client;

            var message = MessageBox.Show("Вы действительно хотите изменить данные клиента " + client.Name  + '?', "Изменение данных клиента", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (message == MessageBoxResult.Yes)
            {
                var editWindow = new AddEditWindow(client);
                editWindow.Show();
            }
            else
                MessageBox.Show("Изменение данных клиента " + client.Name + " было отменено!", "Изменение данных клиента", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteClientClick(object sender, RoutedEventArgs e)
        {
            var client = (sender as Button).DataContext as Client;

            var message = MessageBox.Show("Вы действительно хотите удалить данные клиента " + client.Name + '?', "Удаление данных клиента", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (message == MessageBoxResult.Yes)
            {
                TechnoGuideEntities.GetContext().Client.Remove(client);
                TechnoGuideEntities.GetContext().SaveChanges();
                MessageBox.Show("Удаление данных клиента " + client.Name + " успешно завершено!", "Удаление данных клиента", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Удаление данных клиента " + client.Name + " было отменено!", "Удаление данных клиента", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ReplaceDGridClick(object sender, RoutedEventArgs e)
        {
            DGridClients.ItemsSource = TechnoGuideEntities.GetContext().Client.ToList();
        }

        private void AddClientClick(object sender, RoutedEventArgs e)
        {
            var message = MessageBox.Show("Вы действительно хотите добавить данные клиента?", "Добавление данных клиента", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (message == MessageBoxResult.Yes)
            {
                var addWindow = new AddEditWindow(new Client());
                addWindow.Show();
            }
            else
                MessageBox.Show("Добавление данных клиента было отменено!", "Добавление данных клиента", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
