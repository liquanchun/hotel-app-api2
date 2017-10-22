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
    public class SetPaytypeController : Controller
    {
		private readonly IMapper _mapper;
        private readonly ISetPaytypeRepository _setPaytypeRpt;
        public SetPaytypeController(ISetPaytypeRepository setPaytypeRpt,
				IMapper mapper)
        {
            _setPaytypeRpt = setPaytypeRpt;
			_mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
		    IEnumerable<set_paytype> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _setPaytypeRpt.FindBy(f => f.IsValid);
			});
            var entity = _mapper.Map<IEnumerable<set_paytype>, IEnumerable<SetPaytypeDto>>(entityDto);

            return new OkObjectResult(entity);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _setPaytypeRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SetPaytypeDto value)
        {
            var entityDto = _mapper.Map<SetPaytypeDto, set_paytype>(value);
            entityDto.CreatedAt = DateTime.Now;
            entityDto.UpdatedAt = DateTime.Now;
            entityDto.IsValid = true;
            if(User.Identity is ClaimsIdentity identity)
            {
                entityDto.CreatedBy = identity.Name ?? "test";
            }
            _setPaytypeRpt.Add(entityDto);
            _setPaytypeRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]SetPaytypeDto value)
        {
            var single = _setPaytypeRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
            var entityDto = _mapper.Map<SetPaytypeDto, set_paytype>(value);
            ObjectCopy.Copy<set_paytype>(single, entityDto,new string[] { "IsReturn", "Name", "PayType", "IsIntegral", "IsDefault", "Remark" });
            //更新字段内容
            single.UpdatedAt = DateTime.Now;
            if(User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            _setPaytypeRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _setPaytypeRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }
            single.IsValid = false;
            _setPaytypeRpt.Commit();

            return new NoContentResult();
        }
    }
}
