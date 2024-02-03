using System;
using System.ComponentModel;
using System.Windows;

namespace SpeechHelper
{
    /// <summary>
    /// Логика взаимодействия для ClientChoise.xaml
    /// </summary>
    public partial class ClientChoiseWindow : Window
    {
        public static Client client;
        public ClientChoiseWindow(Client client)
        {
            InitializeComponent();
            if (client == null)
                client = new Client();
            ClientChoiseWindow.client = client;
        }

        private void NameChecked(object sender, RoutedEventArgs e)
        {
            inputClientName.IsEnabled = true;
            inputClientInn.IsEnabled = false;
            inputClientId.IsEnabled = false;
        }

        private void InnChecked(object sender, RoutedEventArgs e)
        {
            inputClientInn.IsEnabled = true;
            inputClientName.IsEnabled = false;
            inputClientId.IsEnabled = false;
        }

        private void IdChecked(object sender, RoutedEventArgs e)
        {
            inputClientId.IsEnabled = true;
            inputClientName.IsEnabled = false;
            inputClientInn.IsEnabled = false;
        }

        private void СloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SearchClientClick(object sender, RoutedEventArgs e)
        {
            if (checkName.IsChecked == true)
            {
                foreach(var client in TechnoGuideEntities.GetContext().Client)
                {
                    if (inputClientName.Text == client.Name)
                        ClientChoiseWindow.client = client;
                }
            }
            else if (checkInn.IsChecked == true)
            {
                foreach (var client in TechnoGuideEntities.GetContext().Client)
                {
                    if (inputClientInn.Text == Convert.ToString(client.INN))
                        ClientChoiseWindow.client = client;
                }
            }
            else if (checkId.IsChecked == true)
            {
                foreach (var client in TechnoGuideEntities.GetContext().Client)
                {
                    if (inputClientId.Text == Convert.ToString(client.ClientID))
                        ClientChoiseWindow.client = client;
                }
            }
            if (client.Name == null)
                MessageBox.Show("Клиент не найден");
            Close();
        }

        private void ClientChoiseWindowClosing(object sender, CancelEventArgs e)
        {
            WorkingWindow.client = client;
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