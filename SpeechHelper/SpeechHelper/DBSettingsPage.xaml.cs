using System.Collections.Generic;
using System.Windows.Controls;

namespace SpeechHelper
{
    /// <summary>
    /// Логика взаимодействия для DBSettingsPage.xaml
    /// </summary>
    public partial class DBSettingsPage : Page
    {
        public DBSettingsPage()
        {
            InitializeComponent();

            List<string> list = new List<string>{ "Вопросы", "Ответы", "Связь", "Обращения", "Клиенты" };
            choiseTable.ItemsSource = list;
        }

        private void ChoiseTableSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(choiseTable.SelectedItem != null)
            {
                if(choiseTable.SelectedIndex == 0)
                {
                    DBTable.Navigate(new QuestionsSettingsPage());
                }
                else if (choiseTable.SelectedIndex == 1)
                {
                    DBTable.Navigate(new AnswersSettingsPage());
                
                }
                else if (choiseTable.SelectedIndex == 2)
                {
                    DBTable.Navigate(new QAConnectionSettingsPage());
                }
                else if (choiseTable.SelectedIndex == 3)
                {
                    DBTable.Navigate(new AppealSettingsPage());
                }
                else if(choiseTable.SelectedIndex == 4)
                {
                    DBTable.Navigate(new ClientSettingsPage());
                }
            }
        }
    }
}
