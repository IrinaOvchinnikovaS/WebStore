using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.ViewModels;
using WebStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WebStore.Controllers
{
    //[Route("empl/[action]/{id?}")]
    //[Route("Staff/{action=Index}/{id?}")]
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;
        private readonly ILogger<EmployeesController> _Logger;

        public EmployeesController(IEmployeesData EmployeesData, ILogger<EmployeesController> Logger)
        {
            _EmployeesData = EmployeesData;
            _Logger = Logger;
        }
        public IActionResult Index()
        {
            var employeesData = _EmployeesData.GetAll();
            return View(employeesData);
        }

        //[Route("~/employees/info-{id}")]
        public IActionResult Details(int id)
        {
            ViewData["TestValue"] = 123;

            var employee = _EmployeesData.GetById(id);

            if (employee is null)
                return NotFound();

            ViewBag.SelectedEmployee = employee;

            return View(employee);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View("Edit", new EmployeeViewModel());

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return View(new EmployeeViewModel());

            var employee = _EmployeesData.GetById((int)id);

            if (employee is null)
            {
                _Logger.LogWarning("При редактировании сотрудника с id {0} он не был найден", id);
                return NotFound();
            }

            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                Name = employee.FirstName,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
                Education = employee.Education,
                WorkExperience = employee.WorkExperience,
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel Model)
        {
            if (Model.LastName == "Усама" && Model.Name == "Бен" && Model.Patronymic == "Ладен")
                ModelState.AddModelError("", "Террористов на работу не берём!");

            if (!ModelState.IsValid)
                return View(Model);

            var employee = new Employee
            {
                Id = Model.Id,
                LastName = Model.LastName,
                FirstName = Model.Name,
                Patronymic = Model.Patronymic,
                Age = Model.Age,
                Education = Model.Education,
                WorkExperience = Model.WorkExperience,
            };

            if(Model.Id == 0)
            {
                _Logger.LogInformation("Создан новый сотрудник {0}", employee);
                _EmployeesData.Add(employee);
            }
                
            else if(!_EmployeesData.Edit(employee))
            {
                _Logger.LogInformation("Информация о сотруднике {0} НЕ изменена", employee);
                return NotFound();
            }
                

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            if (id < 0)
                return BadRequest();

            var employee = _EmployeesData.GetById(id);

            if (employee is null)
                return NotFound();

            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                Name = employee.FirstName,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
                Education = employee.Education,
                WorkExperience = employee.WorkExperience,
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!_EmployeesData.Delete(id))
                return NotFound();

            _Logger.LogInformation("Сотрудник с id {0} удалён", id);

            return RedirectToAction("Index");
        }
    }
}
