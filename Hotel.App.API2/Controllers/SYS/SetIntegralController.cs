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
    public class SetIntegralController : Controller
    {
		private readonly IMapper _mapper;
        private readonly ISetIntegralRepository _setIntegralRpt;
        private readonly ISetCardRepository _setCardRpt;
        private readonly ISetHouseTypeRepository _setHouseTypeRepository;
        private readonly IYxServiceitemRepository _yxServiceitemRepository;
        private readonly IKcGoodsRepository _kcGoodsRepository;
        public SetIntegralController(ISetIntegralRepository setIntegralRpt, ISetCardRepository setCardRpt,
            ISetHouseTypeRepository setHouseTypeRepository,
            IYxServiceitemRepository yxServiceitemRepository,
            IKcGoodsRepository kcGoodsRepository,
        IMapper mapper)
        {
            _setIntegralRpt = setIntegralRpt;
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
		    IEnumerable<set_integral> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _setIntegralRpt.FindBy(f => f.IsValid);
			});
            var entity = _mapper.Map<IEnumerable<set_integral>, IEnumerable<SetCardIntegralDto>>(entityDto).ToList();
            var cardTypeList = _setCardRpt.GetAll().ToList();
            var houstTypeList = _setHouseTypeRepository.GetAll().ToList();
            var serviceItemList = _yxServiceitemRepository.GetAll().ToList();
            var goodsList = _kcGoodsRepository.GetAll().ToList();
            foreach (var hs in entity)
            {
                hs.CardTypeTxt = cardTypeList.FirstOrDefault(f => f.Id == hs.CardTypeId)?.Name;
                hs.HouseTypeTxt = houstTypeList.FirstOrDefault(f => f.Id == hs.HouseTypeId)?.TypeName;
                hs.ServiceItemTxt = serviceItemList.FirstOrDefault(f => f.Id == hs.ServiceItemId)?.Name;
                hs.GoodsTxt = goodsList.FirstOrDefault(f => f.Id == hs.GoodsId)?.Name;
            }
            return new OkObjectResult(entity);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _setIntegralRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]set_integral value)
        {
            value.CreatedAt = DateTime.Now;
            value.UpdatedAt = DateTime.Now;
            value.IsValid = true;
            if (User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            //if (!string.IsNullOrEmpty(value.HouseTypeTxt))
            //{
            //    var ht = value.HouseTypeTxt.Split(',');
            //    foreach (var h in ht)
            //    {
            //        integral.HouseTypeId = int.Parse(h);
            //        integral.Id = -1;
            //        _setIntegralRpt.Add(integral);
            //    }
            //}
            //if (!string.IsNullOrEmpty(value.ServiceItemTxt))
            //{
            //    var ht = value.ServiceItemTxt.Split(',');
            //    foreach (var h in ht)
            //    {
            //        integral.ServiceItemId = int.Parse(h);
            //        integral.Id = -1;
            //        _setIntegralRpt.Add(integral);
            //    }
            //}
            _setIntegralRpt.Add(value);
            _setIntegralRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]set_integral value)
        {
            var single = _setIntegralRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
            //更新字段内容
            single.UpdatedAt = DateTime.Now;
            single.CardTypeId = value.CardTypeId;
            single.HouseTypeId = value.HouseTypeId;
            single.EndDate = value.EndDate;
            single.Integral = value.Integral;
            single.InteType = value.InteType;
            single.Name = value.Name;
            single.Remark = value.Remark;
            single.StartDate = value.StartDate;
            if(User.Identity is ClaimsIdentity identity)
            {
                single.CreatedBy = identity.Name ?? "test";
            }
            _setIntegralRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _setIntegralRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }
            single.IsValid = false;
            _setIntegralRpt.Commit();

            return new NoContentResult();
        }
    }
}
