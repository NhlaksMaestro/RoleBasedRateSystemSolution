using Entities;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Repository.Pattern.Ef6;

namespace DAL.Repository
{
    public static class EmployeeRepository
    {
        public static Entity EmployeeById(this IRepository<Employee> repository, int employeeId)
        {
            try
            {
                return repository
                    .Queryable()
                    .Where(c => c.EmployeeId == employeeId)
                    .FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static Entity EmployeeByLastName(this IRepository<Employee> repository, string employeeLastName)
        {
            try
            {
                return repository
                    .Queryable()
                    .Where(c => c.LastName == employeeLastName)
                    .First();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
