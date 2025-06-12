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
    public class RecordService : IRecordService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public RecordService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RecordDto>> GetAllAsync() =>
            _mapper.Map<IEnumerable<RecordDto>>(await _unit.Records.GetAllAsync());

        public async Task<RecordDto> GetByIdAsync(int id) =>
            _mapper.Map<RecordDto>(await _unit.Records.GetByIdAsync(id));

        public async Task AddAsync(RecordDto dto)
        {
            await _unit.Records.AddAsync(_mapper.Map<Record>(dto));
            await _unit.CompleteAsync();
        }

        public async Task UpdateAsync(RecordDto dto)
        {
            _unit.Records.Update(_mapper.Map<Record>(dto));
            await _unit.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var r = await _unit.Records.GetByIdAsync(id);
            if (r != null)
            {
                _unit.Records.Delete(r);
                await _unit.CompleteAsync();
            }
        }

        public async Task<IEnumerable<RecordDto>> GetRecordsByDateAsync(DateTime date)
        {
            var result = await _unit.Records.GetRecordsByDateAsync(date);
            return _mapper.Map<IEnumerable<RecordDto>>(result); 
        }

        public async Task<IEnumerable<RecordDto>> GetPagedAsync(RecordQueryParameters parameters)
        {
            var records = await _unit.Records.GetAllAsync();
            var query = records.AsQueryable();

            if (parameters.FromDate.HasValue)
                query = query.Where(r => r.Date >= parameters.FromDate.Value);
            if (parameters.ToDate.HasValue)
                query = query.Where(r => r.Date <= parameters.ToDate.Value);
            if (parameters.ClientId.HasValue)
                query = query.Where(r => r.ClientId == parameters.ClientId.Value);
            if (parameters.ServiceId.HasValue)
                query = query.Where(r => r.ServiceId == parameters.ServiceId.Value);

            query = parameters.SortBy?.ToLower() switch
            {
                "date" => parameters.Descending ? query.OrderByDescending(r => r.Date) : query.OrderBy(r => r.Date),
                _ => query
            };

            query = query.Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize);

            return query.Select(r => new RecordDto
            {
                RecordId = r.RecordId,
                Date = r.Date,
                ClientId = r.ClientId,
                CarId = r.CarId,
                ServiceId = r.ServiceId
            }).ToList();
        }
    }

}
