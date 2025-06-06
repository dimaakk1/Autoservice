using Autoservice.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.BLL.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto> GetByIdAsync(int id);
        Task AddAsync(EmployeeDto dto);
        Task UpdateAsync(EmployeeDto dto);
        Task DeleteAsync(int id);
    }
}
