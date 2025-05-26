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
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(AppDbContext context) : base(context) 
        { 
        }

        public async Task<Client> GetClientWithRecordsAsync(int clientId)
        {
            return await _dbSet
                .Include(c => c.Records)
                .FirstOrDefaultAsync(c => c.ClientId == clientId);
        }
    }
}
