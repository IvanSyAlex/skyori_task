using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace TestSkyori
{
    public class Program
    {
        /// <summary>
        /// Имя файла
        /// </summary>
        private static readonly string _fileName = "Checking_Access.json";

        /// <summary>
        /// текущая директория
        /// </summary>
        private static string _currentPath = Directory.GetCurrentDirectory();
        

        static void Main(string[] args)
        {
            var sm = new ScreenManager();
            var path = _currentPath + "\\" + _fileName;
            var send = new SendingEmails();
            var jsonFile = new FileGenerator();

            try
            {
                if (args.Length > 0)
                {
                    if (args[0].Equals("-Print"))
                    {
                        ShowResult(GetCheckListFromFile(path));
                        return;
                    }
                }
                
                // Запустить экран ввода данных
                var dataForChecks = sm.Start();
                if (dataForChecks.ScreenResult == ScreenTypes.StartCheck)
                {
                    // Запустить проверки и записать результат в файл
                    jsonFile.WriteJsonFile(RunChecks(dataForChecks.SiteList,dataForChecks.ConnectionDbStringList), path);
                
                    // Отправка файла с результатима проверок  по почте
                    send.SendEmail(dataForChecks.Email, path);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        
        /// <summary>
        /// Выводит на консоль информацию о последней проверке
        /// </summary>
        /// <param name="check">Список проверок подключения</param>
        private static void ShowResult(List<Result> checks)
        {
            // Если список пуст 
            if (checks is null || checks.Count <= 0)
            {
                Console.WriteLine($"Результатов проверок не обнаружено");
                return;
            }

            var lastCheck = checks.LastOrDefault();
            Console.WriteLine($"Дата и время последней проверки: {lastCheck.VerificationDate}");
            
            Console.WriteLine($"Связь с базой данных:");
            foreach (var item in lastCheck.ConnectMsSqlResult)
            {
                Console.WriteLine($"Результат проверки: {item}");
            }
            
            Console.WriteLine($"Связь с Сайтом:");
            foreach (var item in lastCheck.ConnectSiteResult)
            {
                Console.WriteLine($"Результат проверки: {item}");
            }
        }


        /// <summary>
        /// Проверяет существует ли файл
        /// Если файл существует то вернёт набор записей в виде  List<Result>
        /// Если файл не существует то вернёт пустой List<Result>
        /// </summary>
        /// <param name="fileNAme">Путь до файла</param>
        /// <returns></returns>
        private static List<Result> GetCheckListFromFile(string fileName)
        {
            return File.Exists(fileName)
                ? JsonConvert.DeserializeObject<List<Result>>(File.ReadAllText(fileName))
                : new List<Result>();
        }

        
        /// <summary>
        /// Провести проверки доступа к БД и сайту
        /// </summary>
        /// <returns>результат проверок</returns>
        private static Result RunChecks(List<string> siteUrl, List<string> connectionDb)
        {
            var connection = new Connection();
            var result = new Result();

            foreach (var item in siteUrl)
            {
                result.ConnectSiteResult.Add(connection.ConnectionSite(item));
            }
            
            foreach (var item in connectionDb)
            {
                result.ConnectMsSqlResult.Add(connection.ConnectionMsSQL(item));
            }
            
            result.VerificationDate = DateTime.Now;
            return result;
        }
    }
}