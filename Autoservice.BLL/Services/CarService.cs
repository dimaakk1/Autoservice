using AutoMapper;
using Autoservice.BLL.DTO;
using Autoservice.BLL.Services.Interfaces;
using Autoservice.DAL.Entities;
using Autoservice.DAL.UOW;
using Autoservice.BLL.DTO.HelpDTO;
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

        public async Task<IEnumerable<CarDto>> GetCarsByBrandAsync(string brand)
        {
            var cars = await _unit.Cars.GetCarsByBrandAsync(brand);
            return _mapper.Map<IEnumerable<CarDto>>(cars);
        }

        public async Task<IEnumerable<CarDto>> GetPagedAsync(CarQueryParameters parameters)
        {
            var cars = await _unit.Cars.GetAllAsync();
            var query = cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Brand))
                query = query.Where(c => c.Brand.Contains(parameters.Brand));
            if (parameters.Year.HasValue)
                query = query.Where(c => c.Year == parameters.Year.Value);

            query = parameters.SortBy?.ToLower() switch
            {
                "brand" => parameters.Descending ? query.OrderByDescending(c => c.Brand) : query.OrderBy(c => c.Brand),
                "year" => parameters.Descending ? query.OrderByDescending(c => c.Year) : query.OrderBy(c => c.Year),
                _ => query
            };

            query = query.Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize);

            return query.Select(c => new CarDto 
            { 
                CarId = c.CarId, 
                Brand = c.Brand, 
                Year = c.Year }
            ).ToList();
        }
    }
}
