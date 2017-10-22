using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.App.Data.Abstract;
using Hotel.App.Model.SYS;
using AutoMapper;
using Hotel.App.API2.Core;
using Hotel.App.Data;
using Microsoft.Azure.KeyVault.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    public class SysMenuController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISysMenuRepository _sysMenuRpt;
        private readonly ISysRoleMenuRepository _sysRoleMenuRpt;
        private ISysRoleRepository _sysRoleRpt;
        private readonly HotelAppContext _context;

        public SysMenuController(ISysMenuRepository sysMenuRpt,
            ISysRoleMenuRepository sysRoleMenuRpt,
            ISysRoleRepository sysRoleRpt,
            HotelAppContext context,
            IMapper mapper)
        {
            _sysMenuRpt = sysMenuRpt;
            _sysRoleMenuRpt = sysRoleMenuRpt;
            _sysRoleRpt = sysRoleRpt;
            _context = context;
            _mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<sys_menu> _menusVM = _sysMenuRpt.FindBy(f => f.IsValid).OrderBy(f => f.MenuOrder);
            //var entityDto = _mapper.Map<IEnumerable<sys_menu>, IEnumerable<SysMenuDto>>(_menusVM);
            //foreach (var item in entityDto)
            //{
            //    //角色名称转换
            //    List<string> roleName = new List<string>();
            //    if (!string.IsNullOrEmpty(item.RoleIds))
            //    {
            //        string[] roleid = item.RoleIds.Split(",".ToCharArray());
            //        for (int i = 0; i < roleid.Length; i++)
            //        {
            //            var role = _sysRoleRpt.GetSingle(int.Parse(roleid[i]));
            //            if (role != null)
            //            {
            //                roleName.Add(role.RoleName);
            //            }
            //        }
            //    }
            //    item.RoleNames = string.Join(",", roleName);
            //}

            return new OkObjectResult(_menusVM);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        /// <summary>
        /// 获取组织下面的用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/roles", Name = "GetRoleList")]
        public IActionResult GetRoleList(int id)
        {
            return new OkObjectResult(_sysRoleMenuRpt.FindBy(f => f.MenuId == id));
        }
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] sys_menu value)
        {
            var oldSysMenu = _sysMenuRpt.FindBy(f => f.MenuName == value.MenuName);
            if (oldSysMenu.Any())
            {
                return BadRequest(string.Concat(value.MenuName, "已经存在。"));
            }
            value.CreatedAt = DateTime.Now;
            value.UpdatedAt = DateTime.Now;
            value.IsValid = true;

            _sysMenuRpt.Add(value);
            using (var tran = _context.Database.BeginTransaction())
            {
                try
                {
                    _sysMenuRpt.Commit();
                    this.SetMenuRoles(value);
                    tran.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    tran.Rollback();
                    return new BadRequestResult();;
                }
            }
            return new OkObjectResult(value);
        }

        /// <summary>
        /// 设置菜单权限
        /// </summary>
        /// <param name="value"></param>
        private void SetMenuRoles(sys_menu value)
        {
            _sysRoleMenuRpt.DeleteWhere(f => f.MenuId == value.Id);
            if (!string.IsNullOrEmpty(value.RoleIds) && value.RoleIds.Length > 1)
            {
                //新增用户角色关系表
                string[] roles = value.RoleIds.Split(",".ToArray());
                foreach (var item in roles)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        var userrole = new sys_role_menu { RoleId = int.Parse(item), MenuId = value.Id };
                        _sysRoleMenuRpt.Add(userrole);
                    }
                }
                _sysRoleMenuRpt.Commit();
            }
        }
        /// <summary>
        /// 设置用户所属组织
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rid"></param>
        /// <returns></returns>
        // POST api/values
        [HttpPost("{id}/{rid}", Name = "NewRoleMenu")]
        public IActionResult NewRoleMenu(int id,int rid)
        {
            _sysRoleMenuRpt.Add(new sys_role_menu { MenuId= id, RoleId = rid });
            _sysRoleMenuRpt.Commit();
            return new NoContentResult();
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]sys_menu value)
        {
            var menuDb = _sysMenuRpt.GetSingle(id);

            if (menuDb == null)
            {
                return NotFound();
            }
            menuDb.MenuName = value.MenuName;
            menuDb.MenuOrder = value.MenuOrder;
            menuDb.RoleIds = value.RoleIds;
            menuDb.MenuAddr = value.MenuAddr;
            menuDb.Icon = value.Icon;
            menuDb.UpdatedAt = DateTime.Now;
            menuDb.IsValid = true;
            using (var tran = _context.Database.BeginTransaction())
            {
                try
                {
                    _sysMenuRpt.Commit();
                    this.SetMenuRoles(menuDb);
                    tran.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    tran.Rollback();
                    return new BadRequestResult(); ;
                }
            }
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            sys_menu sysMenu = _sysMenuRpt.GetSingle(id);
            if (sysMenu == null)
            {
                return new NotFoundResult();
            }
            _sysRoleMenuRpt.DeleteWhere(f => f.MenuId == id);
            _sysRoleMenuRpt.Commit();

            sysMenu.IsValid = false;
            _sysMenuRpt.Commit();

            return new NoContentResult();
        }

        /// <summary>
        /// 删除用户组织
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rid"></param>
        /// <returns></returns>
        [HttpDelete("{id}/{rid}",Name ="DeleteRoleMenu")]
        public IActionResult DeleteRoleMenu(int id,int rid)
        {
            _sysRoleMenuRpt.DeleteWhere(f => f.MenuId == id && f.RoleId == rid);
            _sysRoleMenuRpt.Commit();
            return new NoContentResult();
        }
    }
}
