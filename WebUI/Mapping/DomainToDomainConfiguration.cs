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
        }
    }
}