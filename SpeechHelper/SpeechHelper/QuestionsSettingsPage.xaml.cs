using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SpeechHelper
{
    /// <summary>
    /// Логика взаимодействия для QuestionsSettingsPage.xaml
    /// </summary>
    public partial class QuestionsSettingsPage : Page
    {
        public QuestionsSettingsPage()
        {
            InitializeComponent();
            DGridQuestions.ItemsSource = TechnoGuideEntities.GetContext().Question.ToList();
        }
        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            var message = MessageBox.Show("Вы действительно хотите внести изменнения?", "Изменение данных", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (message == MessageBoxResult.Yes)
            {
                var editWindow = new AddEditWindow((sender as Button).DataContext as Question);
                editWindow.Show();
            }
            else
            {
                MessageBox.Show("Внесение изменений отменено!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ReplaceDGridClick(object sender, RoutedEventArgs e)
        {
            DGridQuestions.ItemsSource = TechnoGuideEntities.GetContext().Question.ToList();
        }

        private void AddRecordClick(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddEditWindow(new Question());
            addWindow.Show();
        }

        private void DeleteRecordClick(object sender, RoutedEventArgs e)
        {
            var question = (sender as Button).DataContext as Question;

            var message = MessageBox.Show("Вы действительно хотите удалить запись №" + question.QuestionID + "?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (message == MessageBoxResult.Yes)
            {
                bool foreignKeyRecords = false;

                foreach(var answer in TechnoGuideEntities.GetContext().AnswerAfterQuestion)
                {
                    if (question.QuestionID == answer.QuestionID)
                    {
                        foreignKeyRecords = true;
                    }
                }
                foreach (var questionRelation in TechnoGuideEntities.GetContext().QuestionAfterAnswer)
                {
                    if (question.QuestionID == questionRelation.QuestionID)
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
                            if (record.QuestionID == question.QuestionID)
                                TechnoGuideEntities.GetContext().AnswerAfterQuestion.Remove(record);
                        }
                        foreach (var record in TechnoGuideEntities.GetContext().QuestionAfterAnswer)
                        {
                            if (record.QuestionID == question.QuestionID)
                                TechnoGuideEntities.GetContext().QuestionAfterAnswer.Remove(record);
                        }
                    }
                    else if (msg == MessageBoxResult.No)
                    {
                        MessageBox.Show("Удаление записи отменено!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                TechnoGuideEntities.GetContext().Question.Remove(question);
                TechnoGuideEntities.GetContext().SaveChanges();
                MessageBox.Show("Запись №" + question.QuestionID + " удалена!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (message == MessageBoxResult.No) 
            {
                MessageBox.Show("Удаление записи отменено!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
