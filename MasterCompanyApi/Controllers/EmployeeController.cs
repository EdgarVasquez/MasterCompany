using MasterCompanyApi.DTO;
using MasterCompanyApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterCompanyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _employeeService;

        public EmployeeController(IEmployeeServices employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet("increaseSalaries")]
        public async Task<IActionResult> IncreaseSalaries()
        {
            string json = await _employeeService.IncreaseSalaries();
            return Ok(json);
        }

        [HttpGet("genderPercentage")]
        public async Task<IActionResult> GetGenderPercentage()
        {
            var percentage = await _employeeService.GetGenderPercentage();
            return Ok(percentage);
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            List<Employee> employees = await _employeeService.GetEmployees();
            return Ok(employees);
        }

        [HttpGet("salaryRange")]
        public async Task<IActionResult> GetEmployeesBySalaryRange(decimal minSalary=0, decimal maxSalary = 0)
        {
            List<Employee> employees = await _employeeService.GetEmployeesBySalaryRange(minSalary, maxSalary);
            return Ok(employees);
        }

        [HttpGet("duplicates")]
        public async Task<IActionResult> GetUniqueEmployees()
        {
            List<Employee> employees = await _employeeService.GetUniqueEmployees();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            Employee addedEmployee = await _employeeService.AddEmployee(employee);
            return Ok(addedEmployee);
        }

        [HttpDelete("{document}")]
        public async Task<IActionResult> DeleteEmployee(string document)
        {
            bool result = await _employeeService.DeleteEmployee(document);
            if (result)
                return Ok();
            else
                return NotFound();
        }
    }
}
