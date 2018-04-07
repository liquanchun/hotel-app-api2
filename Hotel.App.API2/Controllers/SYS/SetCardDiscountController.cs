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
    public class SetCardDiscountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISetCardDiscountRepository _setCardDiscountRpt;
        private readonly ISetCardRepository _setCardRpt;
        private readonly ISetHouseTypeRepository _setHouseTypeRepository;
        private readonly IYxServiceitemRepository _yxServiceitemRepository;
        private readonly IKcGoodsRepository _kcGoodsRepository;
        public SetCardDiscountController(ISetCardDiscountRepository setDiscountRpt, ISetCardRepository setCardRpt,
            ISetHouseTypeRepository setHouseTypeRepository,
            IYxServiceitemRepository yxServiceitemRepository,
            IKcGoodsRepository kcGoodsRepository,
            IMapper mapper)
        {
            _setCardDiscountRpt = setDiscountRpt;
            _setCardRpt = setCardRpt;
            _mapper = mapper;
            _setHouseTypeRepository = setHouseTypeRepository;
            _yxServiceitemRepository = yxServiceitemRepository;
            _kcGoodsRepository = kcGoodsRepository;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
		    IEnumerable<set_card_discount> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _setCardDiscountRpt.FindBy(f => f.IsValid);
			});

            var entity = _mapper.Map<IEnumerable<set_card_discount>, IEnumerable<SetCardDiscountDto>>(entityDto).ToList();
            var cardTypeList = _setCardRpt.GetAll().ToList();
            var houstTypeList = _setHouseTypeRepository.GetAll().ToList();
            var serviceItemList = _yxServiceitemRepository.GetAll().ToList();
            var goodsList = _kcGoodsRepository.GetAll().ToList();
            foreach (var hs in entity)
            {
                hs.CardTypeTxt = cardTypeList.FirstOrDefault(f => f.Id == hs.CardTypeId)?.Name;
                hs.HouseTypeTxt = houstTypeList.FirstOrDefault(f => f.Id == hs.HouseTypeId)?.TypeName;
                hs.ServiceItemTxt = serviceItemList.FirstOrDefault(f => f.Id == hs.ServiceItemId)?.Name;
            }
            return new OkObjectResult(entity);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _setCardDiscountRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]set_card_discount value)
        {
            value.CreatedAt = DateTime.Now;
			value.UpdatedAt = DateTime.Now;
			value.IsValid = true;
            if(User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            _setCardDiscountRpt.Add(value);
            _setCardDiscountRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]set_card_discount value)
        {
            var single = _setCardDiscountRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
			//更新字段内容
			single.UpdatedAt = DateTime.Now;
			if(User.Identity is ClaimsIdentity identity)
			{
				single.CreatedBy = identity.Name ?? "test";
			}
            _setCardDiscountRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _setCardDiscountRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }

            single.IsValid = false;
            _setCardDiscountRpt.Commit();

            return new NoContentResult();
        }
    }
}
