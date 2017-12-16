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
    public class CwCusaccountController : Controller
    {
		private readonly IMapper _mapper;
        private readonly ISysDicRepository _sysDicRpt;
        private readonly ISetPaytypeRepository _setPaytypeRepository;
        private readonly ICwCusaccountRepository _cwCusaccountRpt;
        public CwCusaccountController(ICwCusaccountRepository cwCusaccountRpt, ISysDicRepository sysDicRpt, ISetPaytypeRepository setPaytypeRepository,
        IMapper mapper)
        {
            _cwCusaccountRpt = cwCusaccountRpt;
            _setPaytypeRepository = setPaytypeRepository;
            _mapper = mapper;
            _sysDicRpt = sysDicRpt;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
		    IEnumerable<cw_cusaccount> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _cwCusaccountRpt.FindBy(f => f.IsValid);
			});
            var entity = _mapper.Map<IEnumerable<cw_cusaccount>, IEnumerable<CWCusAccountDto>>(entityDto).ToList();
            var payTypeList = this._setPaytypeRepository.GetAll();
            foreach (var hs in entity)
            {
                hs.PayTypeTxt = payTypeList.FirstOrDefault(f => f.Id == hs.PayType).Name;
            }
            return new OkObjectResult(entity);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _cwCusaccountRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]cw_cusaccount value)
        {
            value.CreatedAt = DateTime.Now;
			value.IsValid = true;
            if(User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            _cwCusaccountRpt.Add(value);
            _cwCusaccountRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]cw_cusaccount value)
        {
            var single = _cwCusaccountRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
			//更新字段内容
			if(User.Identity is ClaimsIdentity identity)
			{
				single.CreatedBy = identity.Name ?? "test";
			}
            _cwCusaccountRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _cwCusaccountRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }

            single.IsValid = false;
            _cwCusaccountRpt.Commit();

            return new NoContentResult();
        }
    }
}
