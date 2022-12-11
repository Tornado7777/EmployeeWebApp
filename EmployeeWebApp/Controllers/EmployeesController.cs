using EmployeeWebApp.Models;
using EmployeeWebApp.Services;
using EmployeeWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace EmployeeWebApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesRepository _employeesRepository;
        public EmployeesController(IEmployeesRepository employeesRepository) 
        {
            _employeesRepository = employeesRepository;
        }
        public IActionResult Index()
        {
            var employees =_employeesRepository.GetAll();
            return View(employees);
        }

        public IActionResult Detail(int Id)
        {
            var employee = _employeesRepository.GetById(Id);
            if (employee is null)
                return NotFound();

            return View(new EmployeesViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Patronymic = employee.Patronymic,
                Birthday = employee.Birthday,
            });
        }

        public IActionResult Create()
        {
            return View("Edit", new EmployeesViewModel());
        }

        [HttpPost]
        public IActionResult Edit(EmployeesViewModel model)
        {
            if (model.LastName == "Иванов" && model.FirstName == "Иван" && model.Patronymic == "Иванович")
                ModelState.AddModelError("", "Слишком банальное ФИО ");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var employee = new Employee
            {
                Id = model.Id,
                LastName = model.LastName,
                FirstName = model.FirstName,
                Patronymic = model.Patronymic,
                Birthday = model.Birthday,
            };

            if (employee.Id == 0)
            {
                var id = _employeesRepository.Add(employee);
                return RedirectToAction("Detail", new {id});
            }

            var success = _employeesRepository.Edit(employee);

            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return View("Edit", new EmployeesViewModel());

            var employee = _employeesRepository.GetById((int) id);
            if (employee == null)
                return NotFound();

            return View(new EmployeesViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                Birthday = employee.Birthday
            });
        }

        public IActionResult Delete(int id)
        {
            var employee = _employeesRepository.GetById(id);
            if (employee == null)
                return NotFound();
            return View(new EmployeesViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Patronymic = employee.Patronymic,
                Birthday = employee.Birthday,
            });

        }

        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            if (!_employeesRepository.Remove(id))
                return NotFound();
            return RedirectToAction("Index");
        }
    }
}
