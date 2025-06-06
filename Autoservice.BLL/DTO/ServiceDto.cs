using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.BLL.DTO
{
    public class ServiceDto
    {
        public int ServiceId { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int EmployeeId { get; set; }
    }
}
