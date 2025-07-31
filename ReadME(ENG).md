# **ShortenedLinks - URL Shortening Service**

## **Project Description**  
A simple web application for shortening long URLs. Users can:  
- Create short links (e.g., `example.com/abc123`).  
- View a list of all shortened links.  
- Edit and delete existing links.  
- Track click statistics.  

## **Technologies**  
- **Backend**: ASP.NET Core 8.0  
- **Frontend**: Razor Pages, Bootstrap 5, JavaScript  
- **Database**: MySQL, MariaDB (10.3)  
- **Security**: Protection against SQL injection, XSS, CSRF, Rate Limiting  

---

## **Installation & Setup**  

### **Requirements**  
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)  
- MySQL Server (e.g., [MySQL Community Server](https://dev.mysql.com/downloads/mysql/))  
- Git (optional)  

### **1. Clone the Repository**  
```bash
git clone https://github.com/your-repository/ShortenedLinks.git
cd ShortenedLinks
```

### **2. Database Setup**  
1. Create a database:  
   ```sql
   CREATE DATABASE shortener_db;
   ```
2. Configure the connection in `appsettings.json`:  
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=shortener_db;User=root;Password=your-password;"
   }
   ```
3. The `Urls` table will be created automatically on first launch.  

### **3. Run the Application**  
```bash
dotnet run
```
The app will be available at: [https://localhost:5001](https://localhost:5001)  

---

## **Author**  
Maxim Polyakov  
https://github.com/Pampers432  
max23012007@gmail.com  

---

## **Screenshots**  
<img width="1919" height="970" alt="Main Page" src="https://github.com/user-attachments/assets/0675322d-9ce2-4c89-91e7-e05e4be3f828" />  

<img width="1919" height="970" alt="Create Form" src="https://github.com/user-attachments/assets/f8dea824-009d-4d83-ac1c-842a57f300f0" />
