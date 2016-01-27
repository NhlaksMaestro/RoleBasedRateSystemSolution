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
    public class EmployeeBLL : Service<Employee>, IEmployeeBLL
    {
        private readonly IRepositoryAsync<Employee> _repository;

        public EmployeeBLL(IRepositoryAsync<Employee> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public Entity EmployeeById(int employeeId)
        {
            try
            {
                return _repository.EmployeeById(employeeId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Entity EmployeeByLastName(string employeeLastName)
        {
            try
            {
                return _repository.EmployeeByLastName(employeeLastName);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Entity Update(Employee entity)
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
        public Entity Insert(Employee entity)
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
        public IEnumerable<Entity> Employees()
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
