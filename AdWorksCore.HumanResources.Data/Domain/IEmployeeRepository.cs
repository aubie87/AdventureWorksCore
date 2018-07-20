using System.Linq;
using AdWorksCore.HumanResources.Data.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AdWorksCore.HumanResources.Data.Domain
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int id);
        IQueryable<Employee> GetEmployees();
        void Add(Employee employee);
        bool SaveChanges();
        EntityEntry<Employee> Update(Employee employee);
    }
}