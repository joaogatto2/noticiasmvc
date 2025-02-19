using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using noticiasmvc.AppContext.Entities;
using noticiasmvc.Dtos;

namespace noticiasmvc.MappingProfiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<TagFormDto, Tag>();
        }
    }
}