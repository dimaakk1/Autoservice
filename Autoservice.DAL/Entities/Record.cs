using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.DAL.Entities
{
    public class Record
    {
        public int RecordId { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public DateTime Date { get; set; }
    }

}
