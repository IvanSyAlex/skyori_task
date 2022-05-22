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

        /// <summary>
        /// адресс почты получения письма
        /// </summary>
        private static string _emailTo = "sychyov-86@mail.ru";

        /// <summary>
        /// адресс сайта по умолчанию
        /// </summary>
        private static string _siteUrl = "https://yandex.ru/";

        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        private static string
            _connectionDb = ""; //"Server=LAPTOP-H2QHE71S\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True";

        
        static void Main(string[] args)
        {
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

                    GetCheckParameters(args);
                }

                ConnectionStringExists();
                
                jsonFile.WriteJsonFile(RunChecks(), path);
                send.SendEmail(_emailTo, path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Проверка _connectionDb на пустоту
        /// Если пустой то просим ввести строку подключению к БД  через консоль
        /// </summary>
        private static void ConnectionStringExists()
        {
            if (!string.IsNullOrEmpty(_connectionDb))
                return;

            Console.WriteLine("Введите строку подключению к БД: ");
            _connectionDb = Console.ReadLine();
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
            Console.WriteLine($"Связь с базой данных: {lastCheck.StringConnectionDb}");
            Console.WriteLine($"Результат проверки: {lastCheck.ConnectMsSqlResult }");
            Console.WriteLine($"Доступ к сайту: {lastCheck.UrlSite}");
            Console.WriteLine($"Результат проверки: {lastCheck.ConnectSiteResult}");
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
        /// При запуске приложения с параметром 
        /// Перебарает массив args
        /// Находит совпадение по ключу и задаёт новое значение
        /// </summary>
        /// <param name="args"></param>
        private static void GetCheckParameters(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                var argument = args[i];
                switch (argument)
                {
                    case "-EmailTo":
                        _emailTo = args[i + 1];
                        break;
                    case "-Site":
                        _siteUrl = args[i + 1];
                        break;
                    case "-ConnectionDb":
                        _connectionDb = args[i + 1];
                        break;
                }
            }
        }
        
        /// <summary>
        /// Провести проверки доступа к БД и сайту
        /// </summary>
        /// <returns>результат проверок</returns>
        private static Result RunChecks()
        {
            var connection = new Connection();
            
            // Запустить проверки и
            // сформировать модель результата
            var result = new Result
            {
                ConnectSiteResult = connection.ConnectionSite(_siteUrl),
                ConnectMsSqlResult = connection.ConnectionMsSQL(_connectionDb),
                StringConnectionDb = _connectionDb,
                UrlSite = _siteUrl,
                VerificationDate = DateTime.Now
            };

            return result;
        }
    }
}