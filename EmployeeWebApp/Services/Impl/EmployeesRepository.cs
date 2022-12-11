using EmployeeWebApp.Models;

namespace EmployeeWebApp.Services.Impl
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly List<Employee> _employees;
        private int _maxFreeId;
        public EmployeesRepository() 
        {
            _employees = Enumerable.Range(1, 10)
                .Select(i => new Employee
                {
                    Id= i,
                    FirstName= "Имя - " + i.ToString(),
                    LastName = "Фамилия - " + i.ToString(),
                    Patronymic = "Отчество - " + i.ToString(),
                    Birthday = DateTime.Now.AddYears( -18 - i)
                }).ToList();
            _maxFreeId = _employees.Max(i => i.Id) + 1;
        }

        public IEnumerable<Employee> GetAll() => _employees;
        public int Add(Employee employee)
        {
            employee.Id = _maxFreeId;
            _maxFreeId++;
            _employees.Add(employee);
            return employee.Id;
        }

        public bool Edit(Employee employee)
        {
            var db_item = GetById(employee.Id);
            if (db_item is null)
                return false;
            db_item.LastName = employee.LastName;
            db_item.FirstName = employee.FirstName;
            db_item.Patronymic = employee.Patronymic;
            db_item.Birthday = employee.Birthday;

            return true;
        }      

        public Employee? GetById(int id) => _employees.FirstOrDefault(e => e.Id == id);

        public bool Remove(int id)
        {
            var db_item = GetById(id);
            if (db_item is null)
                return false;
            _employees.Remove(db_item);

            return true;
        }
    }
}
