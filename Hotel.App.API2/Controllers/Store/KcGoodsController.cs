using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.App.Data.Abstract;
using Hotel.App.Model.Store;
using Hotel.App.API2.Core;
using AutoMapper;
using System.Security.Claims;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    public class KcGoodsController : Controller
    {
		private readonly IMapper _mapper;
        private IKcGoodsRepository _kcGoodsRpt;
        public KcGoodsController(IKcGoodsRepository kcGoodsRpt,
				IMapper mapper)
        {
            _kcGoodsRpt = kcGoodsRpt;
			_mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
		    IEnumerable<kc_goods> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _kcGoodsRpt.FindBy(f => f.IsValid);
			});
            return new OkObjectResult(entityDto);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _kcGoodsRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]kc_goods value)
        {
            value.CreatedAt = DateTime.Now;
			value.UpdatedAt = DateTime.Now;
            value.IsValid = true;
            var identity = User.Identity as ClaimsIdentity;
            if(identity != null)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            _kcGoodsRpt.Add(value);
            _kcGoodsRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]kc_goods value)
        {
            var single = _kcGoodsRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
            else
            {
				//更新字段内容
				single.UpdatedAt = DateTime.Now;
				var identity = User.Identity as ClaimsIdentity;
				if(identity != null)
				{
					value.CreatedBy = identity.Name ?? "test";
				}
                _kcGoodsRpt.Commit();
            }
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _kcGoodsRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }
            else
            {
                single.IsValid = false;
                _kcGoodsRpt.Commit();

                return new NoContentResult();
            }
        }
    }
}
