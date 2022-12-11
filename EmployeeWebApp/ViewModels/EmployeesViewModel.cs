using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EmployeeWebApp.ViewModels
{
    public class EmployeesViewModel : IValidatableObject
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Фаимилия является обязательной")]
        [Display(Name ="Фаимлия")]
        [StringLength(100,MinimumLength =2, ErrorMessage ="Минимальная длина строки 2, масимальная 100.")]
        [RegularExpression(@"([А-ЯЁ][а-яё\-0-9]+)|([A-Z][a-z]+)", ErrorMessage = "Строка имела неверный формат.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Имя является обязательной")]
        [Display(Name = "Имя")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Минимальная длина строки 2, масимальная 100.")]
        [RegularExpression(@"([А-ЯЁ][а-яё\-0-9]+)|([A-Z][a-z]+)", ErrorMessage = "Строка имела неверный формат.")]
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (LastName == "Иванов" && (DateTime.UtcNow.Year - Birthday.Year <= 30))
                yield return new ValidationResult("Иванов должен быть старше 30 лет.", new[] { nameof(LastName), nameof(Birthday)});

            yield return ValidationResult.Success!;
        }
    }
}
