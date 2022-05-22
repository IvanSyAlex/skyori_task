# skyori_task
Консольное приложение, для проверки доступа к сайту и БД

Результат проверки заносится в файл "Checking_Access.json" в директории расположения exe файла приложения.

Параметры для запуска приложения:
"-Print"  - вывести результат последней проверки из сохраненного файла.
"-EmailTo" - адрес почты куда нужно отправить письмо
"-Site" - адрес сайта
"-ConnectionDb" - строка подключения к БД


# Examples
## run application with parameters
.\TestSkyori -EmailTo "sychyov-86@mail.ru" -Site "https://yandex.ru/" -ConnectionDb "Server=LAPTOP-H2QHE71S\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True"

## file result
[
  {
    "VerificationDate": "2022-05-22T22:26:14.1170889+05:00",
    "UrlSite": "https://yandex.ru/",
    "ConnectSiteResult": true,
    "StringConnectionDb": "Server=LAPTOP-H2QHE71S\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True",
    "ConnectMsSqlResult": true
  },
  {
    "VerificationDate": "2022-05-22T22:27:04.0393957+05:00",
    "UrlSite": "https://yandex.ru/",
    "ConnectSiteResult": true,
    "StringConnectionDb": "Server=LAPTOP-H2QHE71S\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True",
    "ConnectMsSqlResult": true
  }
]
