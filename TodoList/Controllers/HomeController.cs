using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using TodoList.Models;
using TodoList.Models.ViewModels;

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
            var todoListViewModel = GetAllTodos();
            return View(todoListViewModel);
        }

        internal TodoViewModel GetAllTodos()
        {
            List<TodoItem> todoList = new List<TodoItem>();

            using (var connection = new SqliteConnection("data source=tododb.sqlite"))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = "Select * FROM todo";

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                todoList.Add(new TodoItem { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                            }
                        }
                        else // returning empty viewmodel
                        {
                            return new TodoViewModel { TodoList = todoList };
                        }
                    }
                }
            }
            return new TodoViewModel { TodoList = todoList };
        }

        public RedirectResult Insert(TodoItem todo)
        {
            using (SqliteConnection connection = new SqliteConnection("data source=tododb.sqlite"))
            {
                using (SqliteCommand tableCmd = connection.CreateCommand())
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