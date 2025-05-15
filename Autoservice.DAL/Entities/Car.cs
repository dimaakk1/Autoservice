using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.DAL.Entities
{
    public class Car
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        public ICollection<Record> Records { get; set; }
    }

}
