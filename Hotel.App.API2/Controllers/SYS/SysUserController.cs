using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.App.Data.Abstract;
using Hotel.App.Model.SYS;
using AutoMapper;
using NLog;
using System.Security.Claims;
using Hotel.App.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SysUserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISysUserRepository _sysUserRpt;
        private readonly ISysRoleUserRepository _sysRoleUserRpt;
        private readonly ISysRoleRepository _sysRoleRpt;
        private readonly HotelAppContext _context;
        public SysUserController(ISysUserRepository sysUserRpt, 
            ISysRoleUserRepository sysRoleUserRpt, 
            ISysRoleRepository sysRoleRpt,
            ISysOrgRepository orgRepository,
        HotelAppContext context,
            IMapper mapper)
        {
            _sysUserRpt = sysUserRpt;
            _sysRoleUserRpt = sysRoleUserRpt;
            _sysRoleRpt = sysRoleRpt;
            _context = context;
            _mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<SysUserDto> entityDto = null;
            var users = _sysUserRpt.FindBy(f => f.IsDelete == false);
            entityDto = _mapper.Map<IEnumerable<sys_user>, IEnumerable<SysUserDto>>(users);

            foreach (var item in entityDto)
            {
                //角色名称转换
                List<string> roleName = new List<string>();
                if (!string.IsNullOrEmpty(item.RoleIds))
                {
                    string[] roleid = item.RoleIds.Split(",".ToCharArray());
                    for (int i = 0; i < roleid.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(roleid[i]))
                        {
                            var role = _sysRoleRpt.GetSingle(int.Parse(roleid[i]));
                            if (role != null)
                            {
                                roleName.Add(role.RoleName);
                            }
                        }
                    }
                }
                item.RoleNames = string.Join(",", roleName);
            }
            return new OkObjectResult(entityDto.ToList().OrderBy(f => f.UserName));
        }

        // GET api/values/5
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            var single = _sysUserRpt.GetSingle(f => f.UserId == userId);
            return new OkObjectResult(single);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]sys_user value)
        {
            using (var tran = _context.Database.BeginTransaction())
            {
                try
                {
                    var oldSysUser = _sysUserRpt.FindBy(f => f.UserId == value.UserId);
                    if (oldSysUser.Any())
                    {
                        return BadRequest(string.Concat(value.UserId, "已经存在。"));
                    }
                    value.CreatedAt = DateTime.Now;
                    value.UpdatedAt = DateTime.Now;
                    value.IsDelete = false;
                    if (User.Identity is ClaimsIdentity identity)
                    {
                        value.CreatedBy = identity.Name ?? "test";
                    }
                    _sysUserRpt.Add(value);
                    _sysUserRpt.Commit();

                    if (!string.IsNullOrEmpty(value.RoleIds) && value.RoleIds.Length > 1)
                    {
                        //新增用户角色关系表
                        string[] roles = value.RoleIds.Split(",".ToArray());
                        foreach (var item in roles)
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                var userrole = new sys_role_user { RoleId = int.Parse(item), UserId = value.Id };
                                _sysRoleUserRpt.Add(userrole);
                            }
                        }
                        _sysRoleUserRpt.Commit();
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    tran.Rollback();
                    return BadRequest(ex);
                }
            }
            return new OkObjectResult(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]sys_user value)
        {
            if (id == 0)
            {
                //修改密码
                var usr = _sysUserRpt.GetSingle(f => f.UserId == value.UserId);
                if (usr != null)
                {
                    usr.Pwd = value.Pwd;
                    _sysUserRpt.Update(usr);
                    _sysUserRpt.Commit();
                }
            }
            else
            {
                using (var tran = _context.Database.BeginTransaction())
                {
                    try
                    {
                        sys_user userDb = _sysUserRpt.GetSingle(id);
                        if (userDb == null)
                        {
                            return NotFound();
                        }
                        userDb.IsValid = value.IsValid;
                        userDb.Mobile = value.Mobile;
                        userDb.WebChat = value.WebChat;
                        userDb.UserId = value.UserId;
                        userDb.UserName = value.UserName;
                        userDb.UpdatedAt = DateTime.Now;
                        userDb.RoleIds = value.RoleIds;
                        _sysUserRpt.Commit();

                        if (value.RoleIds != userDb.RoleIds)
                        {
                            //修改了用户角色
                            _sysRoleUserRpt.DeleteWhere(f => f.UserId == id);
                            _sysRoleUserRpt.Commit();

                            //新增用户角色关系表
                            string[] roles = value.RoleIds.Split(",".ToArray());
                            foreach (var item in roles)
                            {
                                if (!string.IsNullOrEmpty(item))
                                {
                                    var userrole = new sys_role_user {RoleId = int.Parse(item), UserId = id};
                                    _sysRoleUserRpt.Add(userrole);
                                }
                            }
                            _sysRoleUserRpt.Commit();
                        }
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        tran.Rollback();
                        return BadRequest(ex);
                    }
                }
            }
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            sys_user sysUser = _sysUserRpt.GetSingle(id);
            if (sysUser == null)
            {
                return new NotFoundResult();
            }
            sysUser.IsDelete = true;
            _sysUserRpt.Commit();
            return new NoContentResult();
        }
    }
}
