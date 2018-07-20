using System;
using System.Collections.Generic;
using System.Linq;
using AdWorksCore.HumanResources.Data.Domain;
using AdWorksCore.HumanResources.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AdWorksCore.Web.Views.Employee
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<EmployeeController> logger;

        public EmployeeController(IEmployeeRepository repository, IMapper mapper, ILogger<EmployeeController> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: Employee
        [HttpGet]
        public IActionResult Index()
        {
            logger.LogDebug("Employee:Index()");
            var employees = repository.GetEmployees().ToList();
            return View(mapper.Map<IEnumerable<HumanResources.Data.Entities.Employee>, IEnumerable<EmployeeViewModel>>(employees));
        }

        // GET: Employee/Details/5
        [HttpGet]
        public IActionResult Detail(int id)
        {
            var detail = repository.GetEmployee(id);
            if(detail == null)
            {
                // could return NotFound() but not useful to user
                return RedirectToAction(nameof(Index));
            }
            
            EmployeeViewModel vm = mapper.Map<HumanResources.Data.Entities.Employee, EmployeeViewModel>(detail);
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
                    var employee = mapper.Map<EmployeeViewModel, HumanResources.Data.Entities.Employee>(vm);
                    repository.Add(employee);
                    repository.SaveChanges();

                    //return RedirectToAction(nameof(Index));
                    return RedirectToAction(nameof(Detail), new { id = employee.BusinessEntityId });
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
            var employee = repository.GetEmployee(id);
            if (employee == null)
            {
                // could return NotFound() but not useful to user
                return RedirectToAction(nameof(Index));
            }

            EmployeeViewModel vm = mapper.Map<HumanResources.Data.Entities.Employee, EmployeeViewModel>(employee);
            return View(vm);

        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EmployeeViewModel vm)
        {
            try
            {
                if(ModelState.IsValid && vm.Id == id)
                {
                    var employee = mapper.Map<EmployeeViewModel, HumanResources.Data.Entities.Employee>(vm);
                    repository.Update(employee);
                    repository.SaveChanges();
                    return RedirectToAction(nameof(Detail), new { id });
                }
            }
            catch(Exception e)
            {
                logger.LogError(e, "Error updating Employee");
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