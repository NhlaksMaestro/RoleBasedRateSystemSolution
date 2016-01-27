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
    public interface IProjectBLL : IService<Project>
    {
        Entity ProjectByName(string projectName);
        Entity ProjectById(int projectId);
    }
}
