using System;
using System.Collections.Generic;

namespace Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Office { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public DepartmentRoleModel DepartmentRole { get; set; }
        public int DepartmentRoleId { get; set; }
        public ProjectRoleModel ReportsTo { get; set; }
        public int ReportsToId { get; set; }
        public IList<EmployeeProjectModel> EmployeeProjects { get; set; }
    }
}
