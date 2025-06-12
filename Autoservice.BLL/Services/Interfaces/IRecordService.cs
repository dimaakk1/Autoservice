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
    public interface IRecordService
    {
        Task<IEnumerable<RecordDto>> GetAllAsync();
        Task<RecordDto> GetByIdAsync(int id);
        Task AddAsync(RecordDto dto);
        Task UpdateAsync(RecordDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<RecordDto>> GetRecordsByDateAsync(DateTime date);
        Task<IEnumerable<RecordDto>> GetPagedAsync(RecordQueryParameters parameters);

    }
}
