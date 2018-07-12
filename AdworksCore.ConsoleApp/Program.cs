using AdWorksCore.HumanResources.Data.Entities;
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
                .AddDebug((category, level) => category == "AdworksCore.ConsoleApp" && level == LogLevel.Warning);


        // TODO: code smell - configure logging from here, not within EF
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HrContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks2014;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite");
            optionsBuilder.UseLoggerFactory(loggerFactory);

            var logger = loggerFactory.CreateLogger("AdworksCore.ConsoleApp");

            using (HrContext hrContext = new HrContext(optionsBuilder.Options))
            {
                
                //OnboardEmployee(hrContext, hrContext.Person.Find(20778), DateTime.Parse("1995-08-06"), "M", "111120778");


                List<Person> employees = hrContext.Person
                    .Include(p=>p.Employee)
                    .Include(p=>p.PersonPhone)
                    .Where(p=>p.PersonType == "EM")
                    .ToList();
                foreach (var person in employees)
                {
                    Employee employee = person.Employee;
                    if(employee == null)
                    {
                        logger.LogWarning("{FirstName} {LastName} is NOT an employee - {BusinessEntityId}", person.FirstName, person.LastName, person.BusinessEntityId);
                    }
                    else
                    {
                        logger.LogInformation("{FirstName} {LastName} - {JobTitle}", person.FirstName, person.LastName, employee.JobTitle);
                        Console.WriteLine($"{person.FirstName} {person.LastName} - {employee.JobTitle}");
                    }
                }

                
                //Person person = CreatePerson(hrContext, "Jorge", "Jastanza", "EM");
                //OnboardEmployee(hrContext, person, DateTime.Parse("1996-08-06"), "M", "555443336");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
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
                NationalIdnumber = ssn,
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
