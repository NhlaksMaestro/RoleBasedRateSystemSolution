using Entities;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Providers
{
    public interface IDepartmentBLL : IService<Department>
    {
        Entity DepartmentByName(string departmentName);
        Entity DepartmentById(int departmentId);
        
    }
}
