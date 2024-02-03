using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SpeechHelper
{
    /// <summary>
    /// Логика взаимодействия для AddEditClientPage.xaml
    /// </summary>
    public partial class AddEditClientPage : Page
    {
        private static AddEditWindow addEditWindow;
        private static Client client;
        public AddEditClientPage(Client client, AddEditWindow addEditWindow)
        {
            InitializeComponent();

            if (client == null)
                client = new Client();
            
            AddEditClientPage.addEditWindow = addEditWindow;
            AddEditClientPage.client = client;

            DataContext = client;

            comboTypes.ItemsSource = TechnoGuideEntities.GetContext().TypeOfClient.ToList();
        }

        private void SaveClientClick(object sender, RoutedEventArgs e)
        {
            if (client.ClientID == 0)
            {
                int id = 0;
                int maxId = 0;

                List<int> ids = new List<int>();

                foreach (var client in TechnoGuideEntities.GetContext().Client)
                {
                    ids.Add(client.ClientID);

                    if (client.ClientID > maxId)
                        maxId = client.ClientID;
                }

                for (int i = 0; i < ids.Count; id++, i++)
                {
                    if (ids[i] == id)
                        continue;
                    else
                        break;
                }

                client.ClientID = id;
                client.TelephoneNumber = inputTelephoneNumber.Text;
                client.Name = inputClientName.Text;
                client.INN = Convert.ToInt32(inputClientINN.Text);
                client.Linked = (bool)linkedClient.IsChecked;

                TechnoGuideEntities.GetContext().Client.Add(client);
                MessageBox.Show("Добавлениее данных клиента завершено успешно!", "Добавление данных клиента", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Изменение данных клиента завершено успешно!", "Изменение данных пользователя", MessageBoxButton.OK, MessageBoxImage.Information);

            TechnoGuideEntities.GetContext().SaveChanges();
            addEditWindow.Close();
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Добавление/Изменение данных клиента было отменено!", "Добавление/Изменение данных клиента", MessageBoxButton.OK, MessageBoxImage.Information);
            addEditWindow.Close();
        }
    }
}
