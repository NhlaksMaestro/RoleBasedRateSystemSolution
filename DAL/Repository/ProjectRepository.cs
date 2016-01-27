using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Pattern;
using Repository.Pattern.Repositories;
using Entities;
using System.Data.Entity;
using Repository.Pattern.Ef6;

namespace DAL.Repository
{
    public static class ProjectRepository
    {
        public static Entity projectByName(this IRepository<Project> repository, string projectName)
        {
            try
            {
                return repository
                    .Queryable()
                    .Where(c => c.Name.Equals(projectName))
                    .First();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public static Entity ProjectById(this IRepository<Project> repository, int projectId)
        {
            try
            {
                return repository
                    .Queryable()
                    .Where(c => c.ProjectId == projectId)
                    .FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
