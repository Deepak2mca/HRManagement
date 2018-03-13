using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Models
{
   public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int ManagerId { get; set; }
        public HashSet<int> TopLowerEmployeeIDs{ get; set; }
    }
}
