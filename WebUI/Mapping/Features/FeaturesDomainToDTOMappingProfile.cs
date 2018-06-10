using AutoMapper;
using Entity.Domain;
using Entity.Domain.Training;
using Entity.Enums;
using Services.Features.DTO.Exercise;
using Services.Features.DTO.ExerciseCriteria;
using Services.Features.DTO.UserTraining;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using UnconstrainedMelody;

namespace WebUI.Mapping.Features
{
    public class FeaturesDomainToDTOMappingProfile : Profile
    {
        public FeaturesDomainToDTOMappingProfile()
        {
            CreateMap<Exercise, ExerciseDTO>().ForMember(s => s.Criterias, m => m.MapFrom(z => z.ExerciseCriterias));
            CreateMap<ExerciseCriteria, ExerciseCriteriaDTO>();
            CreateMap<Exercise, ExerciseDeatailsDTO>().ForMember(s => s.DifficultyLevel,
                m => m.ResolveUsing(mf =>
                {
                    return Enums.GetDescription(mf.DifficultyLevel);
                }));
            CreateMap<UserExercise, UserTrainingExerciseDTO>()
                .ForMember(s => s.Name, m => m.MapFrom(mf => mf.Exercise.Name))
                .ForMember(s => s.PreviewText, m => m.MapFrom(mf => mf.Exercise.PreviewText))
                .ForMember(s => s.VideoId, m => m.MapFrom(mf => mf.Exercise.VideoId));
            CreateMap<UserTraining, UserTrainingDTO>().ForMember(s => s.Exercises, m => m.ResolveUsing(mp =>
            {
                return mp.Exercises.GroupBy(s => s.Exercise.DifficultyLevel).OrderBy(s => s.Key).ToDictionary(s => Enums.GetDescription(s.Key), z => Mapper.Map<List<UserExercise>, List<UserTrainingExerciseDTO>>(z.OrderBy(f => f.Exercise.Name).ToList()));
            }));
        }
    }
}