using AutoMapper;
using Entity.Domain;
using Entity.Enums;
using Services.Features.DTO.Exercise;
using Services.Features.DTO.ExerciseCriteria;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebUI.Mapping.Features
{
    public class FeaturesDomainToDTOMappingProfile: Profile
    {
        public FeaturesDomainToDTOMappingProfile()
        {
            CreateMap<Exercise, ExerciseDTO>().ForMember(s=>s.Criterias, m=>m.MapFrom(z=>z.ExerciseCriterias));
            CreateMap<ExerciseCriteria, ExerciseCriteriaDTO>();
            CreateMap<Exercise, ExerciseDeatailsDTO>().ForMember(s => s.DifficultyLevel,
                m => m.ResolveUsing(mf =>
                {
                    var attributes = (DescriptionAttribute[])Enum.GetUnderlyingType(typeof(DifficultyLevel)).GetCustomAttributes(typeof(DescriptionAttribute), false);
                    return attributes.FirstOrDefault()?.Description;
                }));
        }
    }
}