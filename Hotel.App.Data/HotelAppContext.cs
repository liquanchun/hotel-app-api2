using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hotel.App.Model;
using Hotel.App.Model.Account;
using Hotel.App.Model.SYS;
using Microsoft.EntityFrameworkCore.Metadata;
using Hotel.App.Model.House;
using Hotel.App.Model.Store;
using Hotel.App.Model.Sale;

namespace Hotel.App.Data
{
    public class HotelAppContext : DbContext
    {
        public DbSet<sys_function> SysFunctions { get; set; }
        public DbSet<sys_menu> SysMenus { get; set; }
        public DbSet<sys_org> SysOrgs { get; set; }
        public DbSet<sys_role> SysRoles { get; set; }
        public DbSet<sys_role_function> SysRoleFunctions { get; set; }

        public DbSet<sys_role_menu> SysRoleMenus { get; set; }
        public DbSet<sys_role_user> SysRoleUsers { get; set; }
        public DbSet<sys_user> SysUsers { get; set; }
        public DbSet<user_access_log> UserAccessLogs { get; set; }
        public DbSet<user_login_log> UserLoginLogs { get; set; }

        public DbSet<sys_dic> SysDics { get; set; }

        public DbSet<set_allhouse_price> SetAllhousePrices { get; set; }
        public DbSet<set_card> SetCards { get; set; }
        public DbSet<set_card_upgrade> SetCardUpgrades { get; set; }
        public DbSet<set_hourhouse_price> SetHourhousePrices { get; set; }
        public DbSet<set_house_type> SetHouseTypes { get; set; }
        public DbSet<set_integral> SetIntegrals { get; set; }
        public DbSet<set_inte_exchange> SetInteExchanges { get; set; }
        public DbSet<set_inte_house> SetInteHouses { get; set; }
        public DbSet<set_otherhouse_price> SetOtherhousePrices { get; set; }

        public DbSet<set_agent> SetAgents { get; set; }
        public DbSet<set_group> SetGroups { get; set; }
        public DbSet<set_paytype> SetPaytypes { get; set; }
        public DbSet<set_rent> SetRents { get; set; }

        public DbSet<fw_clean> FwCleans { get; set; }
        public DbSet<fw_cusgoods> FwCusgoodss { get; set; }
        public DbSet<fw_houseinfo> FwHouseinfos { get; set; }
        public DbSet<fw_repair> FwRepairs { get; set; }
        public DbSet<fw_statelog> FwStatelogs { get; set; }

        public DbSet<kc_adddel> KcAdddels { get; set; }
        public DbSet<kc_goods> KcGoodss { get; set; }
        public DbSet<kc_store> KcStores { get; set; }
        public DbSet<kc_storeexc> KcStoreexcs { get; set; }
        public DbSet<kc_storein> KcStoreins { get; set; }
        public DbSet<kc_storeout> KcStoreouts { get; set; }
        public DbSet<kc_supplier> KcSuppliers { get; set; }

        public DbSet<yx_book> YxBooks { get; set; }
        public DbSet<yx_booklist> YxBooklists { get; set; }

        public DbSet<yx_order> YxOrders { get; set; }
        public DbSet<yx_orderlist> YxOrderlists { get; set; }
        public DbSet<sms_sendrecord> SmsSendrecords { get; set; }
        public DbSet<sms_setting> SmsSettings { get; set; }
        public DbSet<sms_template> SmsTemplates { get; set; }

        public DbSet<yx_customer> YxCustomers { get; set; }

        public DbSet<kc_storeinlist> KcStoreinlists { get; set; }

        public DbSet<cw_cusaccount> CwCusaccounts { get; set; }
        public DbSet<cw_invoice> CwInvoices { get; set; }
        public DbSet<cw_preauth> CwPreauths { get; set; }
        public DbSet<cw_prefee> CwPrefees { get; set; }

        public DbSet<yx_serviceitem> YxServiceitems { get; set; }
        public DbSet<yx_bookservice> YxBookservices { get; set; }
        public DbSet<yx_orderservice> YxOrderservices { get; set; }

        public DbSet<sys_staff> SysStaffs { get; set; }

        public DbSet<set_card_discount> SetCardDiscounts { get; set; }
        public DbSet<set_card_exchange> SetCardExchanges { get; set; }
        public HotelAppContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            MyModelBuilder.Add(ref modelBuilder);
        }
    }
}
