using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ShortenedLinks.Models;

namespace ShortenedLinks.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IActionResult Index()
        {
            var Urls = new List<UrlEntry>();

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = "SELECT * FROM Urls ORDER BY CreatedDate DESC";
            var command = new MySqlCommand(query, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Urls.Add(new UrlEntry
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    LongUrl = reader["LongUrl"].ToString(),
                    ShortUrl = reader["ShortUrl"].ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    JumpsCount = Convert.ToInt32(reader["JumpsCount"])
                });
            }

            return View(Urls);
        }
    }
}
