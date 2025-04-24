using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Data
{
    /// <summary>
    /// Exception class used to throw success messages
    /// </summary>
    public class SuccessfulException : ApplicationException
    {
        public SuccessfulException() { }

        public SuccessfulException(string message) : base(message) { }
    }
}
