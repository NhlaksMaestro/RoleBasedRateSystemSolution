using System;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Entities
{
    public partial class Department : Entity
    {
        public Department()
        {
            this.Employees = new List<Employee>();
        }

        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string MailStop { get; set; }
        public System.DateTime DateTimeCreated { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
