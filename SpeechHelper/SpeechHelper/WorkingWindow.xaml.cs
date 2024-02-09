using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SpeechHelper
{
    /// <summary>
    /// Логика взаимодействия для WorkingWindow.xaml
    /// </summary>
    public partial class WorkingWindow : Window
    {
        public static bool OpennedSettings = false;

        private static User user;
        public static Client client = new Client();

        private static NotesPage notes = new NotesPage();
        private static SettingsWindow settings;
        private static ClientChoiseWindow clientChoise;

        private StringBuilder descriptionAppeal = new StringBuilder();

        List<ButtonAnswer> answers = new List<ButtonAnswer>();
        public WorkingWindow(User user)
        {
            InitializeComponent();
            WorkingWindow.user = user;
            if (user.RoleID == 1)
            {
                startButton.Visibility = Visibility.Collapsed;
                stopButton.Visibility = Visibility.Collapsed;

                userRole.Content = "Администратор";
            }
            else if (user.RoleID == 2)
                userRole.Content = "Пользователь";
            else
                userRole.Content = "Редактор";

            userData.DataContext = user;
        }
        public static void SelectStatusForWindow(ref bool status)
        {
            if (status == true)
                status = false;
            else
                status = true;

        }
        public void SetClient(Client client)
        {
            WorkingWindow.client = client;
        }
        private void SettingsButtonClick(object sender, RoutedEventArgs e)
        {
            if (OpennedSettings == false)
            {
                settings = new SettingsWindow(user);
                settings.Show();
                SelectStatusForWindow(ref OpennedSettings);
            }
            else if (settings != null)
                settings.Close();
        }

        private void WorkingWindowClosing(object sender, CancelEventArgs e)
        {
            try
            {
                if (OpennedSettings != false)
                    settings.Close();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StartButtonClick(object sender, RoutedEventArgs e)
        {
            stopButton.IsEnabled = true;
            clientChoise = new ClientChoiseWindow(client);
            clientChoise.Show();

            var message = MessageBox.Show(this, "Вы выбрали клиента?", "Выбор клиента", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (message == MessageBoxResult.Yes)
            {
                if (client.Name == null)
                {
                    MessageBox.Show("Клиент, не выбран!", "Выбор клиента", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    clientChoise.Close();
                }
                else
                    clientData.Navigate(new ClientDataPage(client));
            }
            else
            {
                stopButton.IsEnabled = false;
                MessageBox.Show("Чтобы начать сессию необходимо выбрать клиента!", "Выбор клиента", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                clientChoise.Close();
                return;
            }

            foreach (var record in TechnoGuideEntities.GetContext().QuestionAfterAnswer)
            {
                if (record.AnswerID == null)
                {
                    foreach (var question in TechnoGuideEntities.GetContext().Question)
                    {
                        if (record.QuestionID == question.QuestionID)
                        {
                            workingFrame.Children.Add(new ButtonQuestion { Content = question.Text, Tag = question.QuestionID, Visibility = Visibility.Visible }); 
                        }
                    }
                }
            }
            startButton.IsEnabled = false;
        }

        private void ButtonQuestionClick(object sender, RoutedEventArgs e)
        {
            (sender as Button).IsEnabled = false;
            descriptionAppeal.Append((sender as ButtonQuestion).Content + "\n"); // запись текста для обработки обращения.

            foreach (var record in TechnoGuideEntities.GetContext().AnswerAfterQuestion) // поиск в БД записей, имеющих данные о номере кнопки.
            {
                if (record.QuestionID == Convert.ToInt32((sender as Button).Tag))
                {
                    foreach (var answer in TechnoGuideEntities.GetContext().Answer) // поиск объектов в БД, имеющих схожий номер с данными в записи
                    {
                        if (record.AnswerID == answer.AnswerID)
                            answers.Add(new ButtonAnswer
                            { Tag = answer.AnswerID, Content = answer.Text, Visibility = Visibility.Visible }); // создание объекта, передача полученных данных из БД в конструктор.
                    }
                }
            }

            for (int i = 0; i < answers.Count; i++)
                answerChoise.Children.Add(answers[i]); // вывод созданных объектов.
        }

        private void ButtonAnswerClick(object sender, RoutedEventArgs e)
        {
            answerChoise.Children.Clear(); // удаление созданных объектов из поля зрения пользователя.
            workingFrame.Children.Add(sender as ButtonAnswer); // добавление в последовательнось выбранного пользователем объекта.

            (sender as Button).IsEnabled = false; // отключение взаимодействия с пользователем у выбранного объекта

            descriptionAppeal.Append((sender as ButtonAnswer).Content + "\n"); // запись текста для обработки обращения.

            foreach (var record in TechnoGuideEntities.GetContext().QuestionAfterAnswer) // поиск в БД записей, имеющих данные о номере выбранного объекта.
            {
                if (record.AnswerID == Convert.ToInt32((sender as ButtonAnswer).Tag))
                {
                    foreach (var question in TechnoGuideEntities.GetContext().Question) // поиск объектов в БД, имеющих схожий номер с данными в записи
                    {
                        if (record.QuestionID == question.QuestionID)
                            workingFrame.Children.Add(new ButtonQuestion 
                            { Tag = question.QuestionID, Content = question.Text, Visibility = Visibility.Visible }); // создание объекта, передача полученных данных из БД в конструктор.
                    }
                }
            }

            answers.Clear(); // обнуление списка созданных ранее объектов.
        }
        private void StopButtonClick(object sender, RoutedEventArgs e)
        {
            if (descriptionAppeal.ToString() != "")
            {
                var message = MessageBox.Show("Вы хотите сохранить обращение?", 
                    "Сохранение обращения", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (message == MessageBoxResult.Yes)
                {
                    var newAppeal = new Appeal
                    {
                        UserID = user.UserID,
                        ClientID = client.ClientID,
                        DateTime = DateTime.Now,
                        Description = descriptionAppeal.ToString(),
                        User = user,
                        Client = client
                    };

                    TechnoGuideEntities.GetContext().Appeal.Add(new Appeal
                    {
                        UserID = user.UserID, ClientID = client.ClientID, DateTime = DateTime.Now,
                        Description = descriptionAppeal.ToString(), User = user, Client = client
                    });
                    TechnoGuideEntities.GetContext().SaveChanges();

                    MessageBox.Show("Обращение успешно сохранено!", 
                        "Сохранение обращения", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Обращение удалено!",
                        "Сохранение обращения", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            startButton.IsEnabled = true;
            stopButton.IsEnabled = false;
            workingFrame.Children.Clear();
            clientData.Navigate(null);
        }
        private void ChangeUserClick(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow(user);
            main.Show();
            Close();
        }

        private void NotesExpanded(object sender, RoutedEventArgs e)
        {
            notesFrame.Navigate(notes);
            notes.WriteToNotes();
        }
    }
}