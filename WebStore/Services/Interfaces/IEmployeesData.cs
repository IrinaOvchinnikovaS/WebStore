﻿using WebStore.Domain.Entities;

namespace WebStore.Services.Interfaces
{
    public interface IEmployeesData
    {
        IEnumerable<Employee> GetAll();

        Employee? GetById(int id);

        bool Edit(Employee employee);

        bool Delete(int id);

        int Add(Employee employee);
    }
}
