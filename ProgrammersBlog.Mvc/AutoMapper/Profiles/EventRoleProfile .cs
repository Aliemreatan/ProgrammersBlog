using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;

namespace ProgrammersBlog.Mvc.AutoMapper.Profiles
{
    public class EventRoleProfile : Profile
    {
        public EventRoleProfile()
        {
            CreateMap<EventRoleAddDto, EventRole>();
            CreateMap<EventRole, EventRoleUpdateDto>();
            CreateMap<EventRoleUpdateDto, EventRole>();
        }
    }
}
