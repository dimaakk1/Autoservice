using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.DAL.Entities
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public ICollection<Record> Records { get; set; }
    }

}
