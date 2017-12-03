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
using Hotel.App.API2.Common;
using Hotel.App.Model.House;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    public class YxCustomerController : Controller
    {
		private readonly IMapper _mapper;
        private readonly IYxOrderRepository _yxOrderRpt;
        private readonly IYxCustomerRepository _yxCustomerRpt;
        public YxCustomerController(IYxCustomerRepository yxCustomerRpt, IYxOrderRepository yxOrderRpt,

                IMapper mapper)
        {
            _yxCustomerRpt = yxCustomerRpt;
            _yxOrderRpt = yxOrderRpt;
            _mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
		    IEnumerable<yx_customer> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _yxCustomerRpt.FindBy(f => f.IsValid);
			});
            var entity = _mapper.Map<IEnumerable<yx_customer>, IEnumerable<CustomerDto>>(entityDto).ToList();
            foreach (var cust in entity)
            {
                cust.ComeTimes = this._yxOrderRpt.FindBy(f => f.IdCard == cust.IDCardNo && cust.IsValid).Count();
                cust.ConsumeFee = this._yxOrderRpt.FindBy(f => f.IdCard == cust.IDCardNo && cust.IsValid).ToList().Sum(f => f.HouseFee);
                cust.LastTime = this._yxOrderRpt.FindBy(f => f.IdCard == cust.IDCardNo && cust.IsValid)
                    .Max(f => f.CreatedAt);
            }
            return new OkObjectResult(entity);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _yxCustomerRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]yx_customer value)
        {
            value.CreatedAt = DateTime.Now;
			value.UpdatedAt = DateTime.Now;
			value.IsValid = true;
            if(User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            _yxCustomerRpt.Add(value);
            _yxCustomerRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]yx_customer value)
        {
            var single = _yxCustomerRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
            ObjectCopy.Copy<yx_customer>(single, value, new string[] { "customerName", "sex", "mobile", "address", "birthday", "remark"});
            //更新字段内容
            single.UpdatedAt = DateTime.Now;
			if(User.Identity is ClaimsIdentity identity)
			{
				single.CreatedBy = identity.Name ?? "test";
			}
            _yxCustomerRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _yxCustomerRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }

            single.IsValid = false;
            _yxCustomerRpt.Commit();

            return new NoContentResult();
        }
    }
}
