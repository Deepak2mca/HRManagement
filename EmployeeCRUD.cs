using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Models
{
   public class EmployeeCRUD
    {
        Dictionary<int, Employee> employeeList = new Dictionary<int, Employee>();
       /// <summary>
       /// class constructor
       /// adding two employee as default
       /// </summary>
        public  EmployeeCRUD()
        {
            employeeList.Add(1, new Employee() { EmployeeID = 1, EmployeeName = "Tom miller",ManagerId=1, TopLowerEmployeeIDs = new HashSet<int> { } });
            employeeList.Add(2, new Employee() { EmployeeID = 2, EmployeeName = "Jason Brazier",ManagerId=0, TopLowerEmployeeIDs = new HashSet<int> { 1} });
        }
 /// <summary>
 /// add and update employee into dectonary collection
 /// </summary>
 /// <param name="emp"></param>
        public void AddUpdateEmployee(Employee emp)
        {
            if (!employeeList.ContainsKey(emp.EmployeeID))
                employeeList.Add(emp.EmployeeID, emp); // add new entry
            else
                employeeList[emp.EmployeeID] = emp; // update entry value
        }
        public Employee GetEmployeeByEMPId(int empid)
        {
            return employeeList.FirstOrDefault(emp => emp.Key == empid).Value; 
        }
       /// <summary>
       /// get all employeelist from dictonary collection
       /// </summary>
       /// <returns></returns>
        public List<Employee> GetEmployees()
        {
            List<Employee> lstOfEmployee = new List<Employee>();
            Dictionary<int, Employee>.ValueCollection values = employeeList.Values;
            foreach (Employee emp in values)
            {
                lstOfEmployee.Add(emp);
            }
            return lstOfEmployee;
        }
        #region deleteing employeebymanagerId
        public bool DeleteEmployeeByEmployeeId(int empid)
        {
            //this function needs to improve
            //there is one case if we are deleting a manager
            //before that assign all employee to either top most manager id or 0  
            
          return employeeList.Remove(empid);
        }
        #endregion
        /// <summary>
       /// Remove employee reporting to a manager
       /// </summary>
       /// <param name="managerid"></param>
        public void DeleteAllEmployeeByManagerId(int managerid)
        {
            Employee manager = employeeList.FirstOrDefault(emp => emp.Key == managerid).Value;  
            foreach(int empid in manager.TopLowerEmployeeIDs)
            {
                DeleteEmployeeByEmployeeId(empid);
            }
        }
        #region changingmanager
        /// <summary>
        /// Remove manager from 
        /// </summary>
        /// <param name="managerid"></param>
        /// <param name="employeeID"></param>
        public void ChangingReportingManager(int managerid, int employeeid, int newManagerID)
        {
            //remove employee from current manager 
            Employee oldmanager = employeeList.FirstOrDefault(emp => emp.Key == managerid).Value;
            oldmanager.TopLowerEmployeeIDs.Remove(employeeid);
            employeeList[managerid] = oldmanager;
            
           
            //assign employee to another manage
            Employee newmanager = employeeList.FirstOrDefault(emp => emp.Key == newManagerID).Value;
            newmanager.TopLowerEmployeeIDs.Add(employeeid);
            employeeList[newManagerID] = newmanager;


            //update employee managerid 
            Employee employee = employeeList.FirstOrDefault(emp => emp.Key == employeeid).Value;
            employee.ManagerId = newManagerID;
            employeeList[employeeid] = employee;
          
        }
        #endregion
        #region GetEmployeeUnderAnManager
       //this variable is declare to hold the employeeid
       //it should come under the function variable not as class variable
        HashSet<int> allEmployeeIds=new HashSet<int>();


/// <summary>
/// get all employee under manager
/// </summary>
/// <param name="managerid"></param>
/// <returns></returns>
        public List<Employee> GetEmployeeListByManagerId(int managerid)
        {
            GetAllEmployeeIDs(managerid);
            //match the employee ids with employee collection and fetch employee.
            var lstEmployee = employeeList.Keys.Where(allEmployeeIds.Contains).Select(x => employeeList[x]).ToList();
            return lstEmployee;
        }
       //purpose of this method to get collection of all employee
        private void GetAllEmployeeIDs(int managerid)
        {
            //get manager details
            var managerdetails = GetEmployeeByEMPId(managerid);
            //add  employeeids into anothercollection
            allEmployeeIds.UnionWith(managerdetails.TopLowerEmployeeIDs);  
            foreach(int empid in managerdetails.TopLowerEmployeeIDs)
            {
                GetAllEmployeeIDs(empid);//to get employee under another employee.
            }
        }

        #endregion

        #region Commented code
        //foreach (int empid in manager.TopLowerEmployeeIDs)
        //    {
        //        if (empid == employeeid)
        //        {

        //            //Employee tobeUpdatedEmployee=GetEmployeeByEMPId(empid);
        //            //tobeUpdatedEmployee.ManagerId = 0;
        //            //employeeList[empid] = tobeUpdatedEmployee;
        //        }
        //    }
        #endregion



    }
}
