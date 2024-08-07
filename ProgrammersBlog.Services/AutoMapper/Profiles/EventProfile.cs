using AutoMapper;
using MyProject.Entities.Concrete;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.AutoMapper.Profiles
{
    public class EventProfile:Profile
    {
        public EventProfile()
        {
            CreateMap<EventAddDto, Event>().ForMember(dest =>
                dest.CreatedDate, opt => opt.MapFrom(x => DateTime.Now));
            CreateMap<Event, EventAddDto>();

            CreateMap<EventUpdateDto, Event>().ForMember(dest => dest.ModifiedDate,
                opt=> opt.MapFrom(x => DateTime.Now));

            CreateMap<Event, EventUpdateDto>();


        }
    }
}
