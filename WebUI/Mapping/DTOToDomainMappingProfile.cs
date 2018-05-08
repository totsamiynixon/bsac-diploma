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
    public class DTOToDomainMappingProfile : Profile
    {
        public DTOToDomainMappingProfile()
        {
            CreateMap<ExerciseDetailsDTO, Exercise>()
            .ForMember(f => f.ExerciseCriterias, m => m.MapFrom(z => z.Criterias));
            CreateMap<ExerciseCriteriaDTO, ExerciseCriteria>();
        }
    }
}