using EmployeeWebApp.Models;

namespace EmployeeWebApp.Services
{
    public interface IEmployeesRepository
    {
        IEnumerable<Employee> GetAll();
        Employee? GetById(int id);
        int Add(Employee employee);
        bool Edit(Employee employee);
        bool Remove(int id);
    }
}
