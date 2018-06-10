using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Entity.Domain;

namespace WebUI.Mapping
{
    public class DomainToDomainConfiguration : Profile
    {
        public DomainToDomainConfiguration()
        {
            CreateMap<Criteria, Criteria>();
            CreateMap<Exercise, Exercise>()
                .ForMember(f => f.ExerciseCriterias, m => m.Ignore());
            CreateMap<ExerciseCriteria, ExerciseCriteria>()
                .ForMember(z => z.Exercise, m => m.Ignore())
                .ForMember(z => z.Criteria, m => m.Ignore());
            CreateMap<Profession, Profession>()
    .ForMember(f => f.ProfessionCriterias, m => m.Ignore());
            CreateMap<ProfessionCriteria, ProfessionCriteria>()
                .ForMember(z => z.Profession, m => m.Ignore())
                .ForMember(z => z.Criteria, m => m.Ignore());
        }
    }
}