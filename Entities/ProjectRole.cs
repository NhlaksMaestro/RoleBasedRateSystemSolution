using System;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Entities
{
    public partial class ProjectRole: Entity
    {
        public ProjectRole()
        {
            this.EmployeeProjects = new List<EmployeeProject>();
        }

        public int ProjectRoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime DateTimeCreated { get; set; }
        public decimal RatePerHour { get; set; }
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
