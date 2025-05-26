using Autoservice.DAL.Data;
using Autoservice.DAL.Entities;
using Autoservice.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.DAL.Repositories
{
    public class RecordRepository : GenericRepository<Record>, IRecordRepository
    {
        public RecordRepository(AppDbContext context) : base(context) 
        { 
        }

        public async Task<IEnumerable<Record>> GetRecordsByDateAsync(DateTime date)
        {
            return await _dbSet
                .Where(r => r.Date.Date == date.Date)
                .Include(r => r.Client)
                .Include(r => r.Car)
                .Include(r => r.Service)
                .ToListAsync();
        }
    }
}
