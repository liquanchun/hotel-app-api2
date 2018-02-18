using Hotel.App.Model.House;
using Hotel.App.Model.Sale;
using Hotel.App.Model.Store;
using Hotel.App.Model.SYS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Hotel.App.Data.Abstract;
using Hotel.App.Data.Repositories;
using Hotel.App.Model.Account;

namespace Hotel.App.Data
{
    public static class MyModelBuilder
    {
        public static void Add(ref ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<sys_function>().ToTable("sys_function");

            modelBuilder.Entity<sys_menu>().ToTable("sys_menu");

            modelBuilder.Entity<sys_org>().ToTable("sys_org");
            //modelBuilder.Entity<sys_org>().HasMany(p => p.UserList).WithOne(p => p.Org).HasForeignKey(k => k.OrgId);

            modelBuilder.Entity<sys_role>().ToTable("sys_role");
            modelBuilder.Entity<sys_role>().HasKey("Id");

            modelBuilder.Entity<sys_role_function>().ToTable("sys_role_function");

            modelBuilder.Entity<sys_role_menu>().ToTable("sys_role_menu");

            modelBuilder.Entity<sys_role_user>().ToTable("sys_role_user");
            //modelBuilder.Entity<sys_role_user>().HasOne(a => a.Role).WithMany(u => u.RoleUserList).HasForeignKey(a => a.RoleId);
            //modelBuilder.Entity<sys_role_user>().HasOne(a => a.User).WithMany(u => u.RoleUserList).HasForeignKey(a => a.UserId);


            modelBuilder.Entity<sys_user>().ToTable("sys_user");
            modelBuilder.Entity<sys_user>().HasKey("Id");
            //modelBuilder.Entity<sys_user>().HasOne(a => a.Org).WithMany(u => u.UserList).HasForeignKey(a => a.OrgId);

            modelBuilder.Entity<user_access_log>().ToTable("user_access_log");

            modelBuilder.Entity<user_login_log>().ToTable("user_login_log");

            modelBuilder.Entity<sys_dic>().ToTable("sys_dic");

            modelBuilder.Entity<set_allhouse_price>().ToTable("set_allhouse_price");
            modelBuilder.Entity<set_card>().ToTable("set_card");
            modelBuilder.Entity<set_card_upgrade>().ToTable("set_card_upgrade");
            modelBuilder.Entity<set_hourhouse_price>().ToTable("set_hourhouse_price");
            modelBuilder.Entity<set_house_type>().ToTable("set_house_type");
            modelBuilder.Entity<set_integral>().ToTable("set_integral");
            modelBuilder.Entity<set_inte_exchange>().ToTable("set_inte_exchange");
            modelBuilder.Entity<set_inte_house>().ToTable("set_inte_house");
            modelBuilder.Entity<set_otherhouse_price>().ToTable("set_otherhouse_price");

            modelBuilder.Entity<fw_clean>().ToTable("fw_clean");
            modelBuilder.Entity<fw_cusgoods>().ToTable("fw_cusgoods");
            modelBuilder.Entity<fw_houseinfo>().ToTable("fw_houseinfo");
            modelBuilder.Entity<fw_repair>().ToTable("fw_repair");
            modelBuilder.Entity<fw_statelog>().ToTable("fw_statelog");

            modelBuilder.Entity<kc_adddel>().ToTable("kc_adddel");
            modelBuilder.Entity<kc_goods>().ToTable("kc_goods");
            modelBuilder.Entity<kc_store>().ToTable("kc_store");
            modelBuilder.Entity<kc_storeexc>().ToTable("kc_storeexc");
            modelBuilder.Entity<kc_storein>().ToTable("kc_storein");
            modelBuilder.Entity<kc_storeout>().ToTable("kc_storeout");
            modelBuilder.Entity<kc_supplier>().ToTable("kc_supplier");

            modelBuilder.Entity<set_agent>().ToTable("set_agent");
            modelBuilder.Entity<set_group>().ToTable("set_group");
            modelBuilder.Entity<set_paytype>().ToTable("set_paytype");
            modelBuilder.Entity<set_rent>().ToTable("set_rent");

            modelBuilder.Entity<yx_book>().ToTable("yx_book");
            modelBuilder.Entity<yx_booklist>().ToTable("yx_booklist");
            modelBuilder.Entity<yx_order>().ToTable("yx_order");
            modelBuilder.Entity<yx_orderlist>().ToTable("yx_orderlist");

            modelBuilder.Entity<sms_sendrecord>().ToTable("sms_sendrecord");
            modelBuilder.Entity<sms_setting>().ToTable("sms_setting");
            modelBuilder.Entity<sms_template>().ToTable("sms_template");

            modelBuilder.Entity<yx_customer>().ToTable("yx_customer");
            modelBuilder.Entity<kc_storeinlist>().ToTable("kc_storeinlist");
            modelBuilder.Entity<kc_storeoutlist>().ToTable("kc_storeoutlist");

            modelBuilder.Entity<cw_cusaccount>().ToTable("cw_cusaccount");
            modelBuilder.Entity<cw_invoice>().ToTable("cw_invoice");
            modelBuilder.Entity<cw_preauth>().ToTable("cw_preauth");
            modelBuilder.Entity<cw_prefee>().ToTable("cw_prefee");

        }
    }
}
