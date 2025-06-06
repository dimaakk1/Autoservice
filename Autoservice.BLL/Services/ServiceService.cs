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

        public async Task UpdateAsync(ServiceDto dto)
        {
            _unit.Services.Update(_mapper.Map<Service>(dto));
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
    }

}
