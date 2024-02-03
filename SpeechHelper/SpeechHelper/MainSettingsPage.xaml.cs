using Microsoft.Win32;
using SpeechHelper.Properties;
using System.Windows;
using System.Windows.Controls;

namespace SpeechHelper
{
    /// <summary>
    /// Логика взаимодействия для MainSettingsPage.xaml
    /// </summary>
    public partial class MainSettingsPage : Page
    {
        public MainSettingsPage()
        {
            InitializeComponent();
            inputPathToNotes.Text = Settings.Default.PathToFileWithNotes;
        }

        private void FileReviewClick(object sender, RoutedEventArgs e)
        { 
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                inputPathToNotes.Text = openFileDialog.FileName;
        }

        private void SaveSettingsBtnClick(object sender, RoutedEventArgs e)
        {
            Settings.Default.PathToFileWithNotes = inputPathToNotes.Text;
            Settings.Default.Save();

            MessageBox.Show("Путь к файлу сохранен.", "Сохранение",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
