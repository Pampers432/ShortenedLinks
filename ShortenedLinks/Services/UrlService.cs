using MySql.Data.MySqlClient;
using ShortenedLinks.Models;

namespace ShortenedLinks.Services
{
    public class UrlService
    {
        private readonly string _connectionString;

        public UrlService(string connectionString)
        {
            _connectionString = connectionString;
        }


        public UrlEntry? GetByShortUrl(string shortUrl)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("SELECT * FROM Urls WHERE ShortUrl = @shortUrl", connection);
            command.Parameters.AddWithValue("@shortUrl", shortUrl);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new UrlEntry
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    LongUrl = reader["LongUrl"].ToString(),
                    ShortUrl = reader["ShortUrl"].ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    JumpsCount = Convert.ToInt32(reader["JumpsCount"])
                };
            }

            return null;
        }

        public void AddUrl(UrlEntry url)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand(@"
            INSERT INTO Urls (LongUrl, ShortUrl, CreatedDate, JumpsCount)
            VALUES (@longUrl, @shortUrl, @createdDate, @jumpsCount)", connection);

            command.Parameters.AddWithValue("@longUrl", url.LongUrl);
            command.Parameters.AddWithValue("@shortUrl", url.ShortUrl);
            command.Parameters.AddWithValue("@createdDate", url.CreatedDate);
            command.Parameters.AddWithValue("@jumpsCount", url.JumpsCount);

            command.ExecuteNonQuery();
        }

        public bool Exists(string shortUrl)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("SELECT COUNT(*) FROM Urls WHERE ShortUrl = @shortUrl", connection);
            command.Parameters.AddWithValue("@shortUrl", shortUrl);

            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }

        public void IncrementClicls(string shortUrl)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("UPDATE Urls SET JumpsCount = JumpsCount + 1 WHERE ShortUrl = @shortUrl", connection);
            command.Parameters.AddWithValue("@shortUrl", shortUrl);

            command.ExecuteNonQuery();
        }
    }
}
