using EmployeeAdminPortal.Repository;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.DTOs;
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

  [HttpGet]
  [Route("{id:guid}")]
  public IActionResult GetEmployeeById(Guid id)
  {
    Employee? employee = _dbContext.Employees.Find(id);

    if (employee is null)
    {
      return NotFound();
    }

    return Ok(employee);
  }

  [HttpPost]
  public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
  {
    var employeeEntity = new Employee()
    {
      Name = addEmployeeDto.Name,
      Email = addEmployeeDto.Email,
      Phone = addEmployeeDto.Phone,
      Salary = addEmployeeDto.Salary
    };

    _dbContext.Employees.Add(employeeEntity);
    _dbContext.SaveChanges();

    return Ok(employeeEntity);
  }

  [HttpPut]
  [Route("{id:guid}")]
  public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
  {
    Employee? employee = _dbContext.Employees.Find(id);

    if (employee is null)
    {
      return NotFound();
    }

    employee.Name = updateEmployeeDto.Name;
    employee.Email = updateEmployeeDto.Email;
    employee.Phone = updateEmployeeDto.Phone;
    employee.Salary = updateEmployeeDto.Salary;

    _dbContext.SaveChanges();

    return Ok(employee);
  }

  [HttpDelete]
  [Route("{id:guid}")]
  public IActionResult DeleteEmployee(Guid id)
  {
    Employee? employee = _dbContext.Employees.Find(id);

    if (employee is null)
    {
      return NotFound();
    }

    _dbContext.Employees.Remove(employee);
    _dbContext.SaveChanges();

    return Ok(employee);
  }
}