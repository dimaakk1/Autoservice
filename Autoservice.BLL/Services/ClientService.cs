using AutoMapper;
using Autoservice.BLL.DTO;
using Autoservice.BLL.Services.Interfaces;
using Autoservice.DAL.Entities;
using Autoservice.BLL.DTO.HelpDTO;
using Autoservice.DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.BLL.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public ClientService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientDto>> GetAllAsync()
        {
            var clients = await _unit.Clients.GetAllAsync();
            return _mapper.Map<IEnumerable<ClientDto>>(clients);
        }

        public async Task<ClientDto> GetByIdAsync(int id)
        {
            var client = await _unit.Clients.GetByIdAsync(id);
            return _mapper.Map<ClientDto>(client);
        }

        public async Task AddAsync(ClientDto dto)
        {
            var client = _mapper.Map<Client>(dto);
            await _unit.Clients.AddAsync(client);
            await _unit.CompleteAsync();
        }

        public async Task UpdateAsync(int id, ClientUpdateDto dto)
        {
            var existing = await _unit.Clients.GetByIdAsync(id);

            if (existing == null)
            {
                throw new Exception("Not found");
            }

            existing.FullName = dto.FullName;
            _unit.Clients.Update(existing);
            await _unit.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var client = await _unit.Clients.GetByIdAsync(id);
            if (client != null)
            {
                _unit.Clients.Delete(client);
                await _unit.CompleteAsync();
            }
        }

        public async Task<IEnumerable<ClientDto>> GetClientsWithRecordsAsync()
        {
            var clients = await _unit.Clients.GetClientWithRecordsAsync();
            return _mapper.Map<IEnumerable<ClientDto>>(clients);
        }

        public async Task<IEnumerable<ClientDto>> GetPagedAsync(ClientQueryParameters parameters)
        {
            var query = await _unit.Clients.GetAllAsync();

            if (!string.IsNullOrEmpty(parameters.FullName))
                query = query.Where(c => c.FullName.Contains(parameters.FullName, StringComparison.OrdinalIgnoreCase));

            query = parameters.SortBy?.ToLower() switch
            {
                "fullname" => parameters.Descending ? query.OrderByDescending(c => c.FullName) : query.OrderBy(c => c.FullName),
                "clientid" => parameters.Descending ? query.OrderByDescending(c => c.ClientId) : query.OrderBy(c => c.ClientId),
                _ => query.OrderBy(c => c.ClientId)
            };

            var skip = (parameters.PageNumber - 1) * parameters.PageSize;
            query = query.Skip(skip).Take(parameters.PageSize);

            return query.Select(c => new ClientDto
            {
                ClientId = c.ClientId,
                FullName = c.FullName
            });
        }

    }
}
