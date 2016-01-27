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
    public class ProjectRoleBLL : Service<ProjectRole>, IProjectRoleBLL
    {
        private readonly IRepositoryAsync<ProjectRole> _repository;

        public ProjectRoleBLL(IRepositoryAsync<ProjectRole> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public Entity ProjectRoleByName(string projectRoleName)
        {
            try
            {
                return _repository.ProjectRoleByName(projectRoleName);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Entity ProjectRoleById(int projectRoleId)
        {
            try
            {
                return _repository.ProjectRoleById(projectRoleId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Entity Update(ProjectRole entity)
        {
            try
            {
                // e.g. add business logic here before updating
                base.Update(entity);
                return entity;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Entity Insert(ProjectRole entity)
        {
            try
            {
                // e.g. add business logic here before inserting
                base.Insert(entity);
                return entity;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public override void Delete(object id)
        {
            try
            {
                // e.g. add business logic here before deleting
                base.Delete(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<Entity> ProjectRoles()
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
