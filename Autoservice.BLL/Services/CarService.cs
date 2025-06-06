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
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public CarService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarDto>> GetAllAsync() =>
            _mapper.Map<IEnumerable<CarDto>>(await _unit.Cars.GetAllAsync());

        public async Task<CarDto> GetByIdAsync(int id) =>
            _mapper.Map<CarDto>(await _unit.Cars.GetByIdAsync(id));

        public async Task AddAsync(CarDto dto)
        {
            await _unit.Cars.AddAsync(_mapper.Map<Car>(dto));
            await _unit.CompleteAsync();
        }

        public async Task UpdateAsync(CarDto dto)
        {
            _unit.Cars.Update(_mapper.Map<Car>(dto));
            await _unit.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _unit.Cars.GetByIdAsync(id);
            if (entity != null)
            {
                _unit.Cars.Delete(entity);
                await _unit.CompleteAsync();
            }
        }
    }
}
