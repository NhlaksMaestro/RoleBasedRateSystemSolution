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
    public class DepartmentRoleBLL : Service<DepartmentRole>, IDepartmentRoleBLL
    {
        private readonly IRepositoryAsync<DepartmentRole> _repository;

        public DepartmentRoleBLL(IRepositoryAsync<DepartmentRole> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public Entity DepartmentRoleByName(string departmentRoleName)
        {
            try
            { 
                return _repository.departmentRoleByName(departmentRoleName);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Entity DepartmentRoleById(int departmentRoleId)
        {
            try
            {
                return _repository.departmentRoleById(departmentRoleId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Entity Update(DepartmentRole entity)
        {
            try
            {
                base.Update(entity);
                return entity;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Entity Insert(DepartmentRole entity)
        {
            try
            {
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
            // e.g. add business logic here before deleting
            base.Delete(id);
        }
        public IEnumerable<Entity> DepartmentRoles()
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
