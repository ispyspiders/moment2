using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

        [Route("/ny")]
        public IActionResult Add()
        {
            return View();
        }

    }
}
