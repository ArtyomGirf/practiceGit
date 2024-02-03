using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SpeechHelper
{
    /// <summary>
    /// Логика взаимодействия для AppealSettingsPage.xaml
    /// </summary>
    public partial class AppealSettingsPage : Page
    {
        public AppealSettingsPage()
        {
            InitializeComponent();

            DGridAppeal.ItemsSource = TechnoGuideEntities.GetContext().Appeal.ToList();
        }

        private void ReplaceDGridClick(object sender, RoutedEventArgs e)
        {
            DGridAppeal.ItemsSource = TechnoGuideEntities.GetContext().Appeal.ToList();
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            var message = MessageBox.Show("Вы действительно хотите изменить запись №" + ((sender as Button).DataContext as Appeal).AppealCode + '?', "Изменение записи", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (message == MessageBoxResult.Yes)
            {
                var editWindow = new AddEditWindow((sender as Button).DataContext as Appeal);
                editWindow.Show();
            }
            else
                MessageBox.Show("Изменение записи было отменено!", "Изменение записи", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteRecordClick(object sender, RoutedEventArgs e)
        {
            var appeal = (sender as Button).DataContext as Appeal;

            var message = MessageBox.Show("Вы действительно хотите удалить запись №" + appeal.AppealCode + '?', "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (message == MessageBoxResult.Yes)
            {
                TechnoGuideEntities.GetContext().Appeal.Remove(appeal);
                TechnoGuideEntities.GetContext().SaveChanges();
                MessageBox.Show("Удаление записи прошло успешно", "Удаление записи", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Удаление записи отменено!", "Удаление записи", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}