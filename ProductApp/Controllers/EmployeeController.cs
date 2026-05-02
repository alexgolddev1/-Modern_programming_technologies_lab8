using Microsoft.AspNetCore.Mvc;
using ProductApp.Models;
using ProductApp.Repository;

namespace ProductApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _repository;

        public EmployeeController(EmployeeRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Employee/GetAllEmpDetails
        [HttpGet("GetAllEmpDetails")]
        public IActionResult GetAllEmpDetails()
        {
            return Ok(_repository.GetAllEmployees());
        }

        // GET: api/Employee/GetEmployeeById/5
        [HttpGet("GetEmployeeById/{id:int}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _repository.GetEmployeeById(id);
            return employee is null ? NotFound() : Ok(employee);
        }

        // POST: api/Employee/AddEmployee
        [HttpPost("AddEmployee")]
        public IActionResult AddEmployee([FromBody] EmployeeModel obj)
        {
            if (ModelState.IsValid)
            {
                int employeeId = _repository.AddEmployee(obj);
                if (employeeId > 0)
                {
                    obj.Empid = employeeId;
                    return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeId }, obj);
                }
            }
            return BadRequest("Failed to add employee");
        }

        // PUT: api/Employee/UpdateEmployee
        [HttpPut("UpdateEmployee")]
        public IActionResult UpdateEmployee([FromBody] EmployeeModel obj)
        {
            if (obj.Empid <= 0)
            {
                return BadRequest("Employee id must be greater than zero.");
            }

            if (ModelState.IsValid)
            {
                if (_repository.UpdateEmployee(obj))
                {
                    return NoContent();
                }

                return NotFound();
            }
            return BadRequest("Failed to update employee");
        }

        // DELETE: api/Employee/DeleteEmployee/5
        [HttpDelete("DeleteEmployee/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            if (_repository.DeleteEmployee(id))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
