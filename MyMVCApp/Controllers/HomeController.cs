using MyMVCApp.Fliters;
using BusinessLayer;
using BusinessEntities;
using ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMVCApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HeaderFooterFilter]
        public ActionResult Index()
        {
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
            // employeeListViewModel.UserName = User.Identity.Name; //New Line
            EmployeeBL empBal = new EmployeeBL();
            List<Employee> employees = empBal.GetEmployee();

            List<EmployeeViewModel> empViewModels = new List<EmployeeViewModel>();

            foreach (Employee emp in employees)
            {
                EmployeeViewModel empViewModel = new EmployeeViewModel();
                empViewModel.EmployeeName = emp.FirstName + " " + emp.LastName;
                empViewModel.Salary = emp.Salary.ToString("C");
                if (emp.Salary > 15000)
                {
                    empViewModel.SalaryColor = "yellow";
                }
                else
                {
                    empViewModel.SalaryColor = "green";
                }
                empViewModels.Add(empViewModel);
            }
            employeeListViewModel.Employeelist = empViewModels;
            //employeeListViewModel.FooterData = new FooterViewModel();
            //employeeListViewModel.FooterData.CompanyName = "StepByStepSchools";//Can be set to dynamic value
            //employeeListViewModel.FooterData.Year = DateTime.Now.Year.ToString();
            return View("ViewModel", employeeListViewModel);
        }

        // GET: My view
        public ActionResult GetViewWithObject()
        {
            Employee emp = new Employee();
            emp.FirstName = "Sukesh";
            emp.LastName = "Marla";
            emp.Salary = 20000;

            return View("View", emp);
        }

        // GET: My view
        public ActionResult GetViewWithViewData()
        {
            Employee emp = new Employee();
            emp.FirstName = "Sukesh";
            emp.LastName = "Marla";
            emp.Salary = 20000;

            ViewData["Employee"] = emp;
            return View("View");
        }

        // GET: My view
        [Authorize]
        [HeaderFooterFilter]
        public ActionResult GetViewModel()
        {
            EmployeeListViewModel empViewModel = new EmployeeListViewModel();
            empViewModel = getEmployee();

            empViewModel.FooterData = new FooterViewModel();
            //empViewModel.FooterData.CompanyName = "Aspire";
            //empViewModel.FooterData.Year = 1991;
            return View("ViewModel", empViewModel);
        }

        private EmployeeListViewModel getEmployee()
        {
            EmployeeListViewModel empViewModel = new EmployeeListViewModel();
            List<EmployeeViewModel> listVwEmp = new List<EmployeeViewModel>();
            EmployeeBL employeeBL = new EmployeeBL();
            List<Employee> listEmployee = new List<Employee>();
            listEmployee = employeeBL.GetEmployee();

            foreach (var emp in listEmployee)
            {
                EmployeeViewModel vwEmp = new EmployeeViewModel();
                vwEmp.EmployeeName = string.Format("{0} {1}", emp.FirstName, emp.LastName);
                vwEmp.Salary = emp.Salary.ToString("C");

                if (emp.Salary > 1000)
                {
                    vwEmp.SalaryColor = "yellow";
                }
                else
                {
                    vwEmp.SalaryColor = "green";
                }

                listVwEmp.Add(vwEmp);
            }
            empViewModel.Employeelist = listVwEmp;
            empViewModel.UserName = User.Identity.Name;

            return empViewModel;
        }

        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult AddNew()
        {
            CreateEmployeeViewModel empViewModel = new CreateEmployeeViewModel();
            //empViewModel.FooterData = new FooterViewModel();
            //empViewModel.FooterData.CompanyName = "Aspire";
            //empViewModel.FooterData.Year = 1991;
            //empViewModel.UserName = User.Identity.Name;
            return View("CreateEmployee", empViewModel);
        }

        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult SaveEmployee(Employee emp, string BtnSubmit)
        {    
            switch (BtnSubmit)
            {
                case "Save Employee":
                    if (ModelState.IsValid)
                    {
                        //return Content(emp.FirstName + " " + emp.LastName + ": " + emp.Salary);
                        EmployeeBL blEmployee = new EmployeeBL();
                        blEmployee.SaveEmployee(emp);
                        return View("ViewModel", getEmployee());
                    }
                    else
                    {
                        CreateEmployeeViewModel vm = new CreateEmployeeViewModel();
                        vm.FirstName = emp.FirstName;
                        vm.LastName = emp.LastName;

                        //Footer
                        //vm.FooterData = new FooterViewModel();
                        //vm.FooterData.CompanyName = "Aspire";
                        //vm.FooterData.Year = 1991;
                        //vm.UserName = User.Identity.Name;

                        if (emp.Salary > 0)
                        {
                            vm.Salary = emp.Salary.ToString();
                        }
                        else
                        {
                            vm.Salary = ModelState["Salary"].Value.AttemptedValue;
                        }

                        return View("CreateEmployee", vm);
                    }
                case "Cancel":                     
                    return View("ViewModel", getEmployee());
            }
            return new EmptyResult();
        }

        public class Customer
        {
            public string CustomerName { get; set; }
            public string Address { get; set; }

            public override string ToString()
            {
                return this.CustomerName + "|" + this.Address;
            }
        }

        public Customer GetCustomer()
        {
            Customer c = new Customer();
            c.CustomerName = "Customer 1";
            c.Address = "Address1";
            return c;
        }

        [ChildActionOnly]
        public ActionResult GetAddNewLink()
        { 
            if(Convert.ToBoolean(Session["Admin"]))
            {
                return PartialView("AddNewLink");
            }

            return new EmptyResult();
        }
        

    }
}