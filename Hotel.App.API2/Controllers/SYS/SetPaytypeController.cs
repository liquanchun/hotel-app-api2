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
    public class SetPaytypeController : Controller
    {
		private readonly IMapper _mapper;
        private ISetPaytypeRepository _setPaytypeRpt;
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
            return new OkObjectResult(entityDto);
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
        public async Task<IActionResult> Post([FromBody]set_paytype value)
        {
            value.CreatedAt = DateTime.Now;
			value.UpdatedAt = DateTime.Now;
            value.IsValid = true;
            var identity = User.Identity as ClaimsIdentity;
            if(identity != null)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            _setPaytypeRpt.Add(value);
            _setPaytypeRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]set_paytype value)
        {
            var single = _setPaytypeRpt.GetSingle(id);

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
                _setPaytypeRpt.Commit();
            }
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
            else
            {
                single.IsValid = false;
                _setPaytypeRpt.Commit();

                return new NoContentResult();
            }
        }
    }
}
