using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.App.Data.Abstract;
using Hotel.App.Model.SYS;
using AutoMapper;
using Hotel.App.API2.Core;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    public class SysMenuController : Controller
    {
        private readonly IMapper _mapper;
        private ISysMenuRepository _sysMenuRpt;
        private ISysRoleMenuRepository _sysRoleMenuRpt;
        private ISysRoleRepository _sysRoleRpt;
        public SysMenuController(ISysMenuRepository sysMenuRpt,
            ISysRoleMenuRepository sysRoleMenuRpt,
            ISysRoleRepository sysRoleRpt,
            IMapper mapper)
        {
            _sysMenuRpt = sysMenuRpt;
            _sysRoleMenuRpt = sysRoleMenuRpt;
            _sysRoleRpt = sysRoleRpt;
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
        public IActionResult Post([FromBody]sys_menu value)
        {
            var oldSysMenu = _sysMenuRpt.FindBy(f => f.MenuName == value.MenuName);
            if(oldSysMenu.Count() > 0)
            {
                return BadRequest(string.Concat(value.MenuName, "已经存在。"));
            }
            value.CreatedAt = DateTime.Now;
            value.UpdatedAt = DateTime.Now;
            _sysMenuRpt.Add(value);
            _sysMenuRpt.Commit();
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
            return new OkObjectResult(value);
        }
        /// <summary>
        /// 设置用户所属组织
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uid"></param>
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
            var _menuDb = _sysMenuRpt.GetSingle(id);

            if (_menuDb == null)
            {
                return NotFound();
            }
            else
            {
                _menuDb.MenuName = value.MenuName;
                _menuDb.MenuOrder = value.MenuOrder;
                _menuDb.RoleIds = value.RoleIds;
                _menuDb.MenuAddr = value.MenuAddr;
                _menuDb.Icon = value.Icon;
                _menuDb.UpdatedAt = DateTime.Now;
                _sysMenuRpt.Commit();
            }
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            sys_menu _sysMenu = _sysMenuRpt.GetSingle(id);
            if (_sysMenu == null)
            {
                return new NotFoundResult();
            }
            else
            {
                _sysRoleMenuRpt.DeleteWhere(f => f.MenuId == id);
                _sysRoleMenuRpt.Commit();

                _sysMenu.IsValid = false;
                _sysMenuRpt.Commit();

                return new NoContentResult();
            }
        }
        /// <summary>
        /// 删除用户组织
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uid"></param>
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
