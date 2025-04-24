using Microsoft.Maui.ApplicationModel.Communication;
using MySqlConnector;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;

namespace EmployeeProject.Data
{
    /// <summary>
    /// Manager Class for Connecting to Database
    /// </summary>
    public class EmployeeManager : IEmployeeData
    {
        /// <summary>
        /// String Builder for Connection String
        /// </summary>
        MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
        {
            Server = "localhost",
            UserID = "root",
            Password = "password",
            Database = "employee",
        };

        /// <summary>
        /// Retrieve All Departments from Database
        /// </summary>
        /// <returns>A List of All Departments</returns>
        public List<Department> GetDepartments()
        {
            // New departments list
            List<Department> departments = new();

            MySqlConnection connection = new MySqlConnection(builder.ConnectionString);
            try
            {
                connection.Open();

                // SELECT command string
                string sql = "SELECT * FROM department";
                MySqlCommand command = new MySqlCommand(sql, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    // Read all rows from executing SQL query
                    // Create new Department for each row and add to the list 'departments'
                    while (reader.Read())
                    {
                        int deptId = reader.GetInt32(0);
                        string name = reader.GetString(1);

                        Department temp = new Department(deptId, name);
                        departments.Add(temp);
                    }
                }

                return departments;
            }
            catch (MySqlException)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Retrieve All Employees from Database
        /// </summary>
        /// <returns>A List of All Employees</returns>
        public List<Employee> GetAllEmployees()
        {
            // New employees list
            List<Employee> employees = new();

            MySqlConnection connection = new MySqlConnection(builder.ConnectionString);
            try
            {
                connection.Open();

                // SELECT command string
                string sql = "SELECT * FROM employee";
                MySqlCommand command = new MySqlCommand(sql, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    // Read all rows from executing SQL query
                    // Create new Employee for each row and add to the list 'employees'
                    while (reader.Read())
                    {
                        // USING COLUMN INDEX               
                        int empId = reader.GetInt32(0);
                        string firstName = reader.GetString(1);
                        string lastName = reader.GetString(2);
                        string jobTitle = reader.GetString(3);
                        DateTime hireDate = reader.GetDateTime(4);
                        DateTime doB = reader.GetDateTime(5);
                        string email = reader.GetString(6);
                        decimal salary = reader.GetDecimal(7);
                        int deptId = reader.GetInt32(8);

                        Employee temp = new Employee(empId, firstName, lastName, jobTitle, hireDate, doB, email, salary, deptId);

                        employees.Add(temp);
                    }
                }
                connection.Close();
                return employees;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Retrieve All Employees Matching Conditions from Database
        /// </summary>
        /// <param name="searchOption">Radio button value</param>
        /// <param name="searchValue">Input field value</param>
        /// <param name="departments">List of Departments</param>
        /// <returns>A List of Employees</returns>
        public List<Employee> SearchEmployees(int searchOption, string searchValue, List<Department> departments)
        {
            List<Employee> employees = new();

            // If NO radio button selected, throw error message
            if (searchOption == 0)
            {
                throw new Exception("Please select the type of search!");
            }

            // If user enter nothing in the search field, throw error message
            if (string.IsNullOrWhiteSpace(searchValue) && searchOption != 6)
            {
                throw new Exception("Please enter a value to search!");
            }

            MySqlConnection connection = new MySqlConnection(builder.ConnectionString);
            try
            {
                connection.Open();

                string sql = "";        // storing SELECT command string
                string job_title = "";  // storing Job title

                // Assign sql query string correspond to the selected radio button
                switch (searchOption)
                {
                    case 1: // Search by Employee ID
                        sql = $"SELECT * FROM employee WHERE emp_id={searchValue}";
                        break;
                    case 2: // Search by Last name
                        sql = $"SELECT * FROM employee WHERE LOWER(emp_lname)=\"{searchValue.ToLower()}\"";
                        break;
                    case 3: // Search by Job title
                        if (searchValue == "1")
                        {
                            job_title = "Professor";
                        }
                        else if (searchValue == "2")
                        {
                            job_title = "Lecturer";
                        }
                        else if (searchValue == "3")
                        {
                            job_title = "Associate Professor";
                        }
                        else if (searchValue == "4")
                        {
                            job_title = "Assistant Professor";
                        }
                        else
                        {
                            job_title = searchValue;
                        }
                        sql = $"SELECT * FROM employee WHERE LOWER(job_title) LIKE '{job_title.ToLower()}'";
                        break;
                    case 4: // Search by Department ID
                        if (int.Parse(searchValue) < 1 || int.Parse(searchValue) > departments.Count())
                        {
                            // Throw error message for invalid Department ID
                            throw new Exception("Invalid Department ID!");
                        }
                        sql = $"SELECT * FROM employee WHERE dept_id={searchValue}";
                        break;
                    case 5: // Search by Year Hired
                        sql = $"SELECT * FROM employee WHERE YEAR(hire_date)={searchValue}";
                        break;
                    case 6: // Search by Year Hired
                        sql = $"SELECT * FROM employee ORDER BY emp_id DESC";
                        break;
                }

                MySqlCommand command = new MySqlCommand(sql, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    // Read all rows from executing SQL query
                    // Create new Employee for each row and add to the list 'employees'
                    while (reader.Read())
                    {
                        // USING COLUMN INDEX
                        int empId = reader.GetInt32(0);
                        string firstName = reader.GetString(1);
                        string lastName = reader.GetString(2);
                        string jobTitle = reader.GetString(3);
                        DateTime hireDate = reader.GetDateTime(4);
                        DateTime doB = reader.GetDateTime(5);
                        string email = reader.GetString(6);
                        decimal salary = reader.GetDecimal(7);
                        int deptId = reader.GetInt32(8);

                        Employee temp = new Employee(empId, firstName, lastName, jobTitle, hireDate, doB, email, salary, deptId);

                        employees.Add(temp);
                    }
                }
                return employees;
            }
            catch (MySqlException)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Retrieve An Employees When Search by ID
        /// </summary>
        /// <param name="searchValue">Input value for ID</param>
        /// <returns>An Employee object</returns>
        public Employee SearchById(string searchValue)
        {
            // New employee object
            Employee emp = new();

            MySqlConnection connection = new MySqlConnection(builder.ConnectionString);
            try
            {
                connection.Open();

                // SELECT command string
                string sql = $"select * from employee where emp_id={searchValue}";

                MySqlCommand command = new MySqlCommand(sql, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    // Read data row from executing query
                    // Create new Employee from data retrieved
                    while (reader.Read())
                    {
                        // USING COLUMN INDEX
                        string firstName = reader.GetString(1);
                        string lastName = reader.GetString(2);
                        string jobTitle = reader.GetString(3);
                        DateTime hireDate = reader.GetDateTime(4);
                        DateTime dob = reader.GetDateTime(5);
                        string email = reader.GetString(6);
                        decimal salary = reader.GetDecimal(7);
                        int deptId = reader.GetInt32(8);

                        emp = new Employee(int.Parse(searchValue), firstName, lastName, jobTitle, hireDate, dob, email, salary, deptId);
                    }
                }
                return emp;
            }
            catch (MySqlException)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Delete An Employee from Database
        /// </summary>
        /// <param name="deleteId">ID of deleting Employee</param>
        /// <param name="employees">List of Employees</param>
        public void DeleteEmployee(int deleteId, List<Employee> employees)
        {
            MySqlConnection connection = new MySqlConnection(builder.ConnectionString);
            try
            {
                connection.Open();

                // DELETE command string
                string sql = $"DELETE FROM employee WHERE emp_id = {deleteId}";

                MySqlTransaction transaction = connection.BeginTransaction();
                MySqlCommand delete = new MySqlCommand(sql, connection, transaction);

                try
                {
                    delete.ExecuteNonQuery();
                    transaction.Commit();

                    // Remove from employees list
                    foreach (var emp in employees)
                    {
                        if (emp.EmpId == deleteId)
                        {
                            employees.Remove(emp);
                        }
                    }
                }
                catch (MySqlException)
                {
                    transaction.Rollback();
                }
            }
            catch (MySqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Add a new Employee to Database through input
        /// </summary>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="jobTitle">Job title</param>
        /// <param name="hireDate">Hire date</param>
        /// <param name="dob">Date of birth</param>
        /// <param name="email">Email</param>
        /// <param name="salary">Salary</param>
        /// <param name="deptId">Department ID</param>
        public void AddNewEmployee(string firstName, string lastName, string jobTitle, DateTime hireDate, DateTime dob, string email, decimal salary, string deptId)
        {
            // Convert DateTime to string "yyyy-MM-dd" for INSERT command
            string dob_string = dob.ToString("yyyy-MM-dd");
            string hireDate_string = hireDate.ToString("yyyy-MM-dd");

            MySqlConnection connection = new MySqlConnection(builder.ConnectionString);
            try
            {
                connection.Open();

                // INSERT command string
                string sql = $"INSERT INTO employee (emp_fname, emp_lname, job_title, hire_date, dob, email, salary, dept_id) " +
                    $"VALUES ('{firstName}', '{lastName}', '{jobTitle}', '{hireDate_string}', '{dob_string}', '{email}', {salary}, {deptId});";
                
                MySqlTransaction transaction = connection.BeginTransaction();
                MySqlCommand insert = new MySqlCommand(sql, connection, transaction);

                try
                {
                    insert.ExecuteNonQuery();
                    transaction.Commit();

                    // Pass success message
                    throw new SuccessfulException("New employee has been added!");
                }
                catch (MySqlException)
                {
                    transaction.Rollback();
                }

            }
            catch (MySqlException)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Update An Employee's data
        /// </summary>
        /// <param name="searchValue">Hold value of Employee ID from Search</param>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="jobTitle">Job title</param>
        /// <param name="hireDate">Hire date</param>
        /// <param name="dob">Date of birth</param>
        /// <param name="email">Email</param>
        /// <param name="salary">Salary</param>
        /// <param name="deptId">Department ID</param>
        public void UpdateEmployee(string searchValue, string firstName, string lastName, string jobTitle, DateTime hireDate, DateTime dob, string email, decimal salary, string deptId)
        {
            // Convert DateTime to string "yyyy-MM-dd" for UPDATE command
            string dob_string = dob.ToString("yyyy-MM-dd");
            string hireDate_string = hireDate.ToString("yyyy-MM-dd");

            MySqlConnection connection = new MySqlConnection(builder.ConnectionString);
            try
            {
                connection.Open();

                // UPDATE command string
                string sql = $"UPDATE employee SET emp_fname = '{firstName}', emp_lname = '{lastName}', job_title = '{jobTitle}', hire_date = '{hireDate_string}'," +
                             $"dob = '{dob_string}', email = '{email}', salary = {salary}, dept_id = {deptId} " +
                             $"WHERE emp_id = {searchValue};";

                MySqlTransaction transaction = connection.BeginTransaction();
                MySqlCommand update = new MySqlCommand(sql, connection, transaction);
                try
                {
                    update.ExecuteNonQuery();
                    transaction.Commit();

                    // Pass success message
                    throw new SuccessfulException("Employee updated!");
                }
                catch (MySqlException)
                {
                    transaction.Rollback();
                }

            }
            catch (MySqlException)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
