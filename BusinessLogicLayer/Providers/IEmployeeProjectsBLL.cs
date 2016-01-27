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
    public interface IEmployeeProjectsBLL: IService<EmployeeProject>
    {
        IEnumerable<Entity> EmployeeProjectsByEmployeeId(int employeeId);
        IEnumerable<Entity> EmployeeProjectsByProjectRoleId(int projectRoleId);
        IEnumerable<Entity> EmployeeProjectsByProjectId(int projectId);
        
    }
}
