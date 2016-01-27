using Entities;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL.Repository
{
    public static class EmployeeProjectRepository
    {
        public static IEnumerable<EmployeeProject> EmployeeProjectsByEmployeeId(this IRepository<EmployeeProject> repository, int employeeId)
        {
            try
            {
                return repository
                    .Queryable()
                    .Where(c => c.EmployeeId == employeeId)
                    .AsEnumerable();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public static IEnumerable<EmployeeProject> EmployeeProjectsByProjectRoleId(this IRepository<EmployeeProject> repository, int projectRoleId)
        {
            try
            {
                return repository
                    .Queryable()
                    .Where(c => c.ProjectRoleId == projectRoleId)
                    .AsEnumerable();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public static IEnumerable<EmployeeProject> EmployeeProjectsByProjectId(this IRepository<EmployeeProject> repository, int projectId)
        {
            try
            {
                return repository
                    .Queryable()
                    .Where(c => c.ProjectId == projectId)
                    .AsEnumerable();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
