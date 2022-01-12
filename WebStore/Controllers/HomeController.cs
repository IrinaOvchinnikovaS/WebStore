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

        //public IActionResult Employees(string id)
        //{
        //    int idInt = Convert.ToInt32(id);
        //    if(idInt == 0)
        //        return View(_employees);

        //    Employee employee = _employees.Find(e => e.Id == idInt);
        //    return View("Employee", employee);
        //}
    }
}
