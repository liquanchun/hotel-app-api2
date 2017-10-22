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
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    public class SetHouseTypeController : Controller
    {
		private readonly IMapper _mapper;
        private readonly ISetHouseTypeRepository _setHouseTypeRpt;
        public SetHouseTypeController(ISetHouseTypeRepository setHouseTypeRpt,
				IMapper mapper)
        {
            _setHouseTypeRpt = setHouseTypeRpt;
			_mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
		    IEnumerable<set_house_type> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _setHouseTypeRpt.FindBy(f => f.IsValid);
			});
            return new OkObjectResult(entityDto);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _setHouseTypeRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]set_house_type value)
        {
            value.CreatedAt = DateTime.Now;
            value.UpdatedAt = DateTime.Now;
            value.IsValid = true;
            if(User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            _setHouseTypeRpt.Add(value);
            _setHouseTypeRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]set_house_type value)
        {
            var single = _setHouseTypeRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
            //更新字段内容
            single.UpdatedAt = DateTime.Now;
            single.AddMaxPrice = value.AddMaxPrice;
            single.AddPrice = value.AddPrice;
            single.AllPrice = value.AllPrice;
            single.PreReceiveFee = value.PreReceiveFee;
            single.Remark = value.Remark;
            single.StartPrice = value.StartPrice;
            single.TypeName = value.TypeName;
            if(User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            _setHouseTypeRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _setHouseTypeRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }
            single.IsValid = false;
            _setHouseTypeRpt.Commit();

            return new NoContentResult();
        }
    }
}
