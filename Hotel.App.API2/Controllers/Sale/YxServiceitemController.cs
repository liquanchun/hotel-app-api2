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
using Hotel.App.Model.Dto;
using Hotel.App.Model.Store;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    public class YxServiceitemController : Controller
    {
		private readonly IMapper _mapper;
        private readonly IYxServiceitemRepository _yxServiceitemRpt;
        private readonly ISysDicRepository _sysDicRpt;
        public YxServiceitemController(IYxServiceitemRepository yxServiceitemRpt, ISysDicRepository sysDicRpt,
                IMapper mapper)
        {
            _yxServiceitemRpt = yxServiceitemRpt;
            _sysDicRpt = sysDicRpt;
            _mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
		    IEnumerable<yx_serviceitem> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _yxServiceitemRpt.FindBy(f => f.IsValid);
			});
            var entity = _mapper.Map<IEnumerable<yx_serviceitem>, IEnumerable<ServiceItemDto>>(entityDto).ToList();
            var dicList = _sysDicRpt.GetAll().ToList();
            foreach (var hs in entity)
            {
                var dic = dicList.FirstOrDefault(f => f.Id == hs.TypeId);
                if (dic != null) hs.TypeName = dic.DicName;
            }
            return new OkObjectResult(entity);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _yxServiceitemRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]yx_serviceitem value)
        {
            value.CreatedAt = DateTime.Now;
			value.UpdatedAt = DateTime.Now;
			value.IsValid = true;
            if(User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            _yxServiceitemRpt.Add(value);
            _yxServiceitemRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]yx_serviceitem value)
        {
            var single = _yxServiceitemRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
            ObjectCopy.Copy(single, value, "name", "typeId", "unit", "itemCode", "integral", "remark", "price");
            //更新字段内容
            single.UpdatedAt = DateTime.Now;
			if(User.Identity is ClaimsIdentity identity)
			{
				single.CreatedBy = identity.Name ?? "test";
			}
            _yxServiceitemRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _yxServiceitemRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }

            single.IsValid = false;
            _yxServiceitemRpt.Commit();

            return new NoContentResult();
        }
    }
}
