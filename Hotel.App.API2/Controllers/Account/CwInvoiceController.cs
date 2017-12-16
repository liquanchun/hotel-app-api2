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
    public class CwInvoiceController : Controller
    {
		private readonly IMapper _mapper;
        private readonly ICwInvoiceRepository _cwInvoiceRpt;
        public CwInvoiceController(ICwInvoiceRepository cwInvoiceRpt,
				IMapper mapper)
        {
            _cwInvoiceRpt = cwInvoiceRpt;
			_mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
		    IEnumerable<cw_invoice> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _cwInvoiceRpt.FindBy(f => f.IsValid);
			});
            return new OkObjectResult(entityDto);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _cwInvoiceRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]cw_invoice value)
        {
            value.CreatedAt = DateTime.Now;
			value.IsValid = true;
            if(User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            _cwInvoiceRpt.Add(value);
            _cwInvoiceRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]cw_invoice value)
        {
            var single = _cwInvoiceRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
			//更新字段内容
			if(User.Identity is ClaimsIdentity identity)
			{
				single.CreatedBy = identity.Name ?? "test";
			}
            _cwInvoiceRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _cwInvoiceRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }

            single.IsValid = false;
            _cwInvoiceRpt.Commit();

            return new NoContentResult();
        }
    }
}
