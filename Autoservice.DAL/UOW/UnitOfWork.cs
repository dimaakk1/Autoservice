using Autoservice.DAL.Repositories.Interfaces;
using Autoservice.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.DAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IClientRepository Clients { get; }
        public ICarRepository Cars { get; }
        public IEmployeeRepository Employees { get; }
        public IServiceRepository Services { get; }
        public IRecordRepository Records { get; }

        public UnitOfWork(AppDbContext context,
            IClientRepository clients,
            ICarRepository cars,
            IEmployeeRepository employees,
            IServiceRepository services,
            IRecordRepository records)
        {
            _context = context;
            Clients = clients;
            Cars = cars;
            Employees = employees;
            Services = services;
            Records = records;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
