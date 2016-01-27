using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Service.Pattern;
using BusinessLogicLayer.Providers;
using Repository.Pattern.Repositories;
using DAL.Repository;
using Repository.Pattern.Ef6;

namespace BusinessLogicLayer.Services
{
    public class DepartmentBLL : Service<Department>, IDepartmentBLL
    {
        private readonly IRepositoryAsync<Department> _repository;

        public DepartmentBLL(IRepositoryAsync<Department> repository)
            : base(repository)
        {
            _repository = repository;
        }
        public Entity DepartmentByName(string departmentName)
        {
            try
            {
                // add business logic here
                return _repository.DepartmentByName(departmentName);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Entity DepartmentById(int departmentId)
        {
            try
            {
                return _repository.DepartmentById(departmentId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Entity Update(Department entity)
        {
            // e.g. add business logic here before deleting
            base.Update(entity);
            return entity;
        }
        public Entity Insert(Department entity)
        {
            // e.g. add business logic here before inserting
            base.Insert(entity);
            return entity;
        }
        public void Delete(object id)
        {
            // e.g. add business logic here before deleting
            base.Delete(id);
        }
        public IEnumerable<Entity> Departments()
        {
            try
            {
                return _repository.Queryable().AsEnumerable<Department>();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
