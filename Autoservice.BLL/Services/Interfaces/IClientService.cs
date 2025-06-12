using Autoservice.BLL.DTO;
using Autoservice.BLL.DTO.HelpDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.BLL.Services.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAllAsync();
        Task<ClientDto> GetByIdAsync(int id);
        Task AddAsync(ClientDto dto);
        Task UpdateAsync(ClientDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<ClientDto>> GetClientsWithRecordsAsync();
        Task<IEnumerable<ClientDto>> GetPagedAsync(ClientQueryParameters parameters);

    }
}
