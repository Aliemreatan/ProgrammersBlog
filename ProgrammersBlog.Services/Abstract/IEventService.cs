using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;

namespace ProgrammersBlog.Services.Abstract
{
    public interface IEventService
    {
        Task<IDataResult<EventDto>> Get(int eventId);

        Task<IDataResult<EventUpdateDto>> GetEventUpdateDto(int eventId);
        Task<IDataResult<EventListDto>> GetAll();
        Task<IDataResult<EventListDto>> GetAllByNonDeleted();
        Task<IDataResult<EventListDto>> GetAllByNonDeletedAndActive();
        Task<IDataResult<EventDto>> Add(EventAddDto eventAddDto, string createdByName);
        Task<IDataResult<EventDto>> Update(EventUpdateDto eventUpdateDto, string modifiedByName);
        Task<IDataResult<EventDto>> Delete(int eventId, string modifiedByName);
        Task<IResult> HardDelete(int eventId);
    }
}
