using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Employee> _employees = new()
        {
            new Employee { Id = 1, LastName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", Age = 25, Education = "ПГНИУ Мехмат", WorkExperience = "Прогноз, 2019-2021" },
            new Employee { Id = 2, LastName = "Петров", FirstName = "Пётр", Patronymic = "Петрович", Age = 29, Education = "ПНИПУ электротех", WorkExperience = "Xolla, 2020-н.в." },
            new Employee { Id = 3, LastName = "Сидоров", FirstName = "Сидор", Patronymic = "Сидорович", Age = 23, Education = "ПГНИУ физфак", WorkExperience = "Завод Шпагина, 2020-н.в." }
        };

        public IActionResult Index()
        {
            //return Content("Данные из первого контроллера");
            return View();
        }

        public string ConfiguredAction(string id, string value)
        {
            return $"Hello World! {id} - {value}";
        }

        public IActionResult Employees(string id)
        {
            int idInt = Convert.ToInt32(id);
            if(idInt == 0)
                return View(_employees);

            Employee employee = _employees.Find(e => e.Id == idInt);
            return View("Employee", employee);
        }
    }
}
