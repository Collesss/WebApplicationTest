using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppDbModels.Models;
using WebApplicationTest.ViewsModels.Account;

namespace WebApplicationTest.AutoMapperConfigures
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<RegisterViewModel, User>()
                .ForMember(dest => dest.HashPassword, opts => opts.MapFrom(source => source.Password));
                
        }
    }
}
