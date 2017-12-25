using System.Collections.Generic;
using BusinessEntities;

namespace BusinessServices
{
    /// <summary>
    /// Employee Service Contract
    /// </summary>
    public interface IEmployeeServices
    {
        EmployeeEntity GetEmployeeById(string EmployeeId);
        IEnumerable<EmployeeEntity> GetAllEmployees();
        int CreateEmployee(EmployeeEntity EmployeeEntity);
        bool UpdateEmployee(string EmployeeId,EmployeeEntity EmployeeEntity);
        bool DeleteEmployee(string EmployeeId);
    }
}
