using MasterCompanyApi.DTO;
using Newtonsoft.Json;

namespace MasterCompanyApi.Utility
{
    public class Util
    {
        public static async Task WriteEmployeesToFile(List<Employee> employees, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                var json = JsonConvert.SerializeObject(employees);
                await writer.WriteAsync(json);
            }
        }
        public static async Task<List<Employee>> ReadEmployeesFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<Employee>();

            using (var reader = new StreamReader(filePath))
            {
                var json = await reader.ReadToEndAsync();
                var employees = JsonConvert.DeserializeObject<List<Employee>>(json);
                return employees ?? new List<Employee>();
            }
        }
        public static bool IsValidDate(string date)
        {
            DateTime parsedDate;
            return DateTime.TryParse(date, out parsedDate);
        }
        public static List<Employee> RemoveDuplicates(List<Employee> employees)
        {
            return employees.GroupBy(e => e.Document).Select(g => g.First()).ToList();
        }
        public static List<Employee> CalculateNewSalaries(List<Employee> employees)
        {
            foreach (var employee in employees)
            {
                if (employee.Salary > 100000)
                {
                    employee.Salary *= 1.25m; // Aumentar el salario en un 25%
                }
                else
                {
                    employee.Salary *= 1.30m; // Aumentar el salario en un 30%
                }
            }
            return employees;
        }
        public static GenderPercentage CalculateGenderPercentage(List<Employee> employees)
        {
            int totalEmployees = employees.Count;
            int maleCount = employees.Count(e => e.Gender == "M");
            int femaleCount = employees.Count(e => e.Gender == "F");

            double malePercentage = (double)maleCount / totalEmployees * 100;
            double femalePercentage = (double)femaleCount / totalEmployees * 100;

            return new GenderPercentage
            {
                MalePercentage = malePercentage,
                FemalePercentage = femalePercentage
            };
        }
    }
}
