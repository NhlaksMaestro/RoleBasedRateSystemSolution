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
    public class EmployeeProjectsBLL : Service<EmployeeProject>, IEmployeeProjectsBLL
    {
        private readonly IRepositoryAsync<EmployeeProject> _repository;

        public EmployeeProjectsBLL(IRepositoryAsync<EmployeeProject> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<Entity> EmployeeProjectsByEmployeeId(int employeeId)
        {
            try
            {
                return _repository.EmployeeProjectsByEmployeeId(employeeId);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public IEnumerable<Entity> EmployeeProjectsByProjectRoleId(int projectRoleId)
        {
            try
            {
                return _repository.EmployeeProjectsByProjectRoleId(projectRoleId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Entity> EmployeeProjectsByProjectId(int projectId)
        {
            try
            {
                return _repository.EmployeeProjectsByProjectId(projectId);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
