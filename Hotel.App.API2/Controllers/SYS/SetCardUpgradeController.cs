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
    public class SetCardUpgradeController : Controller
    {
		private readonly IMapper _mapper;
        private readonly ISetCardUpgradeRepository _setCardUpgradeRpt;
        private readonly ISetCardRepository _setCardRpt;
        public SetCardUpgradeController(ISetCardUpgradeRepository setCardUpgradeRpt, ISetCardRepository setCardRpt,
                IMapper mapper)
        {
            _setCardUpgradeRpt = setCardUpgradeRpt;
            _setCardRpt = setCardRpt;
            _mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
		    IEnumerable<set_card_upgrade> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _setCardUpgradeRpt.FindBy(f => f.IsValid);
			});
            var entity = _mapper.Map<IEnumerable<set_card_upgrade>, IEnumerable<SetCardUpgradeDto>>(entityDto).ToList();
            var cardTypeList = _setCardRpt.GetAll().ToList();
            foreach (var hs in entity)
            {
                hs.OldCardTxt = cardTypeList.FirstOrDefault(f => f.Id == hs.OldCard)?.Name;
                hs.NewCardTxt = cardTypeList.FirstOrDefault(f => f.Id == hs.NewCard)?.Name;
            }

            return new OkObjectResult(entity);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _setCardUpgradeRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]set_card_upgrade value)
        {
            value.CreatedAt = DateTime.Now;
            value.UpdatedAt = DateTime.Now;
            value.IsValid = true;
            if(User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            _setCardUpgradeRpt.Add(value);
            _setCardUpgradeRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]set_card_upgrade value)
        {
            var single = _setCardUpgradeRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
            //更新字段内容
            single.UpdatedAt = DateTime.Now;
            single.NeedInte = value.NeedInte;
            single.NewCard = value.NewCard;
            single.OldCard = value.OldCard;
            single.Remark = value.Remark;
            single.TakeInte = value.TakeInte;
            if(User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            _setCardUpgradeRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _setCardUpgradeRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }
            single.IsValid = false;
            _setCardUpgradeRpt.Commit();

            return new NoContentResult();
        }
    }
}
