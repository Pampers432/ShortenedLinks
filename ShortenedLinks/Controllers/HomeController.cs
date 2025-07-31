using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ShortenedLinks.Models;
using ShortenedLinks.Services;

namespace ShortenedLinks.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connectionString;
        private readonly UrlService _urlService;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _urlService = new UrlService(_connectionString);
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

        [HttpGet("{shortUrl}")]
        public IActionResult RedirectUrl(string shortUrl)
        {
            var urlEntry = _urlService.GetByShortUrl(shortUrl);

            if (urlEntry == null)
            {
                return NotFound();
            }

            _urlService.IncrementClicls(shortUrl);

            return Redirect(urlEntry.LongUrl);
        }



        [HttpGet]
        public IActionResult Create()
        {
            var model = new UrlEntry
            {
                ShortUrl = ShortUrlGenerator.Generate()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UrlEntry model)
        {
            if (!ModelState.IsValid)
                return View(model);

            
            if (_urlService.Exists(model.ShortUrl))
            {
                ModelState.AddModelError("ShortUrl", "“акое сокращение уже существует");
                return View(model);
            }


            model.CreatedDate = DateTime.Now;
            model.JumpsCount = 0;

            _urlService.AddUrl(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("SELECT * FROM Urls WHERE Id = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var url = new UrlEntry
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    LongUrl = reader["LongUrl"].ToString(),
                    ShortUrl = reader["ShortUrl"].ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    JumpsCount = Convert.ToInt32(reader["JumpsCount"])
                };
                return View(url);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UrlEntry model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // ѕровер€ем, не зан€та ли нова€ коротка€ ссылка другим URL
            var existing = _urlService.GetByShortUrl(model.ShortUrl);
            if (existing != null && existing.Id != model.Id)
            {
                ModelState.AddModelError("ShortUrl", "“акое сокращение уже используетс€");
                return View(model);
            }

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand(@"
                UPDATE Urls 
                SET LongUrl = @longUrl, 
                    ShortUrl = @shortUrl 
                WHERE Id = @id", connection);

            command.Parameters.AddWithValue("@longUrl", model.LongUrl);
            command.Parameters.AddWithValue("@shortUrl", model.ShortUrl);
            command.Parameters.AddWithValue("@id", model.Id);

            command.ExecuteNonQuery();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("DELETE FROM Urls WHERE Id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

            return RedirectToAction("Index");
        }
    }
}