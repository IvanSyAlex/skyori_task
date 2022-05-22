using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace TestSkyori
{
    public class SendingEmails
    {
       
        // Адрес отправителя
        private readonly string _addressFrom = "testskyori@yandex.ru";
        // Пароль
        private readonly string _psw = "LCdeD+a!rL4-@pP"; 
        //SMTP
        private readonly string _host = "smtp.yandex.ru";
        //PORT
        private readonly int _port = 587; // Порт
        
        /// <summary>
        /// Отправка письма с файлом на почту
        /// </summary>
        /// <param name="EmailAddress"></param>
        /// <param name="path"></param>
        public void SendEmail(string EmailAddress, string path)
        {
            if (string.IsNullOrEmpty(EmailAddress) || string.IsNullOrEmpty(path))
                return;
            
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(_addressFrom); // Адрес отправителя
            mail.To.Add(new MailAddress(EmailAddress)); // Адрес получателя
            mail.Subject = "Проверка подключений";
            mail.Body = "Pезультат последней проверки";

            mail.Attachments.Add(new Attachment(path)); //Вложенный файл

            SmtpClient client = new SmtpClient();
            client.Host = _host;
            client.Port = _port;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(_addressFrom, _psw); // Ваши логин и пароль 
            client.Send(mail); // Отправка сообщения
        }
    }
}