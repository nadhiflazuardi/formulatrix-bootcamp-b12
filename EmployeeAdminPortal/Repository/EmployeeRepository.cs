using EmployeeAdminPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Repository;

public class EmployeeRepository : IEmployeeRepository
{
  private readonly ApplicationDbContext _context;

  public EmployeeRepository(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<List<Employee>> GetAll()
  {
    return await _context.Employees.AsNoTracking().ToListAsync();
  }

  public async Task<Employee?> GetById(Guid id)
  {
    return await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
  }

  public async Task Add(Employee employee)
  {
    await _context.Employees.AddAsync(employee);
    await _context.SaveChangesAsync();
  }

  public async Task Update(Employee employee)
  {
    var existingEmployee = await _context.Employees.FindAsync(employee.Id);
    if (existingEmployee is null)
    {
      return;
    }

    _context.Entry(existingEmployee).CurrentValues.SetValues(employee);
    await _context.SaveChangesAsync();
  }

  public async Task Delete(Guid employeeId)
  {
    var employee = await _context.Employees.FindAsync(employeeId);
    if (employee is not null)
    {
      _context.Employees.Remove(employee);
      await _context.SaveChangesAsync();
    }
  }
}
