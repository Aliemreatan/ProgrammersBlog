using AutoMapper;
using MyProject.Entities.Concrete;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Business.Concrete
{
    public class EventManager : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EventManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<EventDto>> Get(int eventId)
        {
            var etkin = await _unitOfWork.Events.GetAsync(c => c.Id == eventId, c => c.Users);
            if (etkin != null)
            {
                return new DataResult<EventDto>(ResultStatus.Success, new EventDto
                {
                    Event = etkin,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<EventDto>(ResultStatus.Error, "Böyle bir etkinlik bulunamadı.", new EventDto
            {
                Event = null,
                ResultStatus = ResultStatus.Error,
                Message = "Böyle bir etkinlik bulunamadı."
            });
        }
        public async Task<IDataResult<EventUpdateDto>> GetEventUpdateDto(int eventId)
        {
            var result = await _unitOfWork.Events.AnyAsync(c => c.Id == eventId);
            if (result)
            {
                var etkin = await _unitOfWork.Events.GetAsync(c => c.Id == eventId);
                var eventUpdateDto = _mapper.Map<EventUpdateDto>(etkin);
                return new DataResult<EventUpdateDto>(ResultStatus.Success, eventUpdateDto);
            }
            else
            {
                return new DataResult<EventUpdateDto>(ResultStatus.Error, "Böyle bir etkinlik bulunamadı.", null);
            }
        }
        public async Task<IDataResult<EventListDto>> GetAll()
        {
            var etkinli = await _unitOfWork.Events.GetAllAsync(null, c => c.Users);
            if (etkinli.Count > -1)
            {
                return new DataResult<EventListDto>(ResultStatus.Success, new EventListDto
                {
                    Events = etkinli,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<EventListDto>(ResultStatus.Error, "Hiç bir etkinlik bulunamadı.", new EventListDto
            {
                Events = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hiç bir etkinlik bulunamadı."
            });
        }
        public async Task<IDataResult<EventListDto>> GetAllByNonDeleted()
        {
            var etkinli = await _unitOfWork.Events.GetAllAsync(c => !c.IsDeleted, c => c.Users);
            if (etkinli.Count > -1)
            {
                return new DataResult<EventListDto>(ResultStatus.Success, new EventListDto
                {
                    Events = etkinli,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<EventListDto>(ResultStatus.Error, "Hiç bir etkinlik bulunamadı.", new EventListDto
            {
                Events = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hiç bir kategori bulunamadı."
            });
        }
        public async Task<IDataResult<EventListDto>> GetAllByNonDeletedAndActive()
        {
            var etkinli = await _unitOfWork.Events.GetAllAsync(c => !c.IsDeleted && c.IsActive, c => c.Users);
            if (etkinli.Count > -1)
            {
                return new DataResult<EventListDto>(ResultStatus.Success, new EventListDto
                {
                    Events = etkinli,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<EventListDto>(ResultStatus.Error, "Hiç bir etkinlik bulunamadı.", null);
        }

        public async Task<IDataResult<EventDto>> Add(EventAddDto eventAddDto, string createdByName)
        {
            var etkin = _mapper.Map<Event>(eventAddDto);
            etkin.CreatedByName = createdByName;
            etkin.ModifiedByName = createdByName;
            var addedEvent = await _unitOfWork.Events.AddAsync(etkin);
            await _unitOfWork.SaveAsync();
            return new DataResult<EventDto>(ResultStatus.Success, $"{eventAddDto.Name} adlı etkinlik başarıyla eklenmiştir.", new EventDto
            {
                Event = addedEvent,
                ResultStatus = ResultStatus.Success,
                Message = $"{eventAddDto.Name} adlı etkinlik başarıyla eklenmiştir."
            });
        }
        public async Task<IDataResult<EventDto>> Update(EventUpdateDto eventUpdateDto, string modifiedByName)
        {
            var oldEvent = await _unitOfWork.Events.GetAsync(c => c.Id == eventUpdateDto.Id);
            var etkin = _mapper.Map<EventUpdateDto, Event>(eventUpdateDto, oldEvent);
            etkin.ModifiedByName = modifiedByName;
            var updatedEvent = await _unitOfWork.Events.UpdateAsync(etkin);
            await _unitOfWork.SaveAsync();
            return new DataResult<EventDto>(ResultStatus.Success, $"{eventUpdateDto.Name} adlı etkinlik başarıyla güncellenmiştir.", new EventDto
            {
                Event = updatedEvent,
                ResultStatus = ResultStatus.Success,
                Message = $"{eventUpdateDto.Name} adlı etkinlik başarıyla güncellenmiştir."
            });
        }

        public async Task<IDataResult<EventDto>> Delete(int eventId, string modifiedByName)
        {
            var etkin = await _unitOfWork.Events.GetAsync(c => c.Id == eventId);
            if (etkin != null)
            {
                etkin.IsDeleted = true;
                etkin.ModifiedByName = modifiedByName;
                etkin.ModifiedDate = DateTime.Now;
                var deletedEvent = await _unitOfWork.Events.UpdateAsync(etkin);
                await _unitOfWork.SaveAsync();
                return new DataResult<EventDto>(ResultStatus.Success, $"{deletedEvent.Name} adlı etkinlik başarıyla silinmiştir.", new EventDto
                {
                    Event = deletedEvent,
                    ResultStatus = ResultStatus.Success,
                    Message = $"{deletedEvent.Name} adlı etkinlik başarıyla silinmiştir."
                });
            }
            return new DataResult<EventDto>(ResultStatus.Error, $"Böyle bir etkinlik bulunamadı.", new EventDto
            {
                Event = null,
                ResultStatus = ResultStatus.Error,
                Message = $"Böyle bir etkinlik bulunamadı."
            });
        }

        public async Task<IResult> HardDelete(int eventId)
        {
            var etkin = await _unitOfWork.Events.GetAsync(c => c.Id == eventId);
            if (etkin != null)
            {
                await _unitOfWork.Events.DeleteAsync(etkin);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{etkin.Name} adlı etkinlik başarıyla veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir etkinlik bulunamadı.");
        }

    }
}
