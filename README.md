# **ShortenedLinks - Сервис сокращения URL**

## **Описание проекта**
Простое веб-приложение для сокращения длинных URL-адресов. Пользователи могут:
- Создавать короткие ссылки (например, `example.com/abc123`).
- Просматривать список всех сокращённых ссылок.
- Редактировать и удалять существующие ссылки.
- Получать статистику переходов.

## **Технологии**
- **Backend**: ASP.NET Core 8.0
- **Frontend**: Razor Pages, Bootstrap 5, JavaScript
- **База данных**: MySQL, MariaDb(10.3)
- **Безопасность**: Защита от SQL-инъекций, XSS, CSRF, Rate Limiting

---

## **Установка и запуск**

### **Требования**
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- MySQL Server (например, [MySQL Community Server](https://dev.mysql.com/downloads/mysql/))
- Git (опционально)

### **1. Клонирование репозитория**
```bash
git clone https://github.com/ваш-репозиторий/ShortenedLinks.git
cd ShortenedLinks
```

### **2. Настройка базы данных**
1. Создайте базу данных:
   ```sql
   CREATE DATABASE shortener_db;
   ```
2. Настройте подключение в `appsettings.json`:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=shortener_db;User=root;Password=ваш-пароль;"
   }
   ```
3. Таблица `Urls` создастся автоматически при первом запуске.

### **3. Запуск приложения**
```bash
dotnet run
```

---

## **Автор**
Максим Поляков 
https://github.com/Pampers432
max23012007@gmail.com 

---

## **Скриншоты**
<img width="1919" height="970" alt="image" src="https://github.com/user-attachments/assets/0675322d-9ce2-4c89-91e7-e05e4be3f828" />

<img width="1919" height="970" alt="image" src="https://github.com/user-attachments/assets/f8dea824-009d-4d83-ac1c-842a57f300f0" />
