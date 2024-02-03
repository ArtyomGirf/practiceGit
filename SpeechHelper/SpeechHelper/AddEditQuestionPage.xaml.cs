using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SpeechHelper
{
    /// <summary>
    /// Логика взаимодействия для AddEditQuestionPage.xaml
    /// </summary>
    public partial class AddEditQuestionPage : Page
    {
        private static AddEditWindow addEditWindow;
        private static Question question;
        public AddEditQuestionPage(Question question, AddEditWindow addEditWindow)
        {
            InitializeComponent();

            if (question == null)
                question = new Question();

            DataContext = question;

            AddEditQuestionPage.question = question;
            AddEditQuestionPage.addEditWindow = addEditWindow;
        }

        private void SaveRecordClick(object sender, RoutedEventArgs e)
        {
            if (question.QuestionID == 0)
            {
                int id = 0;
                int maxId = 0;

                List<int> ids = new List<int>();

                foreach (var question in TechnoGuideEntities.GetContext().Question)
                {
                    ids.Add(question.QuestionID);

                    if (question.QuestionID > maxId)
                        maxId = question.QuestionID;
                }

                for (int i = 0; i < ids.Count; id++, i++)
                {
                    if (ids[i] == id)
                        continue;
                    else
                        break;
                }

                question.QuestionID = id;
                question.Text = inputDescription.Text;

                TechnoGuideEntities.GetContext().Question.Add(question);
                MessageBox.Show("Запись успешно добавлена!", "Добавление записи", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Данные записи были успешно изменены!", "Изменение записи", MessageBoxButton.OK, MessageBoxImage.Information);

            TechnoGuideEntities.GetContext().SaveChanges();
            addEditWindow.Close();
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Добавление/Изменение записи было отменено!", "Добавление/Изменение записи", MessageBoxButton.OK, MessageBoxImage.Information);
            addEditWindow.Close();
        }
    }
}