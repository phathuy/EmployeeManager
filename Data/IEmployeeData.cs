using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Data
{
    /// <summary>
    /// Interface for Methods connecting to Database
    /// </summary>
    interface IEmployeeData
    {
        List<Employee> GetAllEmployees();
        List<Employee> SearchEmployees(int searchOption, string searchValue, List<Department> departments);
        Employee SearchById(string searchValue);
        void DeleteEmployee(int empId, List<Employee> employees);
        List<Department> GetDepartments();
        void AddNewEmployee(string firstName, string lastName, string jobTitle, DateTime hireDate, DateTime dob, string email, decimal salary, string deptId);
        void UpdateEmployee(string searchValue, string firstName, string lastName, string jobTitle, DateTime hireDate, DateTime dob, string email, decimal salary, string deptId);
    }
}
