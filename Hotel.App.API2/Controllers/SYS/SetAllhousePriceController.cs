using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.App.Data.Abstract;
using Hotel.App.Model.SYS;
using Hotel.App.API2.Core;
using AutoMapper;
using System.Security.Claims;
using Hotel.App.API2.Common;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    public class SetAllhousePriceController : Controller
    {
		private readonly IMapper _mapper;
        private readonly ISetAllhousePriceRepository _setAllhousePriceRpt;
        private readonly ISetHouseTypeRepository _setHouseTypeRpt;
        public SetAllhousePriceController(ISetAllhousePriceRepository setAllhousePriceRpt, ISetHouseTypeRepository setHouseTypeRpt,
                IMapper mapper)
        {
            _setAllhousePriceRpt = setAllhousePriceRpt;
            _setHouseTypeRpt = setHouseTypeRpt;
            _mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
		    IEnumerable<set_allhouse_price> entityDto = null;
            await Task.Run(() =>
            {
                try
                {
                    entityDto = _setAllhousePriceRpt.FindBy(f => f.IsValid);
                    var entList = entityDto.ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
			});
            return new OkObjectResult(entityDto);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _setAllhousePriceRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]set_allhouse_price value)
        {
            value.CreatedAt = DateTime.Now;
            value.UpdatedAt = DateTime.Now;
            value.IsValid = true;
            if(User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            _setAllhousePriceRpt.Add(value);
            _setAllhousePriceRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]set_allhouse_price value)
        {
            var single = _setAllhousePriceRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
            ObjectCopy.Copy<set_allhouse_price>(single, value, new string[] { "name", "halfPriceHours", "allPriceHours", "leaveTime", "addFeeHours", "addAllDay", "addAllHours","remark" });
            //更新字段内容
            single.UpdatedAt = DateTime.Now;
            if(User.Identity is ClaimsIdentity identity)
            {
                single.CreatedBy = identity.Name ?? "test";
            }
            _setAllhousePriceRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _setAllhousePriceRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }
            single.IsValid = false;
            _setAllhousePriceRpt.Commit();

            return new NoContentResult();
        }
    }
}
