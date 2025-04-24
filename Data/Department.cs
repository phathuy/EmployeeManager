using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Data
{
    /// <summary>
    /// Department class
    /// </summary>
    public class Department
    {
        /// <summary>
        /// Property for Department ID
        /// </summary>
        public int DeptId { get; set; }

        /// <summary>
        /// Property for Department name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructor for Department
        /// </summary>
        /// <param name="deptId">Department ID</param>
        /// <param name="name">Department name</param>
        public Department(int deptId, string name)
        {
            DeptId = deptId;
            Name = name;
        }
    }
}
