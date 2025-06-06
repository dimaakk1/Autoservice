using Autoservice.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.DAL.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository Clients { get; }
        ICarRepository Cars { get; }
        IEmployeeRepository Employees { get; }
        IServiceRepository Services { get; }
        IRecordRepository Records { get; }

        Task<int> CompleteAsync();
    }
}
