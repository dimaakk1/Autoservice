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
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync() =>
            _mapper.Map<IEnumerable<EmployeeDto>>(await _unit.Employees.GetAllAsync());

        public async Task<EmployeeDto> GetByIdAsync(int id) =>
            _mapper.Map<EmployeeDto>(await _unit.Employees.GetByIdAsync(id));

        public async Task AddAsync(EmployeeDto dto)
        {
            await _unit.Employees.AddAsync(_mapper.Map<Employee>(dto));
            await _unit.CompleteAsync();
        }

        public async Task UpdateAsync(int id, EmployeeUpdateDto dto)
        {
            var existing = await _unit.Employees.GetByIdAsync(id);

            if (existing == null)
            {
                throw new Exception("Not found");
            }

            existing.FullName = dto.FullName;
            existing.Position = dto.Position;
            _unit.Employees.Update(existing);
            await _unit.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var e = await _unit.Employees.GetByIdAsync(id);
            if (e != null)
            {
                _unit.Employees.Delete(e);
                await _unit.CompleteAsync();
            }
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByPositionAsync(string position)
        {
            var result = await _unit.Employees.GetEmployeesByPositionAsync(position);
            return _mapper.Map<IEnumerable<EmployeeDto>>(result);
        }

        public async Task<IEnumerable<EmployeeDto>> GetPagedAsync(EmployeeQueryParameters parameters)
        {
            var employees = await _unit.Employees.GetAllAsync();
            var query = employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.FullName))
                query = query.Where(e => e.FullName.Contains(parameters.FullName));
            if (!string.IsNullOrWhiteSpace(parameters.Position))
                query = query.Where(e => e.Position.Contains(parameters.Position));

            query = parameters.SortBy?.ToLower() switch
            {
                "fullname" => parameters.Descending ? query.OrderByDescending(e => e.FullName) : query.OrderBy(e => e.FullName),
                "position" => parameters.Descending ? query.OrderByDescending(e => e.Position) : query.OrderBy(e => e.Position),
                _ => query
            };

            query = query.Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize);

            return query.Select(e => new EmployeeDto { EmployeeId = e.EmployeeId, FullName = e.FullName, Position = e.Position }).ToList();
        }
    }

}
