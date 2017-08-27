using Hotel.App.Model.SYS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.App.Data.Repositories.SYS
{
    public static class _0SYSBuilder
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
        }
    }
}
