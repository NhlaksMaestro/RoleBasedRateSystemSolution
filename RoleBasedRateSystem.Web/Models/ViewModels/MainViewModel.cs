using Entities;
using System.Collections.Generic;
namespace RoleBasedRateSystem.Web.Models.ViewModels
{
    public class MainViewModel
    {

        public int DepartmentRoleId { get; set; }
        public decimal MinimumRate { get; set; }
        public decimal MaximumRate { get; set; }
        public IEnumerable<DepartmentRole> Rates { get; set; }
        public Employee Employee { get; set; }
        public IEnumerable<Employee> Employees { get; set; }

    }
}