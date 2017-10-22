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
    public class SysFunctionController : Controller
    {
        private readonly ISysFunctionRepository _sysFunctionRpt;
        private readonly ISysRoleFunctionRepository _sysRoleFunctionRpt;
        public SysFunctionController(ISysFunctionRepository sysFunctionRpt,ISysRoleFunctionRepository sysRoleFunctionRpt)
        {
            _sysFunctionRpt = sysFunctionRpt;
            _sysRoleFunctionRpt = sysRoleFunctionRpt;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult(_sysFunctionRpt.GetAll().ToList());
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]sys_function value)
        {
            var oldSysFunction = _sysFunctionRpt.FindBy(f => f.FunctionName == value.FunctionName);
            if(oldSysFunction.Any())
            {
                return BadRequest(string.Concat(value.FunctionName, "已经存在。"));
            }
            value.CreatedAt = DateTime.Now;
            value.UpdatedAt = DateTime.Now;
            _sysFunctionRpt.Add(value);
            _sysFunctionRpt.Commit();
            return new OkObjectResult(value);
        }

        /// <summary>
        /// 设置用户所属组织
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fid"></param>
        /// <returns></returns>
        // POST api/values
        [HttpPost("{id}/{fid}", Name = "NewRoleFunction")]
        public IActionResult NewRoleFunction(int id,int fid)
        {
            _sysRoleFunctionRpt.Add(new sys_role_function { FunctionId= id, RoleId = fid });
            _sysRoleFunctionRpt.Commit();
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
            sys_function sysFunction = _sysFunctionRpt.GetSingle(id);
            if (sysFunction == null)
            {
                return new NotFoundResult();
            }
            _sysRoleFunctionRpt.DeleteWhere(f => f.FunctionId == id);
            _sysRoleFunctionRpt.Commit();
            _sysFunctionRpt.Delete(sysFunction);
            _sysFunctionRpt.Commit();

            return new NoContentResult();
        }

        /// <summary>
        /// 删除用户组织
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fid"></param>
        /// <returns></returns>
        [HttpDelete("{id}/{fid}",Name ="DeleteRoleFunction")]
        public IActionResult DeleteRoleFunction(int id,int fid)
        {
            _sysRoleFunctionRpt.DeleteWhere(f => f.FunctionId == id && f.RoleId == fid);
            _sysRoleFunctionRpt.Commit();
            return new NoContentResult();
        }
    }
}
