using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static void RegisterMappings(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.AddProfile(new MappingProfile());
        }
    }
}
