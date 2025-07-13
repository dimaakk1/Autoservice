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
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public ServiceService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceDto>> GetAllAsync() =>
            _mapper.Map<IEnumerable<ServiceDto>>(await _unit.Services.GetAllAsync());

        public async Task<ServiceDto> GetByIdAsync(int id) =>
            _mapper.Map<ServiceDto>(await _unit.Services.GetByIdAsync(id));

        public async Task AddAsync(ServiceDto dto)
        {
            await _unit.Services.AddAsync(_mapper.Map<Service>(dto));
            await _unit.CompleteAsync();
        }

        public async Task UpdateAsync(int id, ServiceUpdateDto dto)
        {
            var existing = await _unit.Services.GetByIdAsync(id);

            if (existing == null)
            {
                throw new Exception("Not found");
            }

            existing.Type = dto.Type;
            existing.Price = dto.Price;
            existing.EmployeeId = dto.EmployeeId;
            _unit.Services.Update(existing);
            await _unit.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var s = await _unit.Services.GetByIdAsync(id);
            if (s != null)
            {
                _unit.Services.Delete(s);
                await _unit.CompleteAsync();
            }
        }

        public async Task<IEnumerable<ServiceDto>> GetServicesByEmployeeAsync(int employeeId)
        {
            var result = await _unit.Services.GetServicesByEmployeeAsync(employeeId);
            return _mapper.Map<IEnumerable<ServiceDto>>(result);
        }

        public async Task<IEnumerable<ServiceDto>> GetPagedAsync(ServiceQueryParameters parameters)
        {
            var services = await _unit.Services.GetAllAsync();
            var query = services.AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Type))
                query = query.Where(s => s.Type.Contains(parameters.Type));
            if (parameters.MinPrice.HasValue)
                query = query.Where(s => s.Price >= parameters.MinPrice.Value);
            if (parameters.MaxPrice.HasValue)
                query = query.Where(s => s.Price <= parameters.MaxPrice.Value);

            query = parameters.SortBy?.ToLower() switch
            {
                "type" => parameters.Descending ? query.OrderByDescending(s => s.Type) : query.OrderBy(s => s.Type),
                "price" => parameters.Descending ? query.OrderByDescending(s => s.Price) : query.OrderBy(s => s.Price),
                _ => query
            };

            query = query.Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize);

            return query.Select(s => new ServiceDto { ServiceId = s.ServiceId, Type = s.Type, Price = s.Price }).ToList();
        }
    }

}
