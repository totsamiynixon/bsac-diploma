using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using WebUI.Mapping.Features;

namespace WebUI.Mapping
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToDTOMappingProfile>();
                x.AddProfile<DTOToDomainMappingProfile>();
                x.AddProfile<DomainToDomainConfiguration>();
                x.AddProfile<DtoToDtoConfiguration>();
                x.AddProfile<FeaturesDomainToDTOMappingProfile>();
            });
        }
    }
}