using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebApi.Models;

namespace MyFirstWebApi.Controllers
{
    [Authorize(Roles = "Admin,POC")] 
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> _employees = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                Name = "John Doe",
                Salary = 50000,
                Permanent = true,
                Department = new Department { Id = 1, Name = "HR" },
                Skills = new List<Skill>
                {
                    new Skill { Id = 1, Name = "C#" },
                    new Skill { Id = 2, Name = "SQL" }
                },
                DateOfBirth = new DateTime(1990, 1, 1)
            },
            new Employee
            {
                Id = 2,
                Name = "Jane Smith",
                Salary = 60000,
                Permanent = false,
                Department = new Department { Id = 2, Name = "Finance" },
                Skills = new List<Skill>
                {
                    new Skill { Id = 3, Name = "Excel" }
                },
                DateOfBirth = new DateTime(1992, 6, 10)
            }
        };

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Employee>> GetAll()
        {
            return Ok(_employees);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Employee> CreateEmployee([FromBody] Employee newEmp)
        {
            newEmp.Id = _employees.Max(e => e.Id) + 1;
            _employees.Add(newEmp);
            return Ok(newEmp);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Employee> UpdateEmployee([FromBody] Employee updatedEmp)
        {
            if (updatedEmp == null || updatedEmp.Id <= 0)
                return BadRequest("Invalid employee id");

            var existingEmp = _employees.FirstOrDefault(e => e.Id == updatedEmp.Id);
            if (existingEmp == null)
                return BadRequest("Invalid employee id");

            existingEmp.Name = updatedEmp.Name;
            existingEmp.Salary = updatedEmp.Salary;
            existingEmp.Permanent = updatedEmp.Permanent;
            existingEmp.Department = updatedEmp.Department;
            existingEmp.Skills = updatedEmp.Skills;
            existingEmp.DateOfBirth = updatedEmp.DateOfBirth;

            return Ok(existingEmp);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteEmployee(int id)
        {
            var emp = _employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
                return NotFound();

            _employees.Remove(emp);
            return Ok($"Employee with ID {id} deleted successfully.");
        }
    }
}
