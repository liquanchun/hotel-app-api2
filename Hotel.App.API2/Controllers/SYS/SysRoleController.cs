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
    public class SysRoleController : Controller
    {
        private ISysRoleRepository _sysRoleRpt;
        private ISysUserRepository _sysUserRpt;
        private ISysRoleUserRepository _sysRoleUserRpt;
        public SysRoleController(ISysRoleRepository sysRoleRpt,ISysUserRepository sysUserRpt,ISysRoleUserRepository sysRoleUserRpt)
        {
            _sysRoleRpt = sysRoleRpt;
            _sysUserRpt = sysUserRpt;
            _sysRoleUserRpt = sysRoleUserRpt;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult(_sysRoleRpt.FindBy(f=> f.IsValid).ToList());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]sys_role value)
        {
            var oldSysRole = _sysRoleRpt.FindBy(f => f.RoleName == value.RoleName);
            if(oldSysRole.Count() > 0)
            {
                return BadRequest(string.Concat(value.RoleName, "已经存在。"));
            }
            value.CreatedAt = DateTime.Now;
            value.UpdatedAt = DateTime.Now;
            _sysRoleRpt.Add(value);
            _sysRoleRpt.Commit();
            return new OkObjectResult(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            sys_role _sysRole = _sysRoleRpt.GetSingle(id);
            if (_sysRole == null)
            {
                return new NotFoundResult();
            }
            else
            {
                if(_sysRoleUserRpt.FindBy(f => f.RoleId == _sysRole.Id).Count() > 0)
                {
                    return BadRequest(string.Concat(_sysRole.RoleName, "已经关联用户，不能删除。"));
                }
                _sysRole.IsValid = false;
                _sysRoleRpt.Commit();

                return new NoContentResult();
            }
        }
    }
}
