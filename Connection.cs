using System;
using System.Data.SqlClient;
using System.Net;

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
        public bool ConnectionSite(string url)
        {
            if (string.IsNullOrEmpty(url))
                return false;

            var uri = new Uri(url);
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(uri);
                request.GetResponse();
            }
            catch
            {
                return false;
            }

            return true;
        }
        
        
        /// <summary>
        /// Проверяет пустая ли строка connectionString
        /// если нет, то проверяет доступность сервера mssql 
        /// </summary>
        /// <param name="connectionString">Строка подключения к базе</param>
        /// <returns>True/False</returns>
        public bool ConnectionMsSQL(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                return false;
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}