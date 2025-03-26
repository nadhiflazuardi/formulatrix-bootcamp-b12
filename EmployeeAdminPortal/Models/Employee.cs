using System.ComponentModel.DataAnnotations;

namespace EmployeeAdminPortal.Models;

public class Employee
{
  [Key]
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public required string Email { get; set; }
  public string? Phone { get; set; }
  public decimal Salary { get; set; }
}