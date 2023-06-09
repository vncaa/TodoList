﻿using Microsoft.AspNetCore.Mvc;
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

		[HttpPost]
		public JsonResult Delete(int id)
		{
			using (var connection = new SqliteConnection("datasource=tododb.sqlite"))
			{
				using (var tableCmd = connection.CreateCommand())
				{
					connection.Open();
					tableCmd.CommandText = $"DELETE FROM todo WHERE Id = '{id}'";
					tableCmd.ExecuteNonQuery();
				}

			}
			return Json(new { });
		}

		public RedirectResult Update(TodoItem todo)
		{
			using (var connection = new SqliteConnection("data source=tododb.sqlite"))
			{
				using (var tableCmd = connection.CreateCommand())
				{
					connection.Open();
					tableCmd.CommandText = $"UPDATE todo SET Name = '{todo.Name}' WHERE Id = '{todo.Id}'";
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

		[HttpGet]
		public JsonResult PopulateForm(int id)
		{
			var todo = GetById(id);
			return Json(todo);
		}

		internal TodoItem GetById(int id)
		{
			TodoItem todo = new TodoItem();

			using (var connection = new SqliteConnection("data source=tododb.sqlite"))
			{
				using (var tableCmd = connection.CreateCommand())
				{
					connection.Open();
					tableCmd.CommandText = $"SELECT * FROM todo WHERE Id = '{id}'";

					using (var reader = tableCmd.ExecuteReader())
					{
						if (reader.HasRows)
						{
							reader.Read();
							todo.Id = reader.GetInt32(0);
							todo.Name = reader.GetString(1);
						}
						else
							return todo;
					}
				}
			}
			return todo;
		}
	}
}