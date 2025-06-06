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
    }

}
