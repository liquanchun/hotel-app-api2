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
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    public class YxOrderserviceController : Controller
    {
		private readonly IMapper _mapper;
        private readonly IYxOrderserviceRepository _yxOrderserviceRpt;
        private readonly IYxServiceitemRepository _yxServiceitemRpt;
        private readonly ISysDicRepository _sysDicRpt;
        public YxOrderserviceController(IYxOrderserviceRepository yxOrderserviceRpt,
            IYxServiceitemRepository yxServiceitemRpt,
            ISysDicRepository sysDicRpt,
                IMapper mapper)
        {
            _yxOrderserviceRpt = yxOrderserviceRpt;
            _yxServiceitemRpt = yxServiceitemRpt;
            _sysDicRpt = sysDicRpt;
            _mapper = mapper;
        }
        // GET: api/values
        [HttpGet("{orderno}")]
        public async Task<IActionResult> Get(string orderNo)
        {
		    IEnumerable<yx_orderservice> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _yxOrderserviceRpt.FindBy(f => f.IsValid && f.OrderNo == orderNo);
			});
            var entity = _mapper.Map<IEnumerable<yx_orderservice>, IEnumerable<OrderServiceDto>>(entityDto).ToList();
            var dicList = _sysDicRpt.GetAll().ToList();
            foreach (var hs in entity)
            {
                var dic = dicList.FirstOrDefault(f => f.Id == hs.ServiceType);
                if (dic != null) hs.ServiceTypeTxt = dic.DicName;
            }

            var serList = _yxServiceitemRpt.GetAll().ToList();
            foreach (var hs in entity)
            {
                var dic = serList.FirstOrDefault(f => f.ItemCode == hs.ServiceCode);
                if (dic != null) hs.ServiceName = dic.Name;
            }
            return new OkObjectResult(entity);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _yxOrderserviceRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]yx_orderservice value)
        {
            value.CreatedAt = DateTime.Now;
			value.UpdatedAt = DateTime.Now;
			value.IsValid = true;
            if(User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            _yxOrderserviceRpt.Add(value);
            _yxOrderserviceRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]yx_orderservice value)
        {
            var single = _yxOrderserviceRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
            ObjectCopy.Copy(single, value, "OrderNo", "ServiceType", "ServiceCode", "ServiceTime", "Times", "Price", "remark","Operator");
            //更新字段内容
            single.UpdatedAt = DateTime.Now;
			if(User.Identity is ClaimsIdentity identity)
			{
				single.CreatedBy = identity.Name ?? "test";
			}
            _yxOrderserviceRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _yxOrderserviceRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }

            single.IsValid = false;
            _yxOrderserviceRpt.Commit();

            return new NoContentResult();
        }
    }
}
