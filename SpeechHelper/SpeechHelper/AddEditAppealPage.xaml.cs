using System.Windows;
using System.Windows.Controls;

namespace SpeechHelper
{
    /// <summary>
    /// Логика взаимодействия для AddEditAppealPage.xaml
    /// </summary>
    public partial class AddEditAppealPage : Page
    {
        private static AddEditWindow addEditWindow;
        public AddEditAppealPage(Appeal appeal, AddEditWindow addEditWindow)
        {
            InitializeComponent();
            
            AddEditAppealPage.addEditWindow = addEditWindow;

            DataContext = appeal;
        }

        private void SaveRecord(object sender, RoutedEventArgs e)
        {
            TechnoGuideEntities.GetContext().SaveChanges();
            MessageBox.Show("Изменение записи прошло успешно!", "Изменение записи", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Изменение записи было отменено!", "Изменение записи", MessageBoxButton.OK, MessageBoxImage.Information);
            addEditWindow.Close();
        }
    }
}
