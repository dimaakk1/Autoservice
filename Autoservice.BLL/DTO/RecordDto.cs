using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.BLL.DTO
{
    public class RecordDto
    {
        public int RecordId { get; set; }
        public int ClientId { get; set; }
        public int CarId { get; set; }
        public int ServiceId { get; set; }
        public DateTime Date { get; set; }
    }
}
