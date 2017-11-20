using AutoMapper;
using Hotel.App.Model.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.App.Model.Dto;
using Hotel.App.Model.House;
using Hotel.App.Model.Sale;
using StackExchange.Redis;

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

            CreateMap<fw_houseinfo, HouseinfoDto>();
            CreateMap<HouseinfoDto, fw_houseinfo>();

            CreateMap<set_integral, SetCardIntegralDto>();
            CreateMap<SetCardIntegralDto, set_integral>();

            CreateMap<set_card_upgrade, SetCardUpgradeDto>();
            CreateMap<SetCardUpgradeDto, set_card_upgrade>();

            CreateMap<set_inte_exchange, SetIneExchangDto>();
            CreateMap<SetIneExchangDto, set_inte_exchange>();

            CreateMap<set_inte_house, SetInteHouseDto>();
            CreateMap<SetInteHouseDto, set_inte_house>();

            CreateMap<yx_order, OrderDto>();
            CreateMap<OrderDto, yx_order>();

            CreateMap<yx_orderlist, OrderListDto>();
            CreateMap<OrderListDto, yx_orderlist>();

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
