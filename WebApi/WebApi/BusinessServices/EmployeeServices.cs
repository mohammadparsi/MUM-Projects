using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AutoMapper;
using BusinessEntities;
using DataModel;
using DataModel.UnitOfWork;

namespace BusinessServices
{
    /// <summary>
    /// Offers services for employee specific CRUD operations
    /// </summary>
    public class EmployeeServices:IEmployeeServices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public EmployeeServices()
        {
            _unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Fetches employee details by id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public BusinessEntities.EmployeeEntity GetEmployeeById(string employeeId)
        {
            var employee = _unitOfWork.EmployeeRepository.GetByID(employeeId);
            if (employee != null)
            {
                Mapper.CreateMap<Employee, EmployeeEntity>();
                var employeeModel = Mapper.Map<Employee, EmployeeEntity>(employee);
                return employeeModel;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the employees.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.EmployeeEntity> GetAllEmployees()
        {
            var employees = _unitOfWork.EmployeeRepository.GetAll().ToList();
            if (employees.Any())
            {
                Mapper.CreateMap<Employee, EmployeeEntity>();
                var employeesModel = Mapper.Map<List<Employee>, List<EmployeeEntity>>(employees);
                return employeesModel;
            }
            return null;
        }

        /// <summary>
        /// Creates a employee
        /// </summary>
        /// <param name="employeeEntity"></param>
        /// <returns></returns>
        public int CreateEmployee(BusinessEntities.EmployeeEntity employeeEntity)
        {
            using (var scope = new TransactionScope())
            {
                var employee = new Employee
                {
                
                    Id = employeeEntity.Id,
                FirstName = employeeEntity.FirstName,
                LastName = employeeEntity.LastName,
                FICAPercentage = employeeEntity.FICAPercentage,
                NetPay = employeeEntity.NetPay,
                PostTaxPercentage = employeeEntity.PostTaxPercentage,
                PreTaxPercentage = employeeEntity.PreTaxPercentage,
                TaxesPercentage = employeeEntity.TaxesPercentage,
                Salary = employeeEntity.Salary,
                
                };
                _unitOfWork.EmployeeRepository.Insert(employee);
                _unitOfWork.Save();
                scope.Complete();
                return 1;
            }
        }

        /// <summary>
        /// Updates a employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="employeeEntity"></param>
        /// <returns></returns>
        public bool UpdateEmployee(string employeeId, BusinessEntities.EmployeeEntity employeeEntity)
        {
            var success = false;
            if (employeeEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var employee = _unitOfWork.EmployeeRepository.GetByID(employeeId);
                    if (employee != null)
                    {
                        employee.FirstName = employeeEntity.FirstName;
                        employee.LastName = employeeEntity.LastName;
                        employee.FICAPercentage = employeeEntity.FICAPercentage;
                        employee.NetPay = employeeEntity.NetPay;
                        employee.PostTaxPercentage = employeeEntity.PostTaxPercentage;
                        employee.PreTaxPercentage = employeeEntity.PreTaxPercentage;
                        employee.TaxesPercentage = employeeEntity.TaxesPercentage;

                        _unitOfWork.EmployeeRepository.Update(employee);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public bool DeleteEmployee(string employeeId)
        {
            var success = false;
            //if (employeeId > 0)
            //{
                using (var scope = new TransactionScope())
                {
                    var employee = _unitOfWork.EmployeeRepository.GetByID(employeeId);
                    if (employee != null)
                    {

                        _unitOfWork.EmployeeRepository.Delete(employee);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            //}
            return success;
        }
    }
}
