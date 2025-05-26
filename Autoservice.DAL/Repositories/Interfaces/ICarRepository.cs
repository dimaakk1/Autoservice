using Autoservice.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.DAL.Repositories.Interfaces
{
    public interface ICarRepository : IGenericRepository<Car> 
    {
        Task<IEnumerable<Car>> GetCarsByBrandAsync(string brand);
    }

}
