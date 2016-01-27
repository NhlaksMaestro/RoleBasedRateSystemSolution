using Entities;
using Repository.Pattern.Ef6;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Providers
{
    public interface IEmployeeLoginBLL : IService<EmployeeLogin>
    {
        Entity EmployeeLoginById(int employeeId);
        Entity EmployeeLoginByUserName(string employeeUserName);
        //public Task<string> GetPasswordHashAsync(EmployeeLogin user);
        //public Task<bool> HasPasswordAsync(EmployeeLogin user);
        //public Task SetPasswordHashAsync(EmployeeLogin user, string passwordHash);
    }
}
