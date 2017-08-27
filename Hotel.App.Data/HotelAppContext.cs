using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hotel.App.Model;
using Hotel.App.Model.SYS;
using Microsoft.EntityFrameworkCore.Metadata;
using Hotel.App.Data.Repositories.SYS;

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

        public HotelAppContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            _0SYSBuilder.Add(ref modelBuilder);
        }
    }
}
