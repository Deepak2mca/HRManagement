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
        public  EmployeeCRUD()
        {
            employeeList.Add(1, new Employee() { EmployeeID = 1, EmployeeName = "Tom miller",ManagerId=1, TopLowerEmployeeIDs = new HashSet<int> { } });
            employeeList.Add(2, new Employee() { EmployeeID = 2, EmployeeName = "Jason Brazier",ManagerId=0, TopLowerEmployeeIDs = new HashSet<int> { 1} });
        }
 
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

        public bool DeleteEmployeeByEmployeeId(int empid)
        {
          return employeeList.Remove(empid);
        }
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
           
            //assign employee to another manage
            Employee newmanager = employeeList.FirstOrDefault(emp => emp.Key == newManagerID).Value;
            newmanager.TopLowerEmployeeIDs.Add(employeeid);

            //update employee managerid 
            Employee employee = employeeList.FirstOrDefault(emp => emp.Key == employeeid).Value;
            employee.ManagerId = 0;            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="managerid"></param>
        HashSet<int> allEmployeeIds=new HashSet<int>();



        public List<Employee> GetEmployeeListByManagerId(int managerid)
        {
            GetAllEmployeeIDs(managerid);          
            var lstEmployee = employeeList.Keys.Where(allEmployeeIds.Contains).Select(x => employeeList[x]).ToList();
            return lstEmployee;
        }
        private void GetAllEmployeeIDs(int managerid)
        {
            
            var managerdetails = GetEmployeeByEMPId(managerid);
            allEmployeeIds.UnionWith(managerdetails.TopLowerEmployeeIDs);  
            foreach(int empid in managerdetails.TopLowerEmployeeIDs)
            {
                GetAllEmployeeIDs(empid);
            }
        }


        //foreach (int empid in manager.TopLowerEmployeeIDs)
        //    {
        //        if (empid == employeeid)
        //        {

        //            //Employee tobeUpdatedEmployee=GetEmployeeByEMPId(empid);
        //            //tobeUpdatedEmployee.ManagerId = 0;
        //            //employeeList[empid] = tobeUpdatedEmployee;
        //        }
        //    }


        
    }
}