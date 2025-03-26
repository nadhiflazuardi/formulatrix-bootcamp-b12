using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
  private readonly ApplicationDbContext _dbContext;

  public EmployeesController(ApplicationDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  [HttpGet]
  public IActionResult GetAllEmployees()
  {
    List<Employee> allEmployees = _dbContext.Employees.ToList();

    return Ok(allEmployees);
  }

  [HttpPost]
  public IActionResult AddEmployee()
  {
    
  }
}