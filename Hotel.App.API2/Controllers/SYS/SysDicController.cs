using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.App.Data.Abstract;
using Hotel.App.Model.SYS;
using Hotel.App.API2.Core;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    public class SysDicController : Controller
    {
        private ISysDicRepository _sysDicRpt;
        public SysDicController(ISysDicRepository sysDicRpt)
        {
            _sysDicRpt = sysDicRpt;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult(_sysDicRpt.FindBy(f => f.IsValid).ToList());
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]sys_dic value)
        {
            var oldSysDic = _sysDicRpt.FindBy(f => f.DicName == value.DicName);
            if(oldSysDic.Count() > 0)
            {
                return BadRequest(string.Concat(value.DicName, "已经存在。"));
            }
            value.CreatedAt = DateTime.Now;
            _sysDicRpt.Add(value);
            _sysDicRpt.Commit();
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
            sys_dic _sysDic = _sysDicRpt.GetSingle(id);
            if (_sysDic == null)
            {
                return new NotFoundResult();
            }
            else
            {
                _sysDic.IsValid = false;
                _sysDicRpt.Commit();

                return new NoContentResult();
            }
        }
    }
}
