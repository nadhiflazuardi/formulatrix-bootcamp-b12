using EmployeeAdminPortal.Repository;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace EmployeeAdminPortal.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
  private readonly ApplicationDbContext _dbContext;
  private readonly IMapper _mapper;

  public EmployeesController(ApplicationDbContext dbContext, IMapper mapper)
  {
    _dbContext = dbContext;
    _mapper = mapper;
  }

  [HttpGet]
  public async Task<ActionResult<List<EmployeeDTO>>> GetAllEmployees()
  {
    List<Employee> allEmployees = await _dbContext.Employees.AsNoTracking().ToListAsync();
    List<EmployeeDTO> employeeDTOs = _mapper.Map<List<EmployeeDTO>>(allEmployees);
    return Ok(employeeDTOs);
  }

  [HttpGet("{id:guid}")]
  public async Task<ActionResult> GetEmployeeById(Guid id)
  {
    Employee? employee = await _dbContext.Employees.AsNoTracking().FirstAsync(employee => employee.Id == id);

    if (employee is null)
    {
      return NotFound();
    }

    EmployeeDTO employeeDTO = _mapper.Map<EmployeeDTO>(employee);

    return Ok(employeeDTO);
  }

  [HttpPost]
  public async Task<ActionResult> AddEmployee(EmployeeCreateDTO employeeCreateDTO)
  {
    Employee employee = _mapper.Map<Employee>(employeeCreateDTO);

    await _dbContext.Employees.AddAsync(employee);
    await _dbContext.SaveChangesAsync();

    EmployeeDTO employeeDTO = _mapper.Map<EmployeeDTO>(employee);

    return Ok(employeeDTO);
  }

  [HttpPut("{id:guid}")]
  public async Task<ActionResult> UpdateEmployee(Guid id, EmployeeUpdateDTO employeeUpdateDTO)
  {
    Employee? employee = await _dbContext.Employees.FirstAsync(employee => employee.Id == id);

    if (employee is null)
    {
      return NotFound();
    }

    _mapper.Map(employeeUpdateDTO, employee);
    await _dbContext.SaveChangesAsync();

    EmployeeDTO employeeDTO = _mapper.Map<EmployeeDTO>(employee);

    return Accepted();
  }

  [HttpDelete("{id:guid}")]
  public async Task<ActionResult> DeleteEmployee(Guid id)
  {
    Employee? employee = await _dbContext.Employees.AsNoTracking().FirstAsync(employee => employee.Id == id);

    if (employee is null)
    {
      return NotFound();
    }

    _dbContext.Employees.Remove(employee);
    await _dbContext.SaveChangesAsync();

    return NoContent ();
  }
}
