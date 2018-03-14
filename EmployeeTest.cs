using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Employee.Models;

namespace Employee.Tests
{
    [TestClass]
   public class EmployeeTest
    {
        
        /// <summary>
        /// Add and update employeeList
        /// </summary>
        [TestMethod]
        public void AddUpdateEmployee()
        {
            // arrange  
            // arrange  
            EmployeeCRUD objEmplyeeCRUD = new EmployeeCRUD();
            var emp = new Employee.Models.Employee()
            {
                EmployeeID = 3,
                EmployeeName = "Tom miller",
                ManagerId = 0,
                TopLowerEmployeeIDs = new HashSet<int> { }
            };

            // act  
            objEmplyeeCRUD.AddUpdateEmployee(emp);
            List<Employee.Models.Employee> lstEmployees= objEmplyeeCRUD.GetEmployees();
            var savedEmp=  lstEmployees.Find(x => x.EmployeeID == 3);


            // assert  
            
            Assert.AreEqual(savedEmp.EmployeeName, emp.EmployeeName);  

                        
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void RemoveAllEmployeeReportingToManager()
        {
            // arrange  
            EmployeeCRUD objEmplyeeCRUD = new EmployeeCRUD();
            var emp1 = new Employee.Models.Employee()
            {
                EmployeeID = 3,
                EmployeeName = "Tom miller3",
                ManagerId = 4,
                TopLowerEmployeeIDs = new HashSet<int> { }
            };
            var emp2 = new Employee.Models.Employee()
            {
                EmployeeID = 4,
                EmployeeName = "Tom miller4",
                ManagerId = 0,
                TopLowerEmployeeIDs = new HashSet<int> {3,5 }
            };

            var emp3 = new Employee.Models.Employee()
            {
                EmployeeID = 5,
                EmployeeName = "Tom miller5",
                ManagerId = 4,
                TopLowerEmployeeIDs = new HashSet<int> { }
            };


            // act 
            //adding above employee object into collection
            objEmplyeeCRUD.AddUpdateEmployee(emp1);
            objEmplyeeCRUD.AddUpdateEmployee(emp2);
            objEmplyeeCRUD.AddUpdateEmployee(emp3);

            // deleting all employee who has manager "Tom miller4"
            // employee 3 and 5 will removed after execution of function.
            objEmplyeeCRUD.DeleteAllEmployeeByManagerId(4);

            //assert is pending
          

            
        }
        [TestMethod]
        public void ChaingingReportingOfAnEmployee()
        {
            EmployeeCRUD objEmplyeeCRUD = new EmployeeCRUD();
            var emp1 = new Employee.Models.Employee()
            {
                EmployeeID = 3,
                EmployeeName = "Tom miller3",
                ManagerId = 0,
                TopLowerEmployeeIDs = new HashSet<int> { }
            };
            //Employee ID 3 is reporting to emp 4 
            var emp2 = new Employee.Models.Employee()
            {
                EmployeeID = 4,
                EmployeeName = "Tom miller4",
                ManagerId = 0,
                TopLowerEmployeeIDs = new HashSet<int> { 3 }
            };
           
            var emp3 = new Employee.Models.Employee()
            {
                EmployeeID = 5,
                EmployeeName = "Tom miller5",
                ManagerId = 0,
                TopLowerEmployeeIDs = new HashSet<int> {  }
            };

            // act 
            //adding above employee object into collection
            objEmplyeeCRUD.AddUpdateEmployee(emp1);
            objEmplyeeCRUD.AddUpdateEmployee(emp2);
            objEmplyeeCRUD.AddUpdateEmployee(emp3);

            // currently employee "Tom miller3" is reporting to "Tom miller4"
            // now I am changing employee "Tom miller3" is reporting to "Tom miller5"
            objEmplyeeCRUD.ChangingReportingManager(4, 3, 5);

            //assert
            //assert is pending 

        }

        [TestMethod]

        public void GetAllEmployeeofAnManager()
        {
            EmployeeCRUD objEmplyeeCRUD = new EmployeeCRUD();
            var emp1 = new Employee.Models.Employee()
            {
                EmployeeID = 3,
                EmployeeName = "Tom miller3",
                ManagerId = 0,
                TopLowerEmployeeIDs = new HashSet<int> {1,2 }
            };
            //Employee ID 3 is reporting to emp 4 
            var emp2 = new Employee.Models.Employee()
            {
                EmployeeID = 4,
                EmployeeName = "Tom miller4",
                ManagerId = 0,
                TopLowerEmployeeIDs = new HashSet<int> { 3 }
            };
           
            var emp3 = new Employee.Models.Employee()
            {
                EmployeeID = 5,
                EmployeeName = "Tom miller5",
                ManagerId = 0,
                TopLowerEmployeeIDs = new HashSet<int> { 4 }
            };

            // act 
            //adding above employee object into collection
            objEmplyeeCRUD.AddUpdateEmployee(emp1);
            objEmplyeeCRUD.AddUpdateEmployee(emp2);
            objEmplyeeCRUD.AddUpdateEmployee(emp3);

            //employeeIds 1,2,3,4 get selected
            List<Employee.Models.Employee> lst = objEmplyeeCRUD.GetEmployeeListByManagerId(5);

            //assert
            //assert is pending
            //all hirarchical employee will get selected.
            
        }
    }
}
