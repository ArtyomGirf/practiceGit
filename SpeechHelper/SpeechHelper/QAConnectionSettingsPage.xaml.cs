﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SpeechHelper
{
    /// <summary>
    /// Логика взаимодействия для QAConnectionSettingsPage.xaml
    /// </summary>
    public partial class QAConnectionSettingsPage : Page
    {
        List<Question> relationQuestions = new List<Question>();
        List<Answer> relationAnswers = new List<Answer>();
        List<int> idsQuestions = new List<int>();
        List<int> idsAnswers = new List<int>();
        public QAConnectionSettingsPage()
        {
            InitializeComponent();

            questions.ItemsSource = TechnoGuideEntities.GetContext().Question.ToList();
            answers.ItemsSource = TechnoGuideEntities.GetContext().Answer.ToList();
            questionsRelations.ItemsSource = TechnoGuideEntities.GetContext().QuestionAfterAnswer.ToList();
            answersRelations.ItemsSource = TechnoGuideEntities.GetContext().AnswerAfterQuestion.ToList();

            detachAnswerBtn.Content = ">>";
            addRelationsAnswers.Content = "Записать >>";
            detachQuestionBtn.Content = "<<";
            addRelationsQuestions.Content = "<< Записать";
        }

        private void ReloadAnswersRelations()
        {
            answersRelations.ItemsSource = TechnoGuideEntities.GetContext().AnswerAfterQuestion.ToList();
        }
        private void ReloadQuestionsRelations()
        {
            questionsRelations.ItemsSource = TechnoGuideEntities.GetContext().QuestionAfterAnswer.ToList();
        }

        private void RelateQuestionClick(object sender, RoutedEventArgs e)
        {
            relationQuestions.Add((Question)questions.SelectedValue);
            questionsList.ItemsSource = relationQuestions.ToList();
        }

        private void RelateAnswerClick(object sender, RoutedEventArgs e)
        {
            relationAnswers.Add((Answer)answers.SelectedValue);
            answersList.ItemsSource = relationAnswers.ToList();
        }

        private void DetachQuestionClick(object sender, RoutedEventArgs e)
        {
            relationQuestions.Remove((Question)questionsList.SelectedValue);
            questionsList.ItemsSource = relationQuestions.ToList();
        }

        private void DetachAnswerClick(object sender, RoutedEventArgs e)
        {
            relationAnswers.Remove((Answer)answersList.SelectedValue);
            answersList.ItemsSource = relationAnswers.ToList();
        }

        private void ClearListClick(object sender, RoutedEventArgs e)
        {
            relationAnswers.Clear();
            relationQuestions.Clear();
            answersList.ItemsSource = null;
            questionsList.ItemsSource = null;
        }

        private void ReloadQuestionsClick(object sender, RoutedEventArgs e)
        {
            questions.ItemsSource = TechnoGuideEntities.GetContext().Question.ToList();
        }

        private void ReloadAnswersClick(object sender, RoutedEventArgs e)
        {
            answers.ItemsSource = TechnoGuideEntities.GetContext().Answer.ToList();
        }

        private void DeleteRelationsQuestionsClick(object sender, RoutedEventArgs e)
        {
            var record = (sender as Button).DataContext as QuestionAfterAnswer;

            var message = MessageBox.Show("Вы действительно хотите удалить данную связь №" + record.RecordID + ": " + record.QuestionID + " <-> " + record.AnswerID + "?", "Удаление связи", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (message == MessageBoxResult.Yes)
            {
                TechnoGuideEntities.GetContext().QuestionAfterAnswer.Remove(record);
                TechnoGuideEntities.GetContext().SaveChanges();
                MessageBox.Show("Связь №" + record.RecordID + " удалена.", "Удаление связи", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Удаление связи №" + record.RecordID + " было отменено.", "Удаление связи", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            ReloadQuestionsRelations();
        }

        private void DeleteRelationsAnswersClick(object sender, RoutedEventArgs e)
        {
            var record = (sender as Button).DataContext as AnswerAfterQuestion;

            var message = MessageBox.Show("Вы действительно хотите удалить данную связь №" + record.RecordID + ": " + record.AnswerID + " <-> " + record.QuestionID + "?", "Удаление связи", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (message == MessageBoxResult.Yes)
            {
                TechnoGuideEntities.GetContext().AnswerAfterQuestion.Remove(record);
                TechnoGuideEntities.GetContext().SaveChanges();
                MessageBox.Show("Связь №" + record.RecordID + " удалена.", "Удаление связи", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Удаление связи №" + record.RecordID + " было отменено.", "Удаление связи", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            ReloadAnswersRelations();
        }

        private void ReloadRelationsQuestionsClick(object sender, RoutedEventArgs e)
        {
            ReloadQuestionsRelations();
        }

        private void ReloadRelationsAnswersClick(object sender, RoutedEventArgs e)
        {
            ReloadAnswersRelations();
        }

        private void AddRelationsQuestionsClick(object sender, RoutedEventArgs e)
        {
            if (relationQuestions.Count == 1)
            {
                if (relationAnswers.Count > 0)
                {
                    for (int i = 0; i < relationAnswers.Count; i++)
                    {
                        foreach (var record in TechnoGuideEntities.GetContext().QuestionAfterAnswer)
                        {
                            if (record.AnswerID == relationAnswers[i].AnswerID)
                            {
                                MessageBox.Show("Данный ответ (" + relationAnswers[i].AnswerID + ") уже связан с другим вопросом и его не возможно использовать для вызова другого вопроса (" + record.QuestionID + ").\nСоздание связи отменено.", "Добавление связи", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                    }

                    for (int i = 0; i < relationAnswers.Count; i++)
                    {
                        TechnoGuideEntities.GetContext().QuestionAfterAnswer.Add(new QuestionAfterAnswer
                        {
                            RecordID = GetIdQuestionRelation(idsQuestions),
                            QuestionID = relationQuestions[0].QuestionID,
                            AnswerID = relationAnswers[i].AnswerID,

                            Question = relationQuestions[0],
                            Answer = relationAnswers[i]
                        });
                    }
                    TechnoGuideEntities.GetContext().SaveChanges();
                }
                else
                    MessageBox.Show("Необходимо выбрать ответ(ы) для создания связи с вопросом.", "Создание связи", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
                MessageBox.Show("Необходимо выбрать вопрос для создания связи.", "Создание связи", MessageBoxButton.OK, MessageBoxImage.Warning);

            idsQuestions.Clear();
            ReloadQuestionsRelations();
        }
        private int GetIdQuestionRelation(List<int> ids)
        {
            int id = 0;

            if (ids.Count == 0)
                foreach (var record in TechnoGuideEntities.GetContext().QuestionAfterAnswer)
                    ids.Add(record.RecordID);

            for (int i = 0; i < ids.Count; id++, i++)
            {
                if (ids[i] == id)
                    continue;
                else
                {
                    break;
                }
            }

            ids.Add(id);
            ids.Sort();
            return id;
        }
        private int GetIdAnswerRelation(List<int> ids)
        {
            int id = 0;

            if (ids.Count == 0)
                foreach (var record in TechnoGuideEntities.GetContext().AnswerAfterQuestion)
                    ids.Add(record.RecordID);

            for (int i = 0; i < ids.Count; id++, i++)
            {
                if (ids[i] == id)
                    continue;
                else
                {
                    break;
                }
            }

            ids.Add(id);
            ids.Sort();
            return id;
        }
        private void AddRelationsAnswersClick(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < relationQuestions.Count; i++)
            {
                bool currentRecord = false;

                foreach (var record in TechnoGuideEntities.GetContext().AnswerAfterQuestion)
                {
                    if ((record.QuestionID == relationQuestions[i].QuestionID) && (record.AnswerID == relationAnswers[i].AnswerID))
                    {
                        MessageBox.Show("Связь: " + relationQuestions[i].QuestionID + " <=> " + relationAnswers[i].AnswerID +
                            " уже существует.\nДобавление данной связи отменено.", "Добавление связи", MessageBoxButton.OK, MessageBoxImage.Warning);
                        currentRecord = true;
                        break;
                    }
                }
                if (!currentRecord)
                    TechnoGuideEntities.GetContext().AnswerAfterQuestion.Add(new AnswerAfterQuestion
                    {
                        RecordID = GetIdAnswerRelation(idsAnswers),
                        QuestionID = relationQuestions[i].QuestionID,
                        AnswerID = relationAnswers[i].AnswerID,

                        Question = relationQuestions[i],
                        Answer = relationAnswers[i]
                    });
            }
            TechnoGuideEntities.GetContext().SaveChanges();
            ReloadAnswersRelations();
        }

        private void QuestionsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (questions.SelectedValue != null)
            {
                relationQuestions.Add((Question)questions.SelectedValue);
                questionsList.ItemsSource = relationQuestions.ToList();
                questions.SelectedValue = null;
            }
        }

        private void AnswersSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (answers.SelectedValue != null)
            {
                relationAnswers.Add((Answer)answers.SelectedValue);
                answersList.ItemsSource = relationAnswers.ToList();
                answers.SelectedValue = null;
            }
        }
    }
}   