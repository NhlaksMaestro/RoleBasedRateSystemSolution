using System;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Entities
{
    public partial class EmployeeProject : Entity
    {
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public int ProjectRoleId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ProjectRole ProjectRole { get; set; }
        public virtual Project Project { get; set; }
    }
}