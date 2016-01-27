using System;
using System.Collections.Generic;

namespace Models
{
    public class DepartmentModel
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string MailStop { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public IList<EmployeeModel> Employees { get; set; }
    }
}
