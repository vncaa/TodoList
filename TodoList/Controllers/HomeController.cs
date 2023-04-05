using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public RedirectResult Insert(TodoItem todo)
        {
            using(SqliteConnection connection = new SqliteConnection("data source=tododb.sqlite"))
            {
                using(SqliteCommand tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"INSERT INTO todo (Name) VALUES ('{todo.Name}')";
                    try
                    {
                        tableCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return Redirect("https://localhost:7253/");
        }
    }
}