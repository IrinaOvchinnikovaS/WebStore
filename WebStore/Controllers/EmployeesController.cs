using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.Data;

namespace WebStore.Controllers
{
    //[Route("empl/[action]/{id?}")]
    //[Route("Staff/{action=Index}/{id?}")]
    public class EmployeesController : Controller
    {
        private ICollection<Employee> _employees;

        public EmployeesController()
        {
            _employees = TestData.Employees;
        }
        public IActionResult Index()
        {
            return View(_employees);
        }

        //[Route("~/employees/info-{id}")]
        public IActionResult Details(int id)
        {
            ViewData["TestValue"] = 123;

            var employee = _employees.FirstOrDefault(e => e.Id == id);

            if (employee is null)
                return NotFound();

            ViewBag.SelectedEmployee = employee;

            return View(employee);
        }

       // public IActionResult Create() =>View();

        public IActionResult Edit(int id) => View();

        public IActionResult Delete(int id) => View();
    }
}
