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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context) 
        { 
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByPositionAsync(string position)
        {
            return await _dbSet
                .Where(e => e.Position.ToLower() == position.ToLower())
                .ToListAsync();
        }
    }
}
