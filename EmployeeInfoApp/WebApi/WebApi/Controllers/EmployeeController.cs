using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using BusinessServices;
using System.Web.Http.Cors;

namespace WebApi.Controllers
{

    [EnableCors(origins: "http://localhost:4208", headers: "*", methods: "*")]
    public class EmployeeController : ApiController
    {

        private readonly IEmployeeServices _employeeServices;

         #region Public Constructor

        /// <summary>
        /// Public constructor to initialize employee service instance
        /// </summary>
        public EmployeeController()
        {
            _employeeServices =new EmployeeServices();
        }

        #endregion

        // GET api/employee
          public HttpResponseMessage Get()
        {
            var employees = _employeeServices.GetAllEmployees();
            if (employees != null)
            {
                var employeeEntities = employees as List<EmployeeEntity> ?? employees.ToList();
                if (employeeEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, employeeEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "employees not found");
        }

        // GET api/employee/5
        public HttpResponseMessage Get(string id)
        {
            var employee = _employeeServices.GetEmployeeById(id);
            if (employee != null)
                return Request.CreateResponse(HttpStatusCode.OK, employee);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No employee found for this id");
        }

        // POST api/employee
        public HttpResponseMessage Post([FromBody] EmployeeEntity employeeEntity)
        {
            
            if (ModelState.IsValid)
            {
                
                _employeeServices.CreateEmployee(employeeEntity);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse
                    (HttpStatusCode.BadRequest, ModelState);
            }

             
        }

        // PUT api/employee/5
        public bool Put(string id, [FromBody]EmployeeEntity employeeEntity)
        {
            
            
                return _employeeServices.UpdateEmployee(id, employeeEntity);
            
        }

        // DELETE api/employee/5
        public bool Delete(string id)
        {
            
                return _employeeServices.DeleteEmployee(id);
            
        }
    }
}
