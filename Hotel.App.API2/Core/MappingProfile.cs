using AutoMapper;
using Hotel.App.Model.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.App.Model.Account;
using Hotel.App.Model.Dto;
using Hotel.App.Model.House;
using Hotel.App.Model.Sale;
using Hotel.App.Model.Store;
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

            CreateMap<sys_staff, SysStaffDto>();
            CreateMap<SysStaffDto, sys_staff>();

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

            CreateMap<fw_statelog, HouseStateLogDto>();
            CreateMap<HouseStateLogDto, fw_statelog>();

            CreateMap<yx_customer, CustomerDto>();
            CreateMap<CustomerDto, yx_customer>();

            CreateMap<kc_goods, GoodsDto>();
            CreateMap<GoodsDto, kc_goods>();

            CreateMap<cw_prefee, CWPreFeeDto>();
            CreateMap<CWPreFeeDto, cw_prefee>();

            CreateMap<cw_cusaccount, CWCusAccountDto>();
            CreateMap<CWCusAccountDto, cw_cusaccount>();

            CreateMap<fw_clean, FWCleanDto>();
            CreateMap<FWCleanDto, fw_clean>();

            CreateMap<kc_storein, StoreInGridDto>();
            CreateMap<StoreInGridDto, kc_storein>();

            CreateMap<kc_storeinlist, StoreInListDto>();
            CreateMap<StoreInListDto, kc_storeinlist>();

            CreateMap<kc_storeout, StoreOutGridDto>();
            CreateMap<StoreOutGridDto, kc_storeout>();

            CreateMap<kc_storeoutlist, StoreOutListDto>();
            CreateMap<StoreOutListDto, kc_storeoutlist>();

            CreateMap<kc_store, StoreDto>();
            CreateMap<StoreDto, kc_store>();

            CreateMap<yx_serviceitem, ServiceItemDto>();
            CreateMap<ServiceItemDto, yx_serviceitem>();

            CreateMap<yx_bookservice, BookServiceDto>();
            CreateMap<BookServiceDto, yx_bookservice>();

            CreateMap<yx_orderservice, OrderServiceDto>();
            CreateMap<OrderServiceDto, yx_orderservice>();

            CreateMap<yx_book, BookingDto>();
            CreateMap<BookingDto, yx_book>();

            CreateMap<set_integral, SetCardIntegralDto>();
            CreateMap<SetCardIntegralDto, set_integral>();

            CreateMap<set_card_discount, SetCardDiscountDto>();
            CreateMap<SetCardDiscountDto, set_card_discount>();

            CreateMap<set_card_exchange, SetCardExchangeDto>();
            CreateMap<SetCardExchangeDto, set_card_exchange>();

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
