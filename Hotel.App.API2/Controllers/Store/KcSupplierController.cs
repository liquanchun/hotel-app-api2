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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Hotel.App.API2.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class KcSupplierController : Controller
    {
		private readonly IMapper _mapper;
        private readonly IKcSupplierRepository _kcSupplierRpt;
        public KcSupplierController(IKcSupplierRepository kcSupplierRpt,
				IMapper mapper)
        {
            _kcSupplierRpt = kcSupplierRpt;
			_mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<kc_supplier> entityDto = null;
            await Task.Run(() =>
            {
                entityDto = _kcSupplierRpt.FindBy(f => f.IsValid);
            });
            return new OkObjectResult(entityDto.ToList().OrderBy(f => f.Name));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _kcSupplierRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]kc_supplier value)
        {
            value.CreatedAt = DateTime.Now;
			value.UpdatedAt = DateTime.Now;
            value.IsValid = true;
            if(User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            _kcSupplierRpt.Add(value);
            _kcSupplierRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]kc_supplier value)
        {
            var single = _kcSupplierRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
            ObjectCopy.Copy<kc_supplier>(single, value, new string[] { "name", "address", "linkMan", "tel", "linkManTitle", "linkManTel", "faxNo", "remark", "city" });
            //更新字段内容
            single.UpdatedAt = DateTime.Now;
            if(User.Identity is ClaimsIdentity identity)
            {
                single.CreatedBy = identity.Name ?? "test";
            }
            _kcSupplierRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _kcSupplierRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }
            single.IsValid = false;
            _kcSupplierRpt.Commit();

            return new NoContentResult();
        }
    }
}
