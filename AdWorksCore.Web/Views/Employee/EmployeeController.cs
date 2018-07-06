using System;
using System.Collections.Generic;
using System.Linq;
using AdWorksCore.HumanResources.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AdWorksCore.Web.Views.Employee
{
    public class EmployeeController : Controller
    {
        //private static List<Person> employeeList = new List<Person>() {
        //    new Person() {Id=101, FirstName="Abe", LastName="Grande", LastModified = DateTime.Now.AddDays(-1), PersonType="EM" },
        //    new Person() {Id=102, FirstName="Betty", LastName="Grande", LastModified = DateTime.Now.AddDays(-2), PersonType="EM" },
        //    new Person() {Id=103, FirstName="Candace", LastName="Grande", LastModified = DateTime.Now.AddDays(-3), PersonType="EM" },
        //    new Person() {Id=104, FirstName="Dave", LastName="Draper", LastModified = DateTime.Now.AddDays(-4), PersonType="EM" },
        //    new Person() {Id=105, FirstName="Eunice", LastName="Evers", LastModified = DateTime.Now.AddDays(-4), PersonType="EM" }
        //};

        private readonly ILogger<EmployeeController> logger;
        private readonly HrContext context;

        public EmployeeController(ILogger<EmployeeController> logger, HrContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        // GET: Employee
        [HttpGet]
        public IActionResult Index()
        {
            logger.LogDebug("Employee:Index()");
            var employees = context.Person
                .Where(p => p.PersonType == "EM" && p.ModifiedDate > DateTime.Now.AddYears(-8))
                .OrderBy(p=>p.LastName).ThenBy(p=>p.FirstName);
            IList<EmployeeViewModel> vmList = EmployeeViewModel.FromPerson(employees.ToList());
            return View(vmList);
        }

        // GET: Employee/Details/5
        [HttpGet]
        public IActionResult Detail(int id)
        {
            Person detail = context.Person.Find(id);
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
                    // must create and save changes to get a new BusinessEntityId from the DB
                    BusinessEntity be = new BusinessEntity
                    {
                        ModifiedDate = DateTime.UtcNow
                    };
                    context.BusinessEntity.Add(be);
                    context.SaveChanges();

                    vm.Id = be.BusinessEntityId;
                    Person person = vm.CreatePerson();
                    context.Person.Add(person);
                    context.SaveChanges();

                    //return RedirectToAction(nameof(Index));
                    return RedirectToAction(nameof(Detail), new { id = person.BusinessEntityId });
                }
                catch(Exception e)
                {
                    logger.LogError(e, "saving new person/employee");
                }
            }
            // invalid input
            return View();
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            //Person person = employeeList.FirstOrDefault(e => e.Id == id);
            var person = context.Person.Find(id);
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
                    Person storedEmp = context.Person.Find(id);
                    employee.CopyToPerson(storedEmp);
                    context.SaveChanges();
                    return RedirectToAction(nameof(Detail), new { id });
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