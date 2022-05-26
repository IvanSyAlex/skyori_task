using System;
using System.Collections.Generic;


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
        /// Результат проверки подключения к сайту
        /// </summary>
        public List<string> ConnectSiteResult { get; set; } = new List<string>();

        
        /// <summary>
        /// Результат проверки подключения к Базе
        /// </summary>
        public List<string> ConnectMsSqlResult { get; set; } = new List<string>();

        
        public Result()
        {
        }
    }
}
