using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SpeechHelper
{
    /// <summary>
    /// Логика взаимодействия для AddEditAnswerPage.xaml
    /// </summary>
    public partial class AddEditAnswerPage : Page
    {
        private static AddEditWindow addEditWindow;
        private static Answer answer;
        public AddEditAnswerPage(Answer answer, AddEditWindow addEditWindow)
        {
            InitializeComponent();

            if(answer == null)
            {
                answer = new Answer();
            }

            DataContext = answer;

            AddEditAnswerPage.addEditWindow = addEditWindow;
            AddEditAnswerPage.answer = answer;
        }
        private void SaveRecordClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (inputDescription.Text == "")
                errors.AppendLine("\n- Описание");

            if (errors.Length > 0)
            {
                MessageBox.Show("Заполните поля:\n" + errors.ToString(), "Ошибка!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if (answer.AnswerID == 0)
                {
                    int id = 0;
                    int maxId = 0;

                    List<int> ids = new List<int>();

                    foreach (var answer in TechnoGuideEntities.GetContext().Answer)
                    {
                        ids.Add(answer.AnswerID);

                        if (answer.AnswerID > maxId)
                            maxId = answer.AnswerID;
                    }

                    for (int i = 0; i < ids.Count; id++, i++)
                    {
                        if (ids[i] == id)
                            continue;
                        else
                            break;
                    }

                    answer.AnswerID = id;
                    answer.Text = inputDescription.Text;

                    TechnoGuideEntities.GetContext().Answer.Add(answer);
                    TechnoGuideEntities.GetContext().SaveChanges();

                    MessageBox.Show("Запись успешно добавлена!", "Добавление записи", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Запись успешно изменена!", "Изменение записи", MessageBoxButton.OK, MessageBoxImage.Information);

                addEditWindow.Close();
            }
        }
        private void CloseClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Добавление/Изменение записи было отменено!", "Добавление/Изменение данных", MessageBoxButton.OK, MessageBoxImage.Information);
            addEditWindow.Close();
        }
    }
}