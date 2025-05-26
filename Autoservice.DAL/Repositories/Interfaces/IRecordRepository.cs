using Autoservice.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.DAL.Repositories.Interfaces
{
    public interface IRecordRepository : IGenericRepository<Record> 
    {
        Task<IEnumerable<Record>> GetRecordsByDateAsync(DateTime date);

    }

}
