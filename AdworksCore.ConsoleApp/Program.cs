using AdWorksCore.HumanResources.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace AdworksCore.ConsoleApp
{
    class Program
    {
        // TODO: code smell - configure logging from here, not within EF
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HrContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks2014;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite");
            using (HrContext hrContext = new HrContext(optionsBuilder.Options))
            {
                //Person person = CreatePerson(hrContext, "Jorge", "Jastanza", "EM");
                Person person = hrContext.Person.Include(p=>p.Employee).Include(p=>p.PersonPhone).Where(p => p.BusinessEntityId == 1).FirstOrDefault();
                Employee employee = person.Employee;
                Console.WriteLine($"{person.FirstName} {person.LastName} {employee.JobTitle}");
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
