using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.App.Data.Abstract;
using Hotel.App.Model.House;
using Hotel.App.API2.Core;
using AutoMapper;
using System.Security.Claims;
using Hotel.App.API2.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    public class FwCusgoodsController : Controller
    {
		private readonly IMapper _mapper;
        private readonly IFwCusgoodsRepository _fwCusgoodsRpt;
        public FwCusgoodsController(IFwCusgoodsRepository fwCusgoodsRpt,
				IMapper mapper)
        {
            _fwCusgoodsRpt = fwCusgoodsRpt;
			_mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
		    IEnumerable<fw_cusgoods> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _fwCusgoodsRpt.FindBy(f => f.IsValid);
			});
            return new OkObjectResult(entityDto);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _fwCusgoodsRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]fw_cusgoods value)
        {
            value.CreatedAt = DateTime.Now;
            value.UpdatedAt = DateTime.Now;
            value.IsValid = true;
            if(User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            _fwCusgoodsRpt.Add(value);
            _fwCusgoodsRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]fw_cusgoods value)
        {
            var single = _fwCusgoodsRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
            ObjectCopy.Copy<fw_cusgoods>(single, value, new string[] { "TypeName", "GoodsName", "GoodsPrice", "HouseCode", "OrderNo", "CusName", "Mobile", "Remark" });
            single.UpdatedAt = DateTime.Now;
            //更新字段内容
            if (User.Identity is ClaimsIdentity identity)
            {
                if (!string.IsNullOrEmpty(value.TakeBy))
                {
                    single.TakeBy = identity.Name ?? "test";
                    single.TakeTime = DateTime.Now;
                }
                else
                {
                    single.CreatedBy = identity.Name ?? "test";
                }
            }
            _fwCusgoodsRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _fwCusgoodsRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }
            single.IsValid = false;
            _fwCusgoodsRpt.Commit();

            return new NoContentResult();
        }
    }
}
