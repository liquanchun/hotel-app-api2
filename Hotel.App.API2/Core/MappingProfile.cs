using AutoMapper;
using Hotel.App.Model.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.App.API2.Core
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<sys_user, SysUserDto>();
            CreateMap<SysUserDto, sys_user>();

            CreateMap<set_card, SetCardDto>();
            CreateMap<SetCardDto, set_card>();
        }
    }
}
