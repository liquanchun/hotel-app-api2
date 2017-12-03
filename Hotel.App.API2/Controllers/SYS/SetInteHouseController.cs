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
    public class SetInteHouseController : Controller
    {
		private readonly IMapper _mapper;
        private readonly ISetInteHouseRepository _setInteHouseRpt;
        private readonly ISetCardRepository _setCardRpt;
        private readonly ISetHouseTypeRepository _setHouseTypeRpt;
        public SetInteHouseController(ISetInteHouseRepository setInteHouseRpt, ISetCardRepository setCardRpt,
                ISetHouseTypeRepository setHouseTypeRpt,
                IMapper mapper)
        {
            _setInteHouseRpt = setInteHouseRpt;
            _setCardRpt = setCardRpt;
            _setHouseTypeRpt = setHouseTypeRpt;
            _mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
		    IEnumerable<set_inte_house> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _setInteHouseRpt.FindBy(f => f.IsValid);
			});
            var entity = _mapper.Map<IEnumerable<set_inte_house>, IEnumerable<SetInteHouseDto>>(entityDto).ToList();
            try
            {
                var houseTypeList = _setHouseTypeRpt.GetAll().ToList();
                foreach (var hs in entity)
                {
                    hs.HouseTypeTxt = houseTypeList.FirstOrDefault(f => f.Id == hs.HouseType)?.TypeName;
                }
                var cardTypeList = _setCardRpt.GetAll().ToList();
                foreach (var hs in entity)
                {
                    hs.CardTypeTxt = cardTypeList.FirstOrDefault(f => f.Id == hs.CardType)?.Name;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new OkObjectResult(entity);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _setInteHouseRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]set_inte_house value)
        {
            value.CreatedAt = DateTime.Now;
            value.UpdatedAt = DateTime.Now;
            value.IsValid = true;
            if(User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            _setInteHouseRpt.Add(value);
            _setInteHouseRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]set_inte_house value)
        {
            var single = _setInteHouseRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
            //更新字段内容
            single.UpdatedAt = DateTime.Now;
            single.CardType = value.CardType;
            single.EndDate = value.EndDate;
            single.HouseType = value.HouseType;
            single.Name = value.Name;
            single.Remark = value.Remark;
            single.StartDate = value.StartDate;
            single.TakeInte = value.TakeInte;
            single.UseWeeks = value.UseWeeks;
            if(User.Identity is ClaimsIdentity identity)
            {
                single.CreatedBy = identity.Name ?? "test";
            }
            _setInteHouseRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _setInteHouseRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }
            single.IsValid = false;
            _setInteHouseRpt.Commit();

            return new NoContentResult();
        }
    }
}
