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
    public class SysOrgController : Controller
    {
        private ISysOrgRepository _sysOrgRpt;
        private ISysUserRepository _sysUserRpt;
        public SysOrgController(ISysOrgRepository sysOrgRpt,ISysUserRepository sysUserRpt)
        {
            _sysOrgRpt = sysOrgRpt;
            _sysUserRpt = sysUserRpt;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult(_sysOrgRpt.FindBy(f => f.IsValid ));
        }
        /// <summary>
        /// 获取组织下面的用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/users",Name ="GetUserList")]
        public IActionResult GetUserList(int id)
        {
            return new OkObjectResult(_sysUserRpt.FindBy(f => f.IsValid && f.OrgId == id).ToList());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]sys_org value)
        {
            var oldSysOrg = _sysOrgRpt.FindBy(f => f.DeptName == value.DeptName);
            if(oldSysOrg.Count() > 0)
            {
                return BadRequest(string.Concat(value.DeptName,"已经存在。"));
            }
            value.CreatedAt = DateTime.Now;
            value.UpdatedAt = DateTime.Now;
            _sysOrgRpt.Add(value);
            _sysOrgRpt.Commit();
            return new OkObjectResult(value);
        }
        /// <summary>
        /// 设置用户所属组织
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        // POST api/values
        [HttpPost("{id}/{uid}", Name = "NewUserOrg")]
        public IActionResult NewUserOrg(int id,int uid)
        {
            sys_user sysUser = _sysUserRpt.GetSingle(uid);
            if (sysUser != null)
            {
                sysUser.OrgId = id;
                _sysUserRpt.Commit();
            }
            return new NoContentResult();
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
            sys_org _sysOrg = _sysOrgRpt.GetSingle(id);
            if (_sysOrg == null)
            {
                return new NotFoundResult();
            }
            else
            {
                if(_sysUserRpt.FindBy(f => f.OrgId == _sysOrg.Id).Count() > 0)
                {
                    return BadRequest(string.Concat(_sysOrg.DeptName, "已经关联用户，不能删除。"));
                }
                _sysOrg.IsValid = false;

                _sysOrgRpt.Commit();

                return new NoContentResult();
            }
        }
        /// <summary>
        /// 删除用户组织
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpDelete("{id}/{uid}",Name ="DeleteUserOrg")]
        public IActionResult DeleteUserOrg(int id,int uid)
        {
            sys_user sysUser = _sysUserRpt.GetSingle(uid);
            if (sysUser != null)
            {
                sysUser.OrgId = 0;
                _sysUserRpt.Commit();
            }
            return new NoContentResult();
        }
    }
}
