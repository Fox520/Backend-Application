using AutoMapper;
using Innoloft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Innoloft
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            // No need to use `forMember` since src & dst properties have same names
            //CreateMap<src, dst>();
            CreateMap<Product, ProductDTO>();
            CreateMap<User, UserDTO>();
        }
    }
}
