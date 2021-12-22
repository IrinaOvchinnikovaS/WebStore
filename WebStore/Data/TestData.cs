using WebStore.Models;

namespace WebStore.Data
{
    public static class TestData
    {
        public static List<Employee> Employees { get; } = new()
        {
            new Employee { Id = 1, LastName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", Age = 25, Education = "ПГНИУ Мехмат", WorkExperience = "Прогноз, 2019-2021" },
            new Employee { Id = 2, LastName = "Петров", FirstName = "Пётр", Patronymic = "Петрович", Age = 29, Education = "ПНИПУ электротех", WorkExperience = "Xolla, 2020-н.в." },
            new Employee { Id = 3, LastName = "Сидоров", FirstName = "Сидор", Patronymic = "Сидорович", Age = 23, Education = "ПГНИУ физфак", WorkExperience = "Завод Шпагина, 2020-н.в." }
        };
    }
}
