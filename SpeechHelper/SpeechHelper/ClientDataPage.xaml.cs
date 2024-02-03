using System.Linq;
using System.Windows.Controls;

namespace SpeechHelper
{
    /// <summary>
    /// Логика взаимодействия для ClietnDataPage.xaml
    /// </summary>
    public partial class ClientDataPage : Page
    {
        public ClientDataPage(Client client)
        {
            InitializeComponent();

            if (client == null)
            {
                client = new Client();
            }
            else
            {
                clientId.IsEnabled = false;
                clientInn.IsEnabled = false;
                clientLinked.IsEnabled = false;
                clientName.IsEnabled = false;
                clientType.IsEnabled = false;
            }

            clientType.ItemsSource = TechnoGuideEntities.GetContext().TypeOfClient.ToList();
            DataContext = client;

            if (client.Linked == true)
                clientLinked.IsChecked = true;
            else
                clientLinked.IsChecked = false;
        }
    }
}