using MySql.Data.MySqlClient;

namespace ShortenedLinks.Data
{
    public static class DbInitializer
    {
        public static void Initialize(string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Urls (
                Id INT AUTO_INCREMENT PRIMARY KEY,
                LongUrl TEXT NOT NULL,
                ShortUrl VARCHAR(255) NOT NULL,
                CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                JumpsCount INT NOT NULL DEFAULT 0
            );";

            using var command = new MySqlCommand(createTableQuery, connection);
            command.ExecuteNonQuery();
        }
    }
}
