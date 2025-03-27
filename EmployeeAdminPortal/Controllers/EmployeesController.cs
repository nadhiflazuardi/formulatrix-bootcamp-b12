using EmployeeAdminPortal.Repository;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeAdminPortal.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EmployeesController : ControllerBase
{
  private readonly IEmployeeRepository _employeeRepository;
  private readonly IMapper _mapper;
  private readonly IValidator<EmployeeCreateDTO> _createValidator;
  private readonly IValidator<EmployeeUpdateDTO> _updateValidator;

  public EmployeesController
  (
      IEmployeeRepository employeeRepository,
      IMapper mapper,
      IValidator<EmployeeCreateDTO> createValidator,
      IValidator<EmployeeUpdateDTO> updateValidator)
  {
    _employeeRepository = employeeRepository;
    _mapper = mapper;
    _createValidator = createValidator;
    _updateValidator = updateValidator;
  }

  [HttpGet]
  public async Task<ActionResult<List<EmployeeDTO>>> GetAllEmployees()
  {
    List<Employee> allEmployees = await _employeeRepository.GetAll();
    List<EmployeeDTO> employeeDTOs = _mapper.Map<List<EmployeeDTO>>(allEmployees);
    return Ok(employeeDTOs);
  }

  [HttpGet("{id:guid}")]
  public async Task<ActionResult> GetEmployeeById(Guid id)
  {
    Employee? employee = await _employeeRepository.GetById(id);
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
    var validationResult = await _createValidator.ValidateAsync(employeeCreateDTO);
    if (!validationResult.IsValid)
    {
      return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
    }

    Employee employee = _mapper.Map<Employee>(employeeCreateDTO);
    await _employeeRepository.Add(employee);

    EmployeeDTO employeeDTO = _mapper.Map<EmployeeDTO>(employee);
    return Ok(employeeDTO);
  }

  [HttpPut("{id:guid}")]
  public async Task<ActionResult> UpdateEmployee(Guid id, EmployeeUpdateDTO employeeUpdateDTO)
  {
    var validationResult = await _updateValidator.ValidateAsync(employeeUpdateDTO);
    if (!validationResult.IsValid)
    {
      return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
    }

    Employee? existingEmployee = await _employeeRepository.GetById(id);
    if (existingEmployee is null)
    {
      return NotFound();
    }

    _mapper.Map(employeeUpdateDTO, existingEmployee);
    await _employeeRepository.Update(existingEmployee);

    return Accepted();
  }

  [HttpDelete("{id:guid}")]
  public async Task<ActionResult> DeleteEmployee(Guid id)
  {
    Employee? employee = await _employeeRepository.GetById(id);
    if (employee is null)
    {
      return NotFound();
    }

    await _employeeRepository.Delete(id);
    return NoContent();
  }
}

