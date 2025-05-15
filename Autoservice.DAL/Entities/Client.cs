using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.DAL.Entities
{
    public class Client
    {
        public int ClientId { get; set; }
        public string FullName { get; set; }
        public ICollection<Record> Records { get; set; }
    }
}
