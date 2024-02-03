namespace SpeechHelper
{
    internal interface IWorkWithFileInterface
    {
        /// <summary>
        /// Записывает полученный текст в файл по указанному адресу
        /// </summary>
        /// <param name="adress"></param>
        /// <param name="text"></param>
        void WriteToFile(string adress, string text);
        /// <summary>
        /// Считывает данные из файла
        /// </summary>
        /// <param name="adress"></param>
        /// <returns>string</returns>
        string ReadToFile(string adress);
    }
}
