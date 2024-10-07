using HR_Application.Models.Repository;
using HR_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Application.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository employeeRepo;

        public EmployeeController() {
            this.employeeRepo = new EmployeeRepository(new HR_DBEntities());
        }

        // GET: Employee
        public ActionResult Index()
        {
            var data = from m in employeeRepo.GetEmployees() select m;
            return View(data);
        }

        [HttpPost]
        public ActionResult Index(string txtSearch) {

            if (txtSearch.ToString()==null||txtSearch.ToString()=="") {
                return View(employeeRepo.GetEmployees().ToList());
            }
            else
            {
                int Id = Convert.ToInt32(txtSearch.ToString());
                Employee empObj = employeeRepo.GetEmployeeById(Id);
                List<Employee> elist = new List<Employee>();
                elist.Add(empObj);
                return View(elist);
            }
        }

        public ActionResult Create() { return View(); }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {

            employeeRepo.Add(employee);
            employeeRepo.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {

            Employee e = employeeRepo.GetEmployeeById(Id);
            return View(e);
        }

        [HttpPost]
        public ActionResult Edit(int Id, Employee employee)
        {
            employeeRepo.Update(employee);
            employeeRepo.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int Id)
        {
            Employee e = employeeRepo.GetEmployeeById(Id);
            return View(e);
        }

        public ActionResult Delete(int Id)
        {
            Employee e = employeeRepo.GetEmployeeById(Id);
            return View(e);
        }

        [HttpPost]
        public ActionResult Delete(int Id, Employee e)
        {

            employeeRepo.Delete(Id);
            employeeRepo.Save();
            return RedirectToAction("Index");
        }



        protected override void Dispose(bool disposing)
        {

            if (disposing)
            {
                employeeRepo.Dispose();
            }
        }
    }
}