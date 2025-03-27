using EmployeeAdminPortal.DTOs;
using FluentValidation;

namespace EmployeeAdminPortal.Validators;

public class EmployeeCreateDTOValidator : AbstractValidator<EmployeeCreateDTO>
{
  public EmployeeCreateDTOValidator()
  {
    RuleFor(x => x.Name)
        .NotEmpty().WithMessage("Name is required")
        .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

    RuleFor(x => x.Email)
        .NotEmpty().WithMessage("Email is required")
        .EmailAddress().WithMessage("Invalid email format");

    RuleFor(x => x.Salary)
        .GreaterThan(0).WithMessage("Salary must be greater than 0");

    RuleFor(x => x.Phone)
        .NotEmpty().WithMessage("Phone number is required")
        .Matches(@"^\d{10,15}$").WithMessage("Phone number must be between 10-15 digits");
  }
}
