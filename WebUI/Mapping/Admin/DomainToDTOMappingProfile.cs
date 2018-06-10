using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Entity.Domain;
using Services.DTO.Exercise;
using Services.DTO.ExerciseCriteria;
using Services.DTO.Profession;
using Services.DTO.ProfessionCriteria;

namespace WebUI.Mapping
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Exercise, ExerciseDetailsDTO>()
                .ForMember(f => f.Criterias, m => m.MapFrom(z => z.ExerciseCriterias));
            CreateMap<ExerciseCriteria, ExerciseCriteriaDTO>();
            CreateMap<Profession, ProfessionDetailsDTO>()
                .ForMember(f => f.Criterias, m => m.MapFrom(z => z.ProfessionCriterias));
            CreateMap<ProfessionCriteria, ProfessionCriteriaDTO>();
        }
    }
}