using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entities;
using Repository.Pattern.Repositories;
using Repository.Pattern.Ef6;

namespace DAL.Repository
{
    public static class DepartmentRepository
    {
        public static Entity DepartmentByName(this IRepository<Department> repository, string departmentName)
        {
            try
            {
                return repository
                    .Queryable()
                    .Where(c => c.Name == departmentName)
                    .First();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public static Entity DepartmentById(this IRepository<Department> repository, int departmentId)
        {
            try
            {
                return repository
                    .Queryable()
                    .Where(c => c.DepartmentId == departmentId)
                    .FirstOrDefault();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
