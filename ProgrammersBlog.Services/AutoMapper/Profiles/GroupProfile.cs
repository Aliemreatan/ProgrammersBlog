﻿using AutoMapper;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.AutoMapper.Profiles
{
    public class GroupProfile:Profile
    {
        public GroupProfile()
        {
            CreateMap<CategoryAddDto, Category>().ForMember(dest =>
                dest.CreatedDate, opt => opt.MapFrom(x => DateTime.Now));
            CreateMap<Category, CategoryAddDto>();

            CreateMap<CategoryUpdateDto, Category>().ForMember(dest => dest.ModifiedDate,
                opt=> opt.MapFrom(x => DateTime.Now));

            CreateMap<Category, CategoryUpdateDto>();


        }
    }
}
