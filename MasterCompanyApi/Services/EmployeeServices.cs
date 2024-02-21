using MasterCompanyApi.DTO;
using MasterCompanyApi.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MasterCompanyApi.Services
{
    public interface IEmployeeServices
    {
        Task<List<Employee>> GetEmployees();
        Task<List<Employee>> GetEmployeesBySalaryRange(decimal minSalary, decimal maxSalary);
        Task<List<Employee>> GetUniqueEmployees();
        Task<Employee> AddEmployee(Employee employee);
        Task<bool> DeleteEmployee(string document);
        Task<string> IncreaseSalaries();

        Task<GenderPercentage> GetGenderPercentage();

    }

    public class EmployeeServices : IEmployeeServices
    {
        private readonly string filePath = Path.Combine("models", "Data.txt");
        public async Task<GenderPercentage> GetGenderPercentage()
        {
            List<Employee> employees = await Util.ReadEmployeesFromFile(filePath);
            return Util.CalculateGenderPercentage(employees);
        }
        
        public async Task<string> IncreaseSalaries()
        {
            List<Employee> employees = await Util.ReadEmployeesFromFile(filePath);
            var updatedEmployees = Util.CalculateNewSalaries(employees);
            return JsonConvert.SerializeObject(updatedEmployees);
        }
        
        public async Task<List<Employee>> GetEmployees()
        {
            List<Employee> employees = await Util.ReadEmployeesFromFile(filePath);
            // Validate start dates
            foreach (var employee in employees)
            {
                if (!Util.IsValidDate(employee.StartDate))
                {
                    // Set StartDate to "FechaInvalida" if it's not a valid date
                    employee.StartDate = "Fecha Invalida "+ employee.StartDate.ToString();
                }
                employees = Util.RemoveDuplicates(employees);
            }
            return employees;
        }
       
        public async Task<List<Employee>> GetEmployeesBySalaryRange(decimal minSalary, decimal maxSalary)
        {
            var employees = await Util.ReadEmployeesFromFile(filePath);
            return employees.Where(e => e.Salary >= minSalary && e.Salary <= maxSalary).ToList();
        }

        public async Task<List<Employee>> GetUniqueEmployees()
        {
            var employees = await Util.ReadEmployeesFromFile(filePath);
            return employees.GroupBy(e => e.Document).Select(g => g.First()).ToList();
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var employees = await Util.ReadEmployeesFromFile(filePath);
            employees.Add(employee);
            await Util.WriteEmployeesToFile(employees, filePath);
            return employee;
        }

        public async Task<bool> DeleteEmployee(string document)
        {
            var employees = await Util.ReadEmployeesFromFile(filePath);
            var employeeToDelete = employees.FirstOrDefault(e => e.Document == document);
            if (employeeToDelete == null)
                return false;

            employees.Remove(employeeToDelete);
            await Util.WriteEmployeesToFile(employees,filePath);
            return true;
        }

       

      
    }
}
