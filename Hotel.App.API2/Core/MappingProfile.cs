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

            CreateMap<set_paytype, SetPaytypeDto>()
                .ForMember(d => d.IsReturnT, opt => opt.MapFrom(s => s.IsReturn ? "是":"否"))
                .ForMember(d => d.IsIntegralT, opt => opt.MapFrom(s => s.IsIntegral ? "是" : "否"))
                .ForMember(d => d.IsDefaultT, opt => opt.MapFrom(s => s.IsDefault ? "是" : "否"));
            CreateMap<SetPaytypeDto, set_paytype>()
                .ForMember(d => d.IsReturn, opt => opt.MapFrom(s => s.IsReturnT == "是"))
                .ForMember(d => d.IsIntegral, opt => opt.MapFrom(s => s.IsIntegralT == "是"))
                .ForMember(d => d.IsDefault, opt => opt.MapFrom(s => s.IsDefaultT == "是"));
        }
    }
}
