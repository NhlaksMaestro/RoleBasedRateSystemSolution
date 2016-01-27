using Entities;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public static class EmployeeLoginRepository
    {
        public static Entity EmployeeLoginById(this IRepository<EmployeeLogin> repository, int employeeId)
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
        public static Entity EmployeeLoginByUserName(this IRepository<EmployeeLogin> repository, string userName)
        {
            try
            {
                return repository
                    .Queryable()
                    .Where(c => c.UserName == userName)
                    .First();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
