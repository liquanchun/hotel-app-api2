using Hotel.App.Model;
using Hotel.App.Model.House;
using Hotel.App.Model.Sale;
using Hotel.App.Model.Store;
using Hotel.App.Model.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.App.Model.Account;

namespace Hotel.App.Data.Abstract
{
    public interface ISysFunctionRepository : IEntityBaseRepository<sys_function> { }

    public interface ISysMenuRepository : IEntityBaseRepository<sys_menu> { }

    public interface ISysOrgRepository : IEntityBaseRepository<sys_org> { }

    public interface ISysRoleRepository : IEntityBaseRepository<sys_role> { }

    public interface ISysRoleFunctionRepository : IEntityBaseRepository<sys_role_function> { }

    public interface ISysRoleMenuRepository : IEntityBaseRepository<sys_role_menu> { }

    public interface ISysRoleUserRepository : IEntityBaseRepository<sys_role_user> { }

    public interface ISysUserRepository : IEntityBaseRepository<sys_user> { }

    public interface ISysStaffRepository : IEntityBaseRepository<sys_staff> { }

    public interface ISysDicRepository : IEntityBaseRepository<sys_dic> { }

    public interface IUserAccessLogRepository : IEntityBaseRepository<user_access_log> { }

    public interface IUserLoginLogRepository : IEntityBaseRepository<user_login_log> { }

    public interface ISetAllhousePriceRepository : IEntityBaseRepository<set_allhouse_price> { }
    public interface ISetCardRepository : IEntityBaseRepository<set_card> { }
    public interface ISetCardUpgradeRepository : IEntityBaseRepository<set_card_upgrade> { }
    public interface ISetHourhousePriceRepository : IEntityBaseRepository<set_hourhouse_price> { }
    public interface ISetHouseTypeRepository : IEntityBaseRepository<set_house_type> { }
    public interface ISetIntegralRepository : IEntityBaseRepository<set_integral> { }
    public interface ISetInteExchangeRepository : IEntityBaseRepository<set_inte_exchange> { }
    public interface ISetInteHouseRepository : IEntityBaseRepository<set_inte_house> { }
    public interface ISetOtherhousePriceRepository : IEntityBaseRepository<set_otherhouse_price> { }

    public interface IFwCleanRepository : IEntityBaseRepository<fw_clean> { }
    public interface IFwCusgoodsRepository : IEntityBaseRepository<fw_cusgoods> { }
    public interface IFwHouseinfoRepository : IEntityBaseRepository<fw_houseinfo> { }
    public interface IFwRepairRepository : IEntityBaseRepository<fw_repair> { }
    public interface IFwStatelogRepository : IEntityBaseRepository<fw_statelog> { }

    public interface IKcAdddelRepository : IEntityBaseRepository<kc_adddel> { }
    public interface IKcGoodsRepository : IEntityBaseRepository<kc_goods> { }
    public interface IKcStoreRepository : IEntityBaseRepository<kc_store> { }
    public interface IKcStoreexcRepository : IEntityBaseRepository<kc_storeexc> { }
    public interface IKcStoreinRepository : IEntityBaseRepository<kc_storein> { }
    public interface IKcStoreoutRepository : IEntityBaseRepository<kc_storeout> { }
    public interface IKcSupplierRepository : IEntityBaseRepository<kc_supplier> { }

    public interface ISetAgentRepository : IEntityBaseRepository<set_agent> { }
    public interface ISetGroupRepository : IEntityBaseRepository<set_group> { }
    public interface ISetPaytypeRepository : IEntityBaseRepository<set_paytype> { }
    public interface ISetRentRepository : IEntityBaseRepository<set_rent> { }

    public interface IYxBookRepository : IEntityBaseRepository<yx_book> { }
    public interface IYxBooklistRepository : IEntityBaseRepository<yx_booklist> { }

    public interface IYxOrderRepository : IEntityBaseRepository<yx_order> { }
    public interface IYxOrderlistRepository : IEntityBaseRepository<yx_orderlist> { }

    public interface ISmsSendrecordRepository : IEntityBaseRepository<sms_sendrecord> { }
    public interface ISmsSettingRepository : IEntityBaseRepository<sms_setting> { }
    public interface ISmsTemplateRepository : IEntityBaseRepository<sms_template> { }
    public interface IYxCustomerRepository : IEntityBaseRepository<yx_customer> { }

    public interface IKcStoreinlistRepository : IEntityBaseRepository<kc_storeinlist> { }

    public interface ICwCusaccountRepository : IEntityBaseRepository<cw_cusaccount> { }
    public interface ICwInvoiceRepository : IEntityBaseRepository<cw_invoice> { }
    public interface ICwPreauthRepository : IEntityBaseRepository<cw_preauth> { }
    public interface ICwPrefeeRepository : IEntityBaseRepository<cw_prefee> { }
    public interface IKcStoreoutlistRepository : IEntityBaseRepository<kc_storeoutlist> { }

    public interface IYxServiceitemRepository : IEntityBaseRepository<yx_serviceitem> { }

    public interface IYxBookserviceRepository : IEntityBaseRepository<yx_bookservice> { }
    public interface IYxOrderserviceRepository : IEntityBaseRepository<yx_orderservice> { }

    public interface ISetCardDiscountRepository : IEntityBaseRepository<set_card_discount> { }
    public interface ISetCardExchangeRepository : IEntityBaseRepository<set_card_exchange> { }

}
