using SpeechHelper.Properties;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace SpeechHelper
{
    /// <summary>
    /// Логика взаимодействия для NotesPage.xaml
    /// </summary>
    public partial class NotesPage : Page, IWorkWithFileInterface
    {
        private readonly double[] FontSizeCollection = { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 }; // Массив часто используемых размеров шрифта.
        public NotesPage()
        {
            InitializeComponent();
            notesText.FontSize = Settings.Default.FontSizeNotes; // Задает последний установленный размер шрифта.
        }
        private void ShowFontSize()
        {
            try
            {
                fontSizeTB.Text = Convert.ToString(notesText.FontSize);
                Settings.Default.FontSizeNotes = notesText.FontSize;
                Settings.Default.Save();
            }
            catch (FormatException)
            {
                Settings.Default.FontSizeNotes = 14;
                Settings.Default.Save();
                fontSizeTB.Text = Convert.ToString(Settings.Default.FontSizeNotes);
            }
        }
        /// <summary>
        /// Записывает полученный текст из поля записи в файл формата .txt по указанному адресу.
        /// </summary>
        /// <param name="adress">Путь к файлу с заметками.</param>
        /// <param name="text">Текст для записи в файл.</param>
        public void WriteToFile(string adress, string text)
        {
            try
            {
                using (FileStream file = new FileStream(adress, FileMode.Open))
                {
                    file.Close();
                    using (StreamWriter streamWriter = new StreamWriter(adress))
                    {
                        streamWriter.WriteLine(text);
                    }
                }
            }
            catch (IOException)
            {
                MessageBox.Show("При записи в файл произошла ошибка! Закройте файл и повторите попытку!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        /// <summary>
        /// Считывает текст из файла формата .txt по указанному адресу.
        /// </summary>
        /// <param name="adress">Путь к файлу с заметками.</param>
        /// <returns>Считанный текст из файла по полученному пути.</returns>
        public string ReadToFile(string adress)
        {
            try
            {
                using (FileStream file = new FileStream(adress, FileMode.Append))
                {
                    file.Close();
                    using (StreamReader streamReader = new StreamReader(adress))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Не удалось найти/создать файл!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("У вас нет прав доступа к файлу!\n" +
                    "Перейдите в Настройки -> Основные, чтобы указать путь к файлу.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        /// <summary>
        /// Выводит текст из файла заметок в окно записи.
        /// </summary>
        public void WriteToNotes()
        {
            notesText.Text = ReadToFile(Settings.Default.PathToFileWithNotes);
        }
        /// <summary>
        /// Записывает текст из поля записи текста в файл по установленному ранее адресу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WriteToFileBtnClick(object sender, RoutedEventArgs e)
        {
            WriteToFile((Settings.Default.PathToFileWithNotes), notesText.Text);
        }
        /// <summary>
        /// Увелличивает размер шрифта на 1 позицию массива, если выходит за границы массива, то увеличение идет до первого элемента массива,если меньше, или 2 пт., если больше.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontUpBtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (notesText.FontSize < 8)
                {
                    notesText.FontSize++;
                    ShowFontSize();
                }
                else if (notesText.FontSize >= 72)
                {
                    notesText.FontSize += 2;
                    ShowFontSize();
                }

                else
                {
                    for (int i = 0; i < FontSizeCollection.Length; i++)
                    {
                        if (notesText.FontSize >= FontSizeCollection[i])
                            continue;
                        else
                        {
                            notesText.FontSize = FontSizeCollection[i];
                            ShowFontSize();
                            break;
                        }
                    }
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Превышено МИНИМАЛЬНОЕ/МАКСИМАЛЬНОЕ допустимое значение размера шрифта!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                ShowFontSize();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Критическая ошибка!\n" + ex.ToString(),
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Уменьшает размер шрифта на 1 позицию массива, если выходит за границы массива, то уменьшение идет до последнего элемента массива,если больше, или 1 пт., если меньше.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontDownBtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((notesText.FontSize <= 9) && (notesText.FontSize > 0))
                {
                    notesText.FontSize--;
                    ShowFontSize();
                }
                else
                {
                    for (int i = FontSizeCollection.Length - 1; i > 0; i--)
                    {
                        if (notesText.FontSize <= FontSizeCollection[i])
                            continue;
                        else
                        {
                            notesText.FontSize = FontSizeCollection[i];
                            ShowFontSize();
                            break;
                        }
                    }
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Превышено МИНИМАЛЬНОЕ/МАКСИМАЛЬНОЕ допустимое значение размера шрифта!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                ShowFontSize();
            }

        }
        /// <summary>
        /// Удаляет текст из поля записи шрифта.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontSizeDeleteBtnClick(object sender, RoutedEventArgs e)
        {
            fontSizeTB.Text = "";
        }
        /// <summary>
        /// Устанавливает размер шрифта из поля записи шрифта.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontSizeApplyBtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (fontSizeTB.Text == "")
                    fontSizeTB.Text = Convert.ToString(notesText.FontSize);
                else if ((Convert.ToDouble(fontSizeTB.Text) >= 1) && (Convert.ToDouble(fontSizeTB.Text) <= 256))
                {
                    notesText.FontSize = Convert.ToDouble(fontSizeTB.Text);
                    ShowFontSize();
                }
                else
                {
                    MessageBox.Show("Ошибка ввода! Укажите размер шрифта в диапазоне от 1 до 256.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка ввода! Используйте цифры для указания размера шрифта.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                ShowFontSize();
                return;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Превышено МИНИМАЛЬНОЕ/МАКСИМАЛЬНОЕ допустимое значение размера шрифта!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                ShowFontSize();
            }
            catch (Exception)
            {
                MessageBox.Show("Критическая ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
