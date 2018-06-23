using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdWorksCore.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AdWorksCore.Web.Views.Employee
{
    public class EmployeeController : Controller
    {
        private static List<Person> employeeList = new List<Person>() {
            new Person() {Id=101, FirstName="Abe", LastName="Grande", LastModified = DateTime.Now.AddDays(-1), PersonType="EM" },
            new Person() {Id=102, FirstName="Betty", LastName="Grande", LastModified = DateTime.Now.AddDays(-2), PersonType="EM" },
            new Person() {Id=103, FirstName="Candace", LastName="Grande", LastModified = DateTime.Now.AddDays(-3), PersonType="EM" },
            new Person() {Id=104, FirstName="Dave", LastName="Draper", LastModified = DateTime.Now.AddDays(-4), PersonType="EM" },
            new Person() {Id=105, FirstName="Eunice", LastName="Evers", LastModified = DateTime.Now.AddDays(-4), PersonType="EM" }
        };

        private readonly ILogger<EmployeeController> logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            this.logger = logger;
        }

        // GET: Employee
        [HttpGet]
        public IActionResult Index()
        {
            logger.LogDebug("Employee:Index()");
            return View(employeeList);
        }

        // GET: Employee/Details/5
        [HttpGet]
        public IActionResult Detail(int id)
        {
            Person detail = employeeList.FirstOrDefault(e => e.Id == id);
            if(detail == null)
            {
                // could return NotFound() but not useful to user
                return RedirectToAction(nameof(Index));
            }
            EmployeeViewModel vm = EmployeeViewModel.FromPerson(detail);
            return View(vm);
        }

        // GET: Employee/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Person person = new Person()
                    {
                        FirstName = vm.FirstName,
                        MiddleName = vm.MiddleName,
                        LastName = vm.LastName,
                        PersonType = "EM",
                        Suffix = vm.Suffix,
                        Title = vm.Title,
                        LastModified = DateTime.UtcNow,
                        Id = employeeList.Max(e => e.Id) + 1
                    };
                    employeeList.Add(person);

                    //return RedirectToAction(nameof(Index));
                    return RedirectToAction(nameof(Detail), new { id = person.Id });
                }
                catch
                {
                }
            }
            // invalid input
            return View();
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            Person person = employeeList.FirstOrDefault(e => e.Id == id);
            EmployeeViewModel vm = EmployeeViewModel.FromPerson(person);
            return View(vm);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EmployeeViewModel employee)
        {
            try
            {
                if(ModelState.IsValid && employee.Id == id)
                {
                    Person storedEmp = employeeList.FirstOrDefault(e => e.Id == id);
                    storedEmp.FirstName = employee.FirstName;
                    storedEmp.LastName = employee.LastName;
                    storedEmp.MiddleName = employee.MiddleName;
                    storedEmp.Suffix = employee.Suffix;
                    storedEmp.Title = employee.Title;
                    storedEmp.LastModified = DateTime.UtcNow;
                    return RedirectToAction(nameof(Detail), new { id = employee.Id});
                }
            }
            catch
            {
            }
            return View();
        }

        //// GET: Employee/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}