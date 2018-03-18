using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.App.Data.Abstract;
using Hotel.App.Model.SYS;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Hotel.App.API2.Core;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SysOrgController : Controller
    {
        private readonly ISysOrgRepository _sysOrgRpt;
        private readonly ISysStaffRepository _sysStaffRpt;
        public SysOrgController(ISysOrgRepository sysOrgRpt,ISysStaffRepository sysStaffRpt)
        {
            _sysOrgRpt = sysOrgRpt;
            _sysStaffRpt = sysStaffRpt;
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
        [HttpGet("{id}/staff",Name ="GetStaffList")]
        public IActionResult GetStaffList(int id)
        {
            return new OkObjectResult(_sysStaffRpt.FindBy(f => f.IsValid && f.OrgId == id).ToList());
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
            if(oldSysOrg.Any())
            {
                return BadRequest(string.Concat(value.DeptName,"已经存在。"));
            }
            value.CreatedAt = DateTime.Now;
            value.UpdatedAt = DateTime.Now;
            if (User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            value.IsValid = true;
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
        [HttpPost("{id}/{empno}", Name = "NewStaffOrg")]
        public IActionResult NewStaffOrg(int id,string empno)
        {
            sys_staff sysStaff = _sysStaffRpt.GetSingle(f => f.EmployeeNo == empno);
            if (sysStaff != null)
            {
                sysStaff.OrgId = id;
            }
            _sysStaffRpt.Commit();
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
            sys_org sysOrg = _sysOrgRpt.GetSingle(id);
            if (sysOrg == null)
            {
                return new NotFoundResult();
            }
            if(_sysStaffRpt.FindBy(f => f.OrgId == sysOrg.Id).Any())
            {
                return BadRequest(string.Concat(sysOrg.DeptName, "已经关联用户，不能删除。"));
            }
            sysOrg.IsValid = false;

            _sysOrgRpt.Commit();

            return new NoContentResult();
        }
        /// <summary>
        /// 删除用户组织
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpDelete("{id}/{empno}", Name ="DeleteStaffOrg")]
        public IActionResult DeleteStaffOrg(int id,string empno)
        {
            sys_staff sysStaff = _sysStaffRpt.GetSingle(f => f.EmployeeNo == empno);
            if (sysStaff != null)
            {
                sysStaff.OrgId = 0;
                _sysStaffRpt.Commit();
            }
            return new NoContentResult();
        }
    }
}
