using EmployeeWebAPI2.Models;
using EmployeeWebAPI2.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace EmployeeWebAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        //Injection de dépendance du service IEmployeeRepository
        private readonly IEmployeeRepository employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                return Ok(await employeeRepository.GetEmployees());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var result = await employeeRepository.GetEmployee(id);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        [HttpPost]

        /*public  async Task<Employee> GetEmployeeByEmail(string email) 
        {
            return await AppDbContext.Employee.FirstOrDefaultAsync(e => e.Email == email);
        */
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }
                else
                // Add custom model validation error
                {
                    var emp = await employeeRepository.GetEmployeeByEmail(employee.Email);
                    if (emp != null)
                    {
                        ModelState.AddModelError("email", "Employee email already in use");
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        var createdEmployee = await employeeRepository.AddEmployee(employee);
                        return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.EmployeeId },
                        createdEmployee);
                    }
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
        [HttpPut("id:int")]

        public async Task<ActionResult<Employee>> UpdateEmployee(int id , Employee employee) 
        {
            try 
            {
                if (id != employee.EmployeeId) 
                {
                    return BadRequest("Employee ID mistach")
                ;
                }

                var employeToUpdate = await employeeRepository.GetEmployee(id);

                if(employeToUpdate != null) 
                {
                    return NotFound($"Employee with id {id} not found");
                }
                return await  employeeRepository.UpdateEmployee(employee);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Updating  data");
            }
        }
        [HttpDelete("{id:int}")]

        public async Task<ActionResult<Employee>> DeleteEmployee(int id) 
        {
            try 
            {
                var employeToUpdate = await employeeRepository.GetEmployee(id);
                if(employeToUpdate == null) 
                {
                    return NotFound($"Employee with  Id ={id} Not Found ");
                }

                return await employeeRepository.DeleteEmployee(id);

            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to deleting");
            }
        }
        [HttpGet("{search}")]

        public async Task<ActionResult<IEnumerable<Employee>>> Search (string name ,Gender gender)
               {
            try 
            {
                var result = await employeeRepository.Search(name, gender);
                if(result.Any()) 
                {
                    return Ok(result);
                }
                return NotFound();

            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retreving data from  the data base");
            }
                 }
    }
}
