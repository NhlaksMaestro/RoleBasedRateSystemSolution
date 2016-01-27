using System;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Entities
{
    public partial class Project : Entity
    {
        public Project()
        {
            this.EmployeeProjects = new List<EmployeeProject>();
        }

        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime DateTimeCreated { get; set; }
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
