using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            //ControllerContext.HttpContext.Request.RouteValues

            //return Content("Данные из первого контроллера");
            return View();
        }

        public string ConfiguredAction(string id, string value)
        {
            return $"Hello World! {id} - {value}";
        }

        public void Throw(string msg) => throw new ApplicationException(msg);
    }
}
