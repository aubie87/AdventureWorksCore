using AdWorksCore.HumanResources.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdWorksCore.HumanResources.Data.Domain
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HrContext context;
        private readonly ILogger<EmployeeRepository> logger;

        /// <summary>
        /// The logger instance should be supplied by the DI framework or created manually.
        /// </summary>
        /// <param name="hrContext"></param>
        /// <param name="logger"></param>
        public EmployeeRepository(HrContext hrContext, ILogger<EmployeeRepository> logger)
        {
            this.context = hrContext;
            this.logger = logger;

            // this tells the entire context to perform no tracking - optimal for disconnected, web type applications
            //context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public IQueryable<Employee> GetEmployees()
        {
            // The where clause isn't needed since the Employee table is, by definition, only employees.
            return context.Employee
                // .AsNoTracking()
                .Include(p => p.BusinessEntity)
                // .Where(e => e.BusinessEntity.PersonType == "EM" && e.ModifiedDate > DateTime.Now.AddYears(-8))
                // .Where(e=>e.CurrentFlag.HasValue == true)  // used for employees no longer with the company?
                .Where(e=>e.ModifiedDate > DateTime.Now.AddYears(-4))
                .OrderBy(e => e.BusinessEntity.LastName)
                .ThenBy(e => e.BusinessEntity.FirstName);
        }

        public Employee GetEmployee(int id)
        {
            return context.Employee
                //.AsNoTracking()
                .Include(p => p.BusinessEntity)
                .FirstOrDefault(e => e.BusinessEntityId == id);
        }

        /// <summary>
        /// Need to create an "On boarding" process.
        /// </summary>
        /// <param name="employee"></param>
        public void Add(Employee employee)
        {
            // must create and save changes to get a new BusinessEntityId from the DB
            BusinessEntity be = new BusinessEntity
            {
                ModifiedDate = DateTime.UtcNow
            };

            context.Add(be);
            employee.BusinessEntity.BusinessEntity = be;
            employee.BusinessEntity.BusinessEntityId = be.BusinessEntityId;

            employee.BusinessEntityId = be.BusinessEntityId;

            context.Employee.Add(employee);
        }

        public EntityEntry<Employee> Update(Employee employee)
        {
            return context.Update(employee);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
