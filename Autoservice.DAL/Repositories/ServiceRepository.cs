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
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(AppDbContext context) : base(context) 
        { 
        }

        public async Task<IEnumerable<Service>> GetServicesByEmployeeAsync(int employeeId)
        {
            return await _dbSet
                .Where(s => s.EmployeeId == employeeId)
                .ToListAsync();
        }
    }
}
