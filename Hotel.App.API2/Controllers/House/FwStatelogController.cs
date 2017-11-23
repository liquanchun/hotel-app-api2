using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.App.Data.Abstract;
using Hotel.App.Model.House;
using Hotel.App.API2.Core;
using AutoMapper;
using System.Security.Claims;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    public class FwStatelogController : Controller
    {
		private readonly IMapper _mapper;
        private readonly IFwStatelogRepository _fwStatelogRpt;
        private readonly ISysDicRepository _sysDicRpt;
        public FwStatelogController(IFwStatelogRepository fwStatelogRpt,
            ISysDicRepository sysDicRpt,
                IMapper mapper)
        {
            _fwStatelogRpt = fwStatelogRpt;
            _sysDicRpt = sysDicRpt;
            _mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
		    IEnumerable<fw_statelog> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _fwStatelogRpt.FindBy(f => f.IsValid);
			});
            var entity = _mapper.Map<IEnumerable<fw_statelog>, IEnumerable<HouseStateLogDto>>(entityDto).ToList();
            var dicList = _sysDicRpt.GetAll().ToList();
            foreach (var hs in entity)
            {
                var dic1 = dicList.FirstOrDefault(f => f.Id == hs.OldState);
                if (dic1 != null) hs.OldStateTxt = dic1.DicName;

                var dic2 = dicList.FirstOrDefault(f => f.Id == hs.NewState);
                if (dic2 != null) hs.NewStateTxt = dic2.DicName;
            }
            return new OkObjectResult(entity.OrderByDescending(f=> f.CreatedAt));
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _fwStatelogRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]fw_statelog value)
        {
            value.CreatedAt = DateTime.Now;
			value.UpdatedAt = DateTime.Now;
			value.IsValid = true;
            if(User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            _fwStatelogRpt.Add(value);
            _fwStatelogRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]fw_statelog value)
        {
            var single = _fwStatelogRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
			//更新字段内容
			single.UpdatedAt = DateTime.Now;
			if(User.Identity is ClaimsIdentity identity)
			{
				value.CreatedBy = identity.Name ?? "test";
			}
            _fwStatelogRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _fwStatelogRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }

            single.IsValid = false;
            _fwStatelogRpt.Commit();

            return new NoContentResult();
        }
    }
}
