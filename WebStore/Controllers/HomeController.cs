using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //return Content("Данные из первого контроллера");
            return View();
        }

        public string ConfiguredAction(string id, string value)
        {
            return $"Hello World! {id} - {value}";
        }
    }
}
