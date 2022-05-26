using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;

namespace TestSkyori
{
    public class Connection
    {
        /// <summary>
        /// Проверяет пустая ли строка url
        /// если нет, то проверяет доступ к сайту 
        /// </summary>
        /// <param name="url">адрес сайта</param>
        /// <returns>True/False</returns>
        public string  ConnectionSite(string url)
        {
            var failedResultCheck = url + " - false";
            
            if (string.IsNullOrEmpty(url))
                return failedResultCheck;

            
            if (!CheckValidUrl(url))
                return failedResultCheck;

            var uri = new Uri(url);
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(uri);
                var response = (HttpWebResponse)request.GetResponse();
                
                if (new HttpResponseMessage(response.StatusCode).IsSuccessStatusCode)
                    return url + " - true";
                
                return failedResultCheck;
            }
            catch
            {
                return failedResultCheck;
            }
        }

        /// <summary>
        /// Проверка на правильный формат url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private bool CheckValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp
                    || uriResult.Scheme == Uri.UriSchemeHttps);
        }
        
        
        /// <summary>
        /// Проверяет пустая ли строка connectionString
        /// если нет, то проверяет доступность сервера mssql 
        /// </summary>
        /// <param name="connectionString">Строка подключения к базе</param>
        /// <returns>True/False</returns>
        public string  ConnectionMsSQL(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                return connectionString + " - false";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                }
            }
            catch
            {
                return connectionString + " - false";
            }

            return connectionString + " - true";
        }
    }
}