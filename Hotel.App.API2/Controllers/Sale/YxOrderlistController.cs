using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.App.Data.Abstract;
using Hotel.App.Model.Sale;
using Hotel.App.API2.Core;
using AutoMapper;
using System.Security.Claims;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    public class YxOrderlistController : Controller
    {
		private readonly IMapper _mapper;
        private readonly IYxOrderlistRepository _yxOrderlistRpt;
        public YxOrderlistController(IYxOrderlistRepository yxOrderlistRpt,
				IMapper mapper)
        {
            _yxOrderlistRpt = yxOrderlistRpt;
			_mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
		    IEnumerable<yx_orderlist> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _yxOrderlistRpt.FindBy(f => f.IsValid);
			});
            return new OkObjectResult(entityDto);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _yxOrderlistRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]yx_orderlist value)
        {
            value.CreatedAt = DateTime.Now;
			value.UpdatedAt = DateTime.Now;
			value.IsValid = true;
            if(User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            _yxOrderlistRpt.Add(value);
            _yxOrderlistRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]yx_orderlist value)
        {
            var single = _yxOrderlistRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
			//更新字段内容
			single.UpdatedAt = DateTime.Now;
			if(User.Identity is ClaimsIdentity identity)
			{
				value.CreatedBy = identity.Name ?? "test";
			}
            _yxOrderlistRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _yxOrderlistRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }

            single.IsValid = false;
            _yxOrderlistRpt.Commit();

            return new NoContentResult();
        }
    }
}
