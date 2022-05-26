# skyori_task
Консольное приложение, для проверки доступа к сайту и БД</br>

Результат проверки заносится в файл "Checking_Access.json" в директории расположения exe файла приложения.

Параметры для запуска приложения:</br>
"-Print"  - вывести результат последней проверки из сохраненного файла.</br>

Экран внесения данных:</br>
![image](https://user-images.githubusercontent.com/74009917/170487122-3cf36832-bc4e-4d04-a09f-0f3f80fe25b2.png)


После внесения необходимых данных для начала проверки (email, адрес сайта, строку подключения к БД),</br> 
появляется возможеость изменять email и добавлять сайты и строки БД, для проврки.</br>
![image](https://user-images.githubusercontent.com/74009917/170487616-61838b17-18a4-4dad-b6dc-7bd0b739ae12.png)



# Examples

## file result
[
  {
    "VerificationDate": "2022-05-26T17:06:02.3039318+05:00",
    "ConnectSiteResult": [
      "https://hakim.se/404 - false",
      "https://tproger.ru/devnull/best-404-notfound-pages/ - true"
    ],
    "ConnectMsSqlResult": [
      "Server=LAPTOP-H2QHE71S\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True - true",
    ]
  }
]
