using EmployeeAdminPortal.Models;

namespace EmployeeAdminPortal.Repository;

public interface IEmployeeRepository
{
  Task<List<Employee>> GetAll();
  Task<Employee?> GetById(Guid employeeId);
  Task Add(Employee employee);
  Task Update(Employee employee);
  Task Delete(Guid employeeId);
}
