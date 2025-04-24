using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Data
{
    /// <summary>
    /// Employee class
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Property for Employee ID
        /// </summary>
        public int EmpId {  get; set; }

        /// <summary>
        /// Property for Employee's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Property for Employee's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Property for Employee's job title
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// Property for Employee's hire date
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        /// Property for Employee's birthdate
        /// </summary>
        public DateTime DoB {  get; set; }

        /// <summary>
        /// Property for Employee's email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Property for Employee's salary
        /// </summary>
        public decimal Salary { get; set; }

        /// <summary>
        /// Property for Employee's department ID
        /// </summary>
        public int DeptID { get; set; }

        /// <summary>
        /// Constructor for Employee
        /// </summary>
        public Employee() { }

        /// <summary>
        /// Constructor for Employee
        /// </summary>
        /// <param name="empId">Employee ID</param>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="jobTitle">Job title</param>
        /// <param name="hireDate">Hire date</param>
        /// <param name="doB">Date of birth</param>
        /// <param name="email">Email</param>
        /// <param name="salary">Salary</param>
        /// <param name="deptID">Department ID</param>
        public Employee (int empId, string firstName, string lastName, string jobTitle, DateTime hireDate, DateTime doB, string? email, decimal salary, int deptID)
        {
            EmpId = empId;
            FirstName = firstName;
            LastName = lastName;
            JobTitle = jobTitle;
            HireDate = hireDate;
            DoB = doB;
            Email = email;
            Salary = salary;
            DeptID = deptID;
        }
    }
}
