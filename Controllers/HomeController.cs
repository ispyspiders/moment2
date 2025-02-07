using Microsoft.AspNetCore.Mvc;
using todo.Models;
using Newtonsoft.Json;
using System.Linq;

namespace TodoApp
{
    public class HomeController : Controller
    {
        // Metod: Läs in todo-lista från session
        private List<Todo> GetTodosFromSession()
        {
            var todosJson = HttpContext.Session.GetString("Todos");
            return string.IsNullOrEmpty(todosJson) ? new List<Todo>() : JsonConvert.DeserializeObject<List<Todo>>(todosJson) ?? new List<Todo>();
        }

        // Metod: Spara todo-lista till session
        private void SaveTodosToSession(List<Todo> todos)
        {
            var todosJson = JsonConvert.SerializeObject(todos);
            HttpContext.Session.SetString("Todos", todosJson);
        }


        // Vy: Se todo-lista.
        public ActionResult Index()
        {
            var todos = GetTodosFromSession();
            var todoCountViewModel = new TodoCount
            {
                TotalTodos = todos.Count,
                CompletedTodos = todos.Count(t => t.IsCompleted)
            };
            // Skicka todo count med ViewData
            ViewData["TodoCount"] = todoCountViewModel;
            return View(todos);
        }

        // Vy: Lägg till ny todo
        [Route("/ny")]
        [HttpGet]
        public IActionResult Add()
        {
            return View(new Todo());
        }

        // Lägg till ny todo
        [Route("/ny")]
        [HttpPost]
        public IActionResult Add(Todo newTodo)
        {
            if (ModelState.IsValid)
            {
                var todos = GetTodosFromSession(); // hämta todos från session
                newTodo.Id = todos.Count > 0 ? todos.Max(t => t.Id) + 1 : 1;

                todos.Add(newTodo);

                SaveTodosToSession(todos); // spara tillbaka todo-listan till sessionen
                return RedirectToAction("Index");
            }
            return View(newTodo);
        }



    }
}
