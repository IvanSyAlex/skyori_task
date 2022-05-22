using System;


namespace TestSkyori
{
    /// <summary>
    /// Модель результата проверки
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Дата проверки
        /// </summary>
        public DateTime VerificationDate { get; set; }
        
        /// <summary>
        /// Адрес сайта
        /// </summary>
        public  string UrlSite { get; set; }
        
        /// <summary>
        /// Результат проверки подключения к сайту
        /// </summary>
        public bool ConnectSiteResult { get; set; }
        
        /// <summary>
        /// Строка подключения к Баззе данных
        /// </summary>
        public string StringConnectionDb { get; set; }
        
        /// <summary>
        /// Результат проверки подключения к Базе
        /// </summary>
        public bool ConnectMsSqlResult { get; set; }

        public Result()
        {
        }
    }
}
