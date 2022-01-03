using WebStore.Data;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InMemory
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly ILogger<InMemoryEmployeesData> _Logger;
        private readonly ICollection<Employee> _Employees;
        private int _MaxFreeId;

        public InMemoryEmployeesData(ILogger<InMemoryEmployeesData> Logger) 
        {
            _Logger = Logger;
            _Employees = TestData.Employees;
            _MaxFreeId = _Employees.DefaultIfEmpty().Max(e => e?.Id ?? 0) + 1;
        }

        public IEnumerable<Employee> GetAll() => _Employees;

        public Employee? GetById(int id) => _Employees.FirstOrDefault(x => x.Id == id);

        public int Add(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (_Employees.Contains(employee)) //в БД не надо
                return employee.Id;

            employee.Id = _MaxFreeId++;
            _Employees.Add(employee);

            return employee.Id;
        }

        public bool Edit(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (_Employees.Contains(employee)) //в БД не надо
                return true;

            var db_employee = GetById(employee.Id);
            if(db_employee is null)
            {
                _Logger.LogWarning("Попытка Изменить отсутствующего сотрудника с Id {0}", employee.Id);
                return false;
            }
                

            db_employee.FirstName = employee.FirstName;
            db_employee.LastName = employee.LastName;
            db_employee.Patronymic = employee.Patronymic;
            db_employee.FirstName = employee.FirstName;
            db_employee.Age = employee.Age;
            db_employee.Education = employee.Education;
            db_employee.WorkExperience = employee.WorkExperience;

            //когда будет БД, не забыть вызывать SaveChanges()

            _Logger.LogInformation("Изменение информации о сотруднике с Id {0}", employee.Id);

            return true;

        }

        public bool Delete(int id)
        {
            var employee = GetById(id);
            if (employee is null)
            {
                _Logger.LogWarning("Попытка удалить отсутствующего сотрудника с Id {0}", id);
                return false;
            }    
                
            _Employees.Remove(employee);
            _Logger.LogInformation("Сотрудник с id:{0} был успешно удалён", employee.Id);
            return true;

        }
    }
}
