using System;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Entities
{
    public partial class Employee : Entity
    {
        public Employee()
        {
            this.EmployeeProjects = new List<EmployeeProject>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Nullable<System.DateTime> DateTimeCreated { get; set; }
        public int DepartmentRoleId { get; set; }
        public int DepartmentId { get; set; }
        public virtual DepartmentRole DepartmentRole { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
        public virtual EmployeeLogin EmployeeLogin { get; set; }
    }
}
