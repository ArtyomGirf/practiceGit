using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SpeechHelper
{
    /// <summary>
    /// Логика взаимодействия для AddEditUserPage.xaml
    /// </summary>
    public partial class AddEditUserPage : Page
    {
        private static AddEditWindow addEditWindow;
        private static User user;
        private bool newUser;
        public AddEditUserPage(User user, AddEditWindow addEditWindow)
        {
            InitializeComponent();
            comboRoles.ItemsSource = TechnoGuideEntities.GetContext().TypeOfRole.ToList();

            if (user != null)
            {
                AddEditUserPage.user = user;
                newUser = false;
            }
            else
            {
                AddEditUserPage.user = new User();
                newUser = true;
            }
            DataContext = AddEditUserPage.user;
            AddEditUserPage.addEditWindow = addEditWindow;
        }
        private void SaveUserClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (inputUserLogin.Text == "")
                errors.AppendLine("- Логин");
            if (comboRoles.SelectedIndex != 1)
            {
                if (inputUserPassword.Text == "")
                    errors.AppendLine("- Пароль");
            }
            if (comboRoles.SelectedIndex < 0)
                errors.AppendLine("- Роль");

            if (errors.Length > 0)
            {
                MessageBox.Show("Заполните поля:\n" + errors.ToString(), "Ошибка!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                foreach(var user in TechnoGuideEntities.GetContext().User)
                {
                    if (user.UserID != AddEditUserPage.user.UserID)
                        if (user.Login == inputUserLogin.Text)
                        {
                            MessageBox.Show("Ошибка!!!\nПользователь с таким логином уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                }

                if (newUser)
                {
                    int id = 0;
                    int maxId = 0;

                    List<int> ids = new List<int>();

                    foreach (var user in TechnoGuideEntities.GetContext().User)
                    {
                        ids.Add(user.UserID);

                        if (user.UserID > maxId)
                            maxId = user.UserID;
                    }

                    for (int i = 0; i < ids.Count; id++, i++)
                    {
                        if (ids[i] == id)
                            continue;
                        else
                            break;
                    }

                    user.UserID = id;
                    user.Login = inputUserLogin.Text;
                    user.Password = inputUserPassword.Text;
                    user.Surename = inputUserFirstname.Text;
                    user.Name = inputUserLastname.Text;
                    user.RoleID = comboRoles.SelectedIndex + 1;
                    user.ExtentionNumber = Convert.ToInt32(inputExtentionNumber.Text);

                    TechnoGuideEntities.GetContext().User.Add(user);
                    MessageBox.Show("Пользователь " + user.Surename + " " + user.Name + " успешно добавлен.", "Добавление данных пользователя", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Изменения для пользователя " + user.Surename + " " + user.Name + " успешно внесены.", "Изменение данных пользователя", MessageBoxButton.OK, MessageBoxImage.Information);

                TechnoGuideEntities.GetContext().SaveChanges();
                addEditWindow.Close();
            }
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Добавление/Изменение данных пользователя было отменено!", "Добавление/Изменение данных пользователя", MessageBoxButton.OK, MessageBoxImage.Information);
            addEditWindow.Close();
        }
    }
}