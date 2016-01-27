using System;
using System.Collections.Generic;

namespace Models
{
    public class DepartmentRoleModel
    {
        public int DepartmentRoleId { get; set; }
        public string DepartmentRole { get; set; }
        public string DepartmentDescription { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public IList<EmployeeModel> Employees { get; set; }
    }
}
