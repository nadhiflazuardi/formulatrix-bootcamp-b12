namespace EmployeeAdminPortal.DTOs;

public class EmployeeDTO {
  public required string Name { get; set; }
  public required string Email { get; set; }
  public string? Phone { get; set; }
}