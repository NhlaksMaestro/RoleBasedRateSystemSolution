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
    public class EmployeeLoginBLL : Service<EmployeeLogin>, IEmployeeLoginBLL
    {
        private readonly IRepositoryAsync<EmployeeLogin> _repository;

        public EmployeeLoginBLL(IRepositoryAsync<EmployeeLogin> repository)
            : base(repository)
        {
            _repository = repository;
        }
        public Entity EmployeeLoginById(int employeeId)
        {
            try
            {
                return _repository.EmployeeLoginById(employeeId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Entity EmployeeLoginByUserName(string employeeUserName)
        {
            try
            {
                return _repository.EmployeeLoginByUserName(employeeUserName);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
