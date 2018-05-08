using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Entity.Domain;
using Services.DTO.Exercise;
using Services.DTO.ExerciseCriteria;

namespace WebUI.Mapping
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Exercise, ExerciseDetailsDTO>()
                .ForMember(f=>f.Criterias, m=>m.MapFrom(z=>z.ExerciseCriterias));
            CreateMap<ExerciseCriteria, ExerciseCriteriaDTO>();
        }
    }
}