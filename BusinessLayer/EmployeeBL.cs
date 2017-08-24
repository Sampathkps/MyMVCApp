using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccesLayer;
using BusinessEntities;

namespace BusinessLayer
{
    public class EmployeeBL
    {
        public List<Employee> GetEmployee()
        {
            //Static retrieve
            //List<Employee> listEmployee = new List<Employee>();
            //Employee emp = new Employee();            
            //emp.FirstName = "Sukesh";
            //emp.LastName = "Marla";
            //emp.Salary = 20000;

            //listEmployee.Add(emp);

            //return listEmployee;

            //Dynamic retrieve
            SalesERPDAL salesDal = new SalesERPDAL();
            return salesDal.Employees.ToList();
        }

        public Employee SaveEmployee(Employee emp)
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            salesDal.Employees.Add(emp);
            salesDal.SaveChanges();
            return emp;
        }

        //public bool IsValidUser(UserDetails u)
        //{
        //    if (u.UserName == "Admin" && u.Password == "Admin")
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public UserStatus GetUserValidity(UserDetails u)
        {            
            if (u.UserName == "Admin" && u.Password == "Admin")
            {
                return UserStatus.AuthenticatedAdmin;
            }
            else if (u.UserName == "Sam" && u.Password == "Sam")
            {
                return UserStatus.AuthenticatedUser;
            }
            else
            {
                return UserStatus.NonAuthenticatedUser;
            }
        }
    }
}