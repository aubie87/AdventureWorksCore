using AdWorksCore.HumanResources.Data.Domain;
using AdWorksCore.HumanResources.Data.Entities;
using AdWorksCore.Web.Views.Employee;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdworksCore.ConsoleApp
{
    class Program
    {
        public static readonly ILoggerFactory loggerFactory
            = new LoggerFactory()
                .AddConsole((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information, true)
                .AddDebug((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information);
                //.AddDebug((category, level) => category == "AdworksCore.ConsoleApp" && level == LogLevel.Information);


        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HrContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks2014;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite");
            optionsBuilder.UseLoggerFactory(loggerFactory);

            var logger = loggerFactory.CreateLogger<Program>();

            // configure mapper for learning
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EmployeeMappingProfile>();
            });

            mapConfig.AssertConfigurationIsValid();
            var map = mapConfig.CreateMapper();

            EmployeeViewModel vm;
            using (HrContext ctx1 = new HrContext(optionsBuilder.Options))
            {
                // IoC container usually handles this.
                IEmployeeRepository repo = new EmployeeRepository(ctx1, loggerFactory.CreateLogger<EmployeeRepository>());
                Employee dbEmployee = repo.GetEmployee(20778);
                Employee emptyEmp = new Employee();
                map.Map(dbEmployee, emptyEmp);
                Console.WriteLine($"dbEmployee JobTitle={dbEmployee.JobTitle} emptyEmp JobTitle={emptyEmp.JobTitle}");

                vm = MapToViewModel(dbEmployee, map, 20778);

                PrintListOfEmployees(repo, logger);
                //Person person = CreatePerson(hrContext, "Jorge", "Jastanza", "EM");
                //OnboardEmployee(hrContext, person, DateTime.Parse("1996-08-06"), "M", "555443336");
            }

            using (HrContext ctx2 = new HrContext(optionsBuilder.Options))
            {
                IEmployeeRepository repo = new EmployeeRepository(ctx2, loggerFactory.CreateLogger<EmployeeRepository>());
                vm.LastName += "z";
                SaveChangesToViewModel(repo, map, vm);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void SaveChangesToViewModel(IEmployeeRepository repo, IMapper map, EmployeeViewModel vm)
        {
            Console.WriteLine("   *** SaveChangesToViewModel ***");
            Employee disconnectedEmployee = map.Map<EmployeeViewModel, Employee>(vm);
            Console.WriteLine($" Employee: {disconnectedEmployee.BusinessEntity.FirstName} {disconnectedEmployee.BusinessEntity.LastName}: {disconnectedEmployee.ModifiedDate}");

            var entityEmployee = repo.Update(disconnectedEmployee);
            repo.SaveChanges();
            Console.WriteLine(" Updated");
            Console.WriteLine();
        }

        private static EmployeeViewModel MapToViewModel(Employee employee, IMapper map, int id)
        {
            var vm = map.Map<Employee, EmployeeViewModel>(employee);

            Console.WriteLine("   *** MapToViewModel ***");
            Console.WriteLine($" Employee: {employee.BusinessEntity.FirstName} {employee.BusinessEntity.MiddleName} {employee.BusinessEntity.LastName}");
            Console.WriteLine($"ViewModel: {vm.FirstName} {vm.LastName}: {vm.EmployeeModifiedDate}");
            Console.WriteLine();

            return vm;
        }

        private static void PrintListOfEmployees(IEmployeeRepository repo, ILogger logger)
        {
            Console.WriteLine("   *** PrintListOfEmployees ***");
            var employees = repo.GetEmployeesSummary();
            foreach (var person in employees)
            {
                logger.LogInformation("{FirstName} {LastName} - {JobTitle}", person.FirstName, person.LastName, person.JobTitle);
                Console.WriteLine($"{person.FirstName} {person.LastName} - {person.JobTitle}");
            }
            Console.WriteLine();
        }

        private static Person CreatePerson(HrContext context, string firstName, string lastName, string personType)
        {
            BusinessEntity be = new BusinessEntity();
            //{
            //    ModifiedDate = DateTime.UtcNow
            //};
            context.BusinessEntity.Add(be);

            Person person = new Person
            {
                BusinessEntityId = be.BusinessEntityId,
                PersonType = personType,
                FirstName = firstName,
                LastName = lastName
            };
            context.Person.Add(person);

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine("CreatePerson exception: " + e.Message);
            }
            return person;
        }

        private static void OnboardEmployee(HrContext hrContext, Person person, DateTime birthDate, string sex, string ssn)
        {
            if (person == null)
            {
                Debug.WriteLine("Person 22781 not found");
            }

            Employee employee = new Employee()
            {
                BusinessEntityId = person.BusinessEntityId,
                BirthDate = birthDate,
                Gender = sex,
                NationalIdNumber = ssn,
                LoginId = string.Format($"adventure-works\\{person.FirstName.ToLower()}.{person.LastName.ToLower()}"),
                JobTitle = "Software Intern",
                MaritalStatus = "S",
                HireDate = DateTime.Today,
                SalariedFlag = true,
                VacationHours = 40,
                SickLeaveHours = 16,
                CurrentFlag = true
            };

            hrContext.Employee.Add(employee);

            try
            {
                hrContext.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
