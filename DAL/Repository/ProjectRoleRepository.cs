
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
    public static class ProjectRoleRepository
    {
        public static Entity ProjectRoleByName(this IRepository<ProjectRole> repository, string projectRoleName)
        {
            try
            {
                return repository
                    .Queryable()
                    .Where(c => c.Name == projectRoleName)
                    .First();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public static Entity ProjectRoleById(this IRepository<ProjectRole> repository, int projectRoleId)
        {
            try
            {
                return repository
                    .Queryable()
                    .Where(c => c.ProjectRoleId == projectRoleId)
                    .FirstOrDefault();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
