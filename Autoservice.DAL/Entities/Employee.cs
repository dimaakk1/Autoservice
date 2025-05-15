using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.DAL.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Position { get; set; }
        public string FullName { get; set; }
        public ICollection<Service> Services { get; set; }
    }

}
