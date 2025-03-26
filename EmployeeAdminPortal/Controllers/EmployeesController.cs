using EmployeeAdminPortal.Repository;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
  public async Task<IActionResult> GetAllEmployees()
  {
    List<Employee> allEmployees = await _dbContext.Employees.ToListAsync();
    return Ok(allEmployees);
  }

  [HttpGet("{id:guid}")]
  public async Task<IActionResult> GetEmployeeById(Guid id)
  {
    Employee? employee = await _dbContext.Employees.FindAsync(id);

    if (employee is null)
    {
      return NotFound();
    }

    return Ok(employee);
  }

  [HttpPost]
  public async Task<IActionResult> AddEmployee(AddEmployeeDto addEmployeeDto)
  {
    var employeeEntity = new Employee()
    {
      Name = addEmployeeDto.Name,
      Email = addEmployeeDto.Email,
      Phone = addEmployeeDto.Phone,
      Salary = addEmployeeDto.Salary
    };

    await _dbContext.Employees.AddAsync(employeeEntity);
    await _dbContext.SaveChangesAsync();

    return Ok(employeeEntity);
  }

  [HttpPut("{id:guid}")]
  public async Task<IActionResult> UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
  {
    Employee? employee = await _dbContext.Employees.FindAsync(id);

    if (employee is null)
    {
      return NotFound();
    }

    employee.Name = updateEmployeeDto.Name;
    employee.Email = updateEmployeeDto.Email;
    employee.Phone = updateEmployeeDto.Phone;
    employee.Salary = updateEmployeeDto.Salary;

    await _dbContext.SaveChangesAsync();

    return Ok(employee);
  }

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteEmployee(Guid id)
  {
    Employee? employee = await _dbContext.Employees.FindAsync(id);

    if (employee is null)
    {
      return NotFound();
    }

    _dbContext.Employees.Remove(employee);
    await _dbContext.SaveChangesAsync();

    return Ok(employee);
  }
}
