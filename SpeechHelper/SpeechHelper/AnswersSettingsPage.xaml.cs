using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SpeechHelper
{
    /// <summary>
    /// Логика взаимодействия для AnswersSettingsPage.xaml
    /// </summary>
    public partial class AnswersSettingsPage : Page
    {
        public AnswersSettingsPage()
        {
            InitializeComponent();

            DGridAnswers.ItemsSource = TechnoGuideEntities.GetContext().Answer.ToList();
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            var message = MessageBox.Show("Вы действительно хотите внести изменнения?", "Изменение данных", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (message == MessageBoxResult.Yes)
            {
                var editWindow = new AddEditWindow((sender as Button).DataContext as Answer);
                editWindow.Show();
            }
            else
            {
                MessageBox.Show("Внесение изменений отменено!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ReplaceDGridClick(object sender, RoutedEventArgs e)
        {
            DGridAnswers.ItemsSource = TechnoGuideEntities.GetContext().Answer.ToList();
        }
        private void AddRecordClick(object sender, RoutedEventArgs e)
        {
            var editWindow = new AddEditWindow(new Answer());
            editWindow.Show();
        }

        private void DeleteRecordClick(object sender, RoutedEventArgs e)
        {
            var answer = (sender as Button).DataContext as Answer;

            var message = MessageBox.Show("Вы действительно хотите удалить запись №" + answer.AnswerID + "?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (message == MessageBoxResult.Yes)
            {
                bool foreignKeyRecords = false;

                foreach (var answerRelation in TechnoGuideEntities.GetContext().AnswerAfterQuestion)
                {
                    if (answer.AnswerID == answerRelation.AnswerID)
                    {
                        foreignKeyRecords = true;
                    }
                }
                foreach (var questionRelation in TechnoGuideEntities.GetContext().QuestionAfterAnswer)
                {
                    if (answer.AnswerID == questionRelation.AnswerID)
                    {
                        foreignKeyRecords = true;
                    }
                }

                if (foreignKeyRecords)
                {
                    var msg = MessageBox.Show("Для удаления необходимо удалить все связные записи. Вы хотите удалить все связные записи?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (msg == MessageBoxResult.Yes)
                    {
                        foreach (var record in TechnoGuideEntities.GetContext().AnswerAfterQuestion)
                        {
                            if (record.QuestionID == answer.AnswerID)
                                TechnoGuideEntities.GetContext().AnswerAfterQuestion.Remove(record);
                        }
                        foreach (var record in TechnoGuideEntities.GetContext().QuestionAfterAnswer)
                        {
                            if (record.QuestionID == answer.AnswerID)
                                TechnoGuideEntities.GetContext().QuestionAfterAnswer.Remove(record);
                        }
                    }
                    else if (msg == MessageBoxResult.No)
                    {
                        MessageBox.Show("Удаление записи отменено!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }

                TechnoGuideEntities.GetContext().Answer.Remove(answer);
                TechnoGuideEntities.GetContext().SaveChanges();
                MessageBox.Show("Запись №" + answer.AnswerID + " удалена.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (message == MessageBoxResult.No)
            {
                MessageBox.Show("Удаление записи №" + answer.AnswerID + " отменено.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}