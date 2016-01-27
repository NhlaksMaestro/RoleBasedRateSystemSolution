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
    public static class DepartmentRoleRepository
    {
        public static Entity departmentRoleByName(this IRepository<DepartmentRole> repository, string departmentRoleName)
        {
            try
            {
                return repository
                    .Queryable()
                    .Where(c => c.Name == departmentRoleName)
                    .FirstOrDefault();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public static Entity departmentRoleById(this IRepository<DepartmentRole> repository, int departmentRoleId)
        {
            try
            {
                return repository
                    .Queryable()
                    .Where(c => c.DepartmentRoleId == departmentRoleId)
                    .FirstOrDefault();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
