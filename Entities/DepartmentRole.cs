using System;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Entities
{
    public partial class DepartmentRole : Entity
    {
        public DepartmentRole()
        {
            this.Employees = new List<Employee>();
        }

        public int DepartmentRoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime DateTimeCreated { get; set; }
        public decimal RatePerHour { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
