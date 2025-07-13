using Autoservice.BLL.DTO;
using Autoservice.BLL.DTO.HelpDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.BLL.Services.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<CarDto>> GetAllAsync();
        Task<CarDto> GetByIdAsync(int id);
        Task AddAsync(CarDto dto);
        Task UpdateAsync(int id, CarUpdateDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<CarDto>> GetCarsByBrandAsync(string brand);
        Task<IEnumerable<CarDto>> GetPagedAsync(CarQueryParameters parameters);

    }
}
