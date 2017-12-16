using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.App.Data.Abstract;
using Hotel.App.Model.Account;
using Hotel.App.API2.Core;
using AutoMapper;
using System.Security.Claims;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    public class CwPreauthController : Controller
    {
		private readonly IMapper _mapper;
        private readonly ICwPreauthRepository _cwPreauthRpt;
        public CwPreauthController(ICwPreauthRepository cwPreauthRpt,
				IMapper mapper)
        {
            _cwPreauthRpt = cwPreauthRpt;
			_mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
		    IEnumerable<cw_preauth> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _cwPreauthRpt.FindBy(f => f.IsValid);
			});
            return new OkObjectResult(entityDto);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _cwPreauthRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]cw_preauth value)
        {
            value.CreatedAt = DateTime.Now;
			value.IsValid = true;
            if(User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            _cwPreauthRpt.Add(value);
            _cwPreauthRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]cw_preauth value)
        {
            var single = _cwPreauthRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
			//更新字段内容
			if(User.Identity is ClaimsIdentity identity)
			{
				single.CreatedBy = identity.Name ?? "test";
			}
            _cwPreauthRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _cwPreauthRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }

            single.IsValid = false;
            _cwPreauthRpt.Commit();

            return new NoContentResult();
        }
    }
}
