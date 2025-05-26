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
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        public CarRepository(AppDbContext context) : base(context) 
        { 
        }

        public async Task<IEnumerable<Car>> GetCarsByBrandAsync(string brand)
        {
            return await _dbSet
                .Where(car => car.Brand.ToLower() == brand.ToLower())
                .ToListAsync();
        }
    }
}
