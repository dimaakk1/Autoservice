using Autoservice.BLL.DTO;
using Autoservice.DAL.Entities;
using Autoservice.BLL.DTO.HelpDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.BLL.Services.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDto>> GetAllAsync();
        Task<ServiceDto> GetByIdAsync(int id);
        Task AddAsync(ServiceDto dto);
        Task UpdateAsync(ServiceDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<ServiceDto>> GetServicesByEmployeeAsync(int employeeId);
        Task<IEnumerable<ServiceDto>> GetPagedAsync(ServiceQueryParameters parameters);
    }
}
