using System;
using System.Collections.Generic;

namespace Models
{
    public class ProjectModel
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public IList<EmployeeProjectModel> EmployeeProjects { get; set; }
    }
}
