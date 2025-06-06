using AutoMapper;
using Autoservice.BLL.DTO;
using Autoservice.BLL.Services.Interfaces;
using Autoservice.DAL.Entities;
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

        public async Task UpdateAsync(EmployeeDto dto)
        {
            _unit.Employees.Update(_mapper.Map<Employee>(dto));
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
    }

}
