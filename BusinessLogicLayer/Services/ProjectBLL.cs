using BusinessLogicLayer.Providers;
using Entities;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;
using Repository.Pattern.Ef6;

namespace BusinessLogicLayer.Services
{
    public class ProjectBLL : Service<Project>, IProjectBLL
    {
        private readonly IRepositoryAsync<Project> _repository;

        public ProjectBLL(IRepositoryAsync<Project> repository)
            : base(repository)
        {
            _repository = repository;
        }
        public Entity ProjectByName(string projectName)
        {
            try
            {
                return _repository.projectByName(projectName);
            }
            catch (Exception)
            {
                
                throw;
            } 
        }
        public Entity ProjectById(int projectId)
        {
            try
            {
                return _repository.ProjectById(projectId);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public Entity Update(Project entity)
        {
            // e.g. add business logic here before deleting
            base.Update(entity);
            return entity;
        }
        public Entity Insert(Project entity)
        {
            // e.g. add business logic here before inserting
            base.Insert(entity);
            return entity;
        }
        public override void Delete(object id)
        {
            // e.g. add business logic here before deleting
            base.Delete(id);
        }
        public IEnumerable<Entity> Projects()
        {
            try
            {
                return _repository.Queryable().AsEnumerable();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
