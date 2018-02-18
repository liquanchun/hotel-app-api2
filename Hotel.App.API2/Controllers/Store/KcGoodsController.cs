using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.App.Data.Abstract;
using Hotel.App.Model.Store;
using AutoMapper;
using System.Security.Claims;
using Hotel.App.API2.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class KcGoodsController : Controller
    {
		private readonly IMapper _mapper;
        private IKcGoodsRepository _kcGoodsRpt;
        private readonly ISysDicRepository _sysDicRpt;
        public KcGoodsController(IKcGoodsRepository kcGoodsRpt, ISysDicRepository sysDicRpt,
        IMapper mapper)
        {
            _kcGoodsRpt = kcGoodsRpt;
            _sysDicRpt = sysDicRpt;
            _mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
		    IEnumerable<kc_goods> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _kcGoodsRpt.FindBy(f => f.IsValid);
			});
            var entity = _mapper.Map<IEnumerable<kc_goods>, IEnumerable<GoodsDto>>(entityDto).ToList();
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
            var single = _kcGoodsRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]kc_goods value)
        {
            value.CreatedAt = DateTime.Now;
			value.UpdatedAt = DateTime.Now;
            value.IsValid = true;
            if(User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            _kcGoodsRpt.Add(value);
            _kcGoodsRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]kc_goods value)
        {
            var single = _kcGoodsRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
            ObjectCopy.Copy<kc_goods>(single, value, new string[] { "name","typeId", "unit", "goodsCode", "minAmount", "remark", "goodsNo", "price" });
            //更新字段内容
            single.UpdatedAt = DateTime.Now;
            if(User.Identity is ClaimsIdentity identity)
            {
                single.CreatedBy = identity.Name ?? "test";
            }
            _kcGoodsRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _kcGoodsRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }
            single.IsValid = false;
            _kcGoodsRpt.Commit();

            return new NoContentResult();
        }
    }
}
