# Chatter

Простой чат на ado.net с возможностью регистрации, авторизации и подтверждения по email.

## Установка

Для запуска приложения необходимо:

1. В appsettings.json указать строку подключения DefaultConnection базы даных sql
2. В Консоль диспетчера пакетов NuGet прописать:
   ```
   Update-Database -Context ApplicationContext
   Update-Database -Context UserContext
   ```
3. Открыть класс Controllers.Services.EmailService и изменить поля:
   ```
   private static string smtpLogin = "LOGIN";
   private static string smtpAccess = "ACCESS_KEY";
   ```
  где вместо LOGIN и ACCESS_KEY указать собственные логин и пароль gmail приложения сервиса smtp.
  
## Примечания

* При входе необходимо пройти регистрацию с подтверждением через email. Сообщение может попасть в спам.
* Любой пользователь может создавать любые темы и писать любые сообщения, кроме пустого текста.
* Каждый пользователь может редактировать и удалять только свои сообщения.
* Тема будет удалена если в ней не останется ни одного сообщения.

Ссылка на рабочий проект: [chatter](https://chatter20200420143225.azurewebsites.net)

## Используемые технологии

* ASP.NET Core;
* Microsoft Entity Framework;
* MSSQL Server;.
* Bootstrap;
* Microsoft Visual Studio.

