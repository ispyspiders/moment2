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
            // Skicka meddelande med viewBag
            ViewBag.Message = "Välkommen till Att göra-applikationen!";

            // Läs in todos från session
            var todos = GetTodosFromSession();
            // Läs in Todo count
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

        [HttpPost]
        public IActionResult MarkComplete(int id)
        {
            var todos = GetTodosFromSession(); // läs in todos från session
            var todo = todos.FirstOrDefault(t => t.Id == id); // hitta todo med rätt id

            if (todo != null)
            {
                todo.IsCompleted = !todo.IsCompleted; // omvänd status
                SaveTodosToSession(todos); // spara uppdaterad lista
            }
            return RedirectToAction("Index"); //återgå till todo-lista
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var todos = GetTodosFromSession(); //Läs in todos från session
            var todo = todos.FirstOrDefault(t => t.Id == id); // hitta todo med rätt id
            if (todo != null)
            {
                todos.Remove(todo); // ta bort todo
                SaveTodosToSession(todos); // spara uppdaterad lista
            }
            return RedirectToAction("Index"); //återgå till todo-lista
        }


    }
}
