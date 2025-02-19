using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using noticiasmvc.AppContext.Entities;
using noticiasmvc.Dtos;

namespace noticiasmvc.MappingProfiles
{
    public class NoticiaProfile : Profile
    {
        public NoticiaProfile()
        {
            CreateMap<NoticiaFormDto, Noticia>()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo))
                .ForMember(dest => dest.Texto, opt => opt.MapFrom(src => src.Texto))
                .ForMember(dest => dest.NoticiaTags, opt => opt.Ignore())
                .AfterMap((source, destination) => {
                    if (source.Tags == null)
                    {
                        source.Tags = new List<int>();
                    }

                    if (destination.NoticiaTags == null)
                    {
                        destination.NoticiaTags = new List<NoticiaTag>();
                    }
                    
                    var existingTags = destination.NoticiaTags.ToList();

                    foreach (var existingTag in existingTags)
                    {
                        if (!source.Tags.Any(tId => tId == existingTag.TagId))
                        {
                            destination.NoticiaTags.Remove(existingTag);
                        }
                    }

                    foreach (var newTagId in source.Tags)
                    {
                        if (!existingTags.Any(et => et.TagId == newTagId))
                        {
                            destination.NoticiaTags.Add(new NoticiaTag { TagId = newTagId });
                        }
                    }
                });
        }
    }
    
    public class NoticiaTagsResolver : IValueResolver<NoticiaFormDto, Noticia, ICollection<NoticiaTag>>
    {
        public ICollection<NoticiaTag> Resolve(NoticiaFormDto source, Noticia destination, ICollection<NoticiaTag> destMember, ResolutionContext context)
        {
            if (destination.Id == 0)
            {
                return source.Tags.Select(t => new NoticiaTag { TagId = t }).ToList();
            }
            else
            {
                if (source.Tags == null)
                {
                    source.Tags = new List<int>();
                }

                if (destination.NoticiaTags == null)
                {
                    destination.NoticiaTags = new List<NoticiaTag>();
                }
                
                var existingTags = destination.NoticiaTags.ToList();

                foreach (var existingTag in existingTags)
                {
                    if (!source.Tags.Any(tId => tId == existingTag.TagId))
                    {
                        destination.NoticiaTags.Remove(existingTag);
                    }
                }

                foreach (var newTagId in source.Tags)
                {
                    if (!existingTags.Any(et => et.TagId == newTagId))
                    {
                        destination.NoticiaTags.Add(new NoticiaTag { TagId = newTagId });
                    }
                }

                return destination.NoticiaTags;
            }
        }
    }
}