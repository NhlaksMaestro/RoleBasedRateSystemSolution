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
    public interface IDepartmentRoleBLL: IService<DepartmentRole>
    {
        Entity DepartmentRoleByName(string departmentRoleName);
        Entity DepartmentRoleById(int departmentRoleId);
    }
}
