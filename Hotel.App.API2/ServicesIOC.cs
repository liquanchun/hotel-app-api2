using Hotel.App.Data.Abstract;
using Hotel.App.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.App.API2
{
    public static class ServicesIOC
    {
        public static void Add(ref IServiceCollection services)
        {
            services.AddScoped<ISysOrgRepository, SysOrgRepository>();
            services.AddScoped<ISysFunctionRepository, SysFunctionRepository>();
            services.AddScoped<ISysMenuRepository, SysMenuRepository>();
            services.AddScoped<ISysRoleFunctionRepository, SysRoleFunctionRepository>();
            services.AddScoped<ISysRoleMenuRepository, SysRoleMenuRepository>();

            services.AddScoped<ISysRoleRepository, SysRoleRepository>();
            services.AddScoped<ISysRoleUserRepository, SysRoleUserRepository>();
            services.AddScoped<IUserAccessLogRepository, UserAccessLogRepository>();
            services.AddScoped<IUserLoginLogRepository, UserLoginLogRepository>();
            services.AddScoped<ISysUserRepository, SysUserRepository>();

            services.AddScoped<ISysDicRepository, SysDicRepository>();

            services.AddScoped<ISetAllhousePriceRepository, SetAllhousePriceRepository>();
            services.AddScoped<ISetCardRepository, SetCardRepository>();
            services.AddScoped<ISetCardUpgradeRepository, SetCardUpgradeRepository>();
            services.AddScoped<ISetHourhousePriceRepository, SetHourhousePriceRepository>();
            services.AddScoped<ISetHouseTypeRepository, SetHouseTypeRepository>();
            services.AddScoped<ISetIntegralRepository, SetIntegralRepository>();
            services.AddScoped<ISetInteExchangeRepository, SetInteExchangeRepository>();
            services.AddScoped<ISetInteHouseRepository, SetInteHouseRepository>();
            services.AddScoped<ISetOtherhousePriceRepository, SetOtherhousePriceRepository>();

            services.AddScoped<IFwCleanRepository, FwCleanRepository>();
            services.AddScoped<IFwCusgoodsRepository, FwCusgoodsRepository>();
            services.AddScoped<IFwHouseinfoRepository, FwHouseinfoRepository>();
            services.AddScoped<IFwRepairRepository, FwRepairRepository>();
            services.AddScoped<IFwStatelogRepository, FwStatelogRepository>();

            services.AddScoped<IKcAdddelRepository, KcAdddelRepository>();
            services.AddScoped<IKcGoodsRepository, KcGoodsRepository>();
            services.AddScoped<IKcStoreRepository, KcStoreRepository>();
            services.AddScoped<IKcStoreexcRepository, KcStoreexcRepository>();
            services.AddScoped<IKcStoreinRepository, KcStoreinRepository>();
            services.AddScoped<IKcStoreoutRepository, KcStoreoutRepository>();
            services.AddScoped<IKcSupplierRepository, KcSupplierRepository>();

            services.AddScoped<ISetAgentRepository, SetAgentRepository>();
            services.AddScoped<ISetGroupRepository, SetGroupRepository>();
            services.AddScoped<ISetPaytypeRepository, SetPaytypeRepository>();
            services.AddScoped<ISetRentRepository, SetRentRepository>();

            services.AddScoped<IYxBookRepository, YxBookRepository>();
            services.AddScoped<IYxBooklistRepository, YxBooklistRepository>();
            services.AddScoped<IYxOrderRepository, YxOrderRepository>();
            services.AddScoped<IYxOrderlistRepository, YxOrderlistRepository>();

            services.AddScoped<ISmsSendrecordRepository, SmsSendrecordRepository>();
            services.AddScoped<ISmsSettingRepository, SmsSettingRepository>();
            services.AddScoped<ISmsTemplateRepository, SmsTemplateRepository>();
            services.AddScoped<IYxCustomerRepository, YxCustomerRepository>();
            services.AddScoped<IKcStoreinlistRepository, KcStoreinlistRepository>();

            services.AddScoped<ICwCusaccountRepository, CwCusaccountRepository>();
            services.AddScoped<ICwInvoiceRepository, CwInvoiceRepository>();
            services.AddScoped<ICwPreauthRepository, CwPreauthRepository>();
            services.AddScoped<ICwPrefeeRepository, CwPrefeeRepository>();
            services.AddScoped<IKcStoreoutlistRepository, KcStoreoutlistRepository>();

        }
    }
}
