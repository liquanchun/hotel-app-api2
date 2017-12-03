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
using Hotel.App.API2.Common;
using Hotel.App.Model.SYS;
using Newtonsoft.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    public class FwHouseinfoController : Controller
    {
		private readonly IMapper _mapper;
        private readonly IFwHouseinfoRepository _fwHouseinfoRpt;
        private readonly ISetHouseTypeRepository _setHouseTypeRpt;
        private readonly ISysDicRepository _sysDicRpt;
        private readonly IFwStatelogRepository _fwStatelogRepository;
        public FwHouseinfoController(IFwHouseinfoRepository fwHouseinfoRpt, 
            ISetHouseTypeRepository setHouseTypeRpt,
            ISysDicRepository sysDicRpt,
            IFwStatelogRepository fwStatelogRepository,
                IMapper mapper)
        {
            _fwHouseinfoRpt = fwHouseinfoRpt;
            _setHouseTypeRpt = setHouseTypeRpt;
            _fwStatelogRepository = fwStatelogRepository;
            _sysDicRpt = sysDicRpt;
            _mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<fw_houseinfo> entityDto = null;
            await Task.Run(() =>
            {
                entityDto = _fwHouseinfoRpt.FindBy(f => f.IsValid);
            });
            var entity = _mapper.Map<IEnumerable<fw_houseinfo>, IEnumerable<HouseinfoDto>>(entityDto).ToList();
            var houseTypeList = _setHouseTypeRpt.GetAll().ToList();
            var dicList = _sysDicRpt.GetAll().ToList();

            foreach (var hs in entity)
            {
                var ht = houseTypeList.FirstOrDefault(f => f.Id == hs.HouseType);
                if (ht != null)
                {
                    hs.HouseTypeTxt = ht.TypeName;
                    hs.HouseFee = ht.AllPrice;
                    hs.PreFee = ht.PreReceiveFee;
                }

                var dic = dicList.FirstOrDefault(f => f.Id == hs.State);
                if (dic != null) hs.StateTxt = dic.DicName;
            }

            return new OkObjectResult(entity.OrderBy(f => f.Code));
        }
        // GET api/values/
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _fwHouseinfoRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]fw_houseinfo value)
        {
            value.CreatedAt = DateTime.Now;
			value.UpdatedAt = DateTime.Now;
            value.State = 1001;  //初始化状态为空净
            value.IsValid = true;
            if(User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            if (_fwHouseinfoRpt.Exist(f => f.Code == value.Code))
            {
                return BadRequest(string.Concat(value.Code, "已经存在。"));
            }
            _fwHouseinfoRpt.Add(value);
            _fwHouseinfoRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]fw_houseinfo value)
        {
            var single = _fwHouseinfoRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
            ObjectCopy.Copy<fw_houseinfo>(single, value, new string[] { "floor", "houseType", "tags", "remark"});
            //更新字段内容
            single.UpdatedAt = DateTime.Now;
            if(User.Identity is ClaimsIdentity identity)
            {
                single.CreatedBy = identity.Name ?? "test";
            }
            _fwHouseinfoRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _fwHouseinfoRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }
            single.IsValid = false;
            _fwHouseinfoRpt.Commit();

            return new NoContentResult();
        }
    }
}
