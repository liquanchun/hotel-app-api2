using Hotel.App.Model;
using Hotel.App.Model.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    public interface ISysDicRepository : IEntityBaseRepository<sys_dic> { }

    public interface IUserAccessLogRepository : IEntityBaseRepository<user_access_log> { }

    public interface IUserLoginLogRepository : IEntityBaseRepository<user_login_log> { }
}
