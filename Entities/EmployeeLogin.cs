

using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public partial class EmployeeLogin : Entity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        //[NotMapped]
        //public string PasswordHash { get; set; }
        //[NotMapped]
        //public string Id
        //{
        //    get { return UserId; }
        //}
    }
}
