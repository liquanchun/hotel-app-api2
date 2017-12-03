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
    public class SetCardController : Controller
    {
		private readonly IMapper _mapper;
        private readonly ISetCardRepository _setCardRpt;
        public SetCardController(ISetCardRepository setCardRpt,
				IMapper mapper)
        {
            _setCardRpt = setCardRpt;
			_mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
		    IEnumerable<set_card> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _setCardRpt.FindBy(f => f.IsValid);
			});
            return new OkObjectResult(entityDto);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _setCardRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SetCardDto value)
        {
           var entityDto = _mapper.Map<SetCardDto, set_card>(value);
            entityDto.CreatedAt = DateTime.Now;
            entityDto.UpdatedAt = DateTime.Now;
            entityDto.IsValid = true;
            entityDto.IsRecharge = value.InCome == "是";
            if(User.Identity is ClaimsIdentity identity)
            {
                entityDto.CreatedBy = identity.Name ?? "test";
            }
            _setCardRpt.Add(entityDto);
            _setCardRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]SetCardDto value)
        {
            var single = _setCardRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
            //更新字段内容
            single.UpdatedAt = DateTime.Now;
            single.IsRecharge = value.InCome == "是";
            single.Level = value.Level;
            single.Name = value.Name;
            single.Remark = value.Remark;
            single.CardFee = value.CardFee;
            if(User.Identity is ClaimsIdentity identity)
            {
                single.CreatedBy = identity.Name ?? "test";
            }
            _setCardRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _setCardRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }
            single.IsValid = false;
            _setCardRpt.Commit();

            return new NoContentResult();
        }
    }
}
