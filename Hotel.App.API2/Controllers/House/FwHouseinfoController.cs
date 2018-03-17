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
using Hotel.App.Data;
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
        private readonly HotelAppContext _context;
        private readonly IFwStatelogRepository _fwStatelogRepository;
        private readonly IFwCleanRepository _cleanRepository;
        private readonly IFwRepairRepository _fwRepairRepository;
        private readonly IYxOrderRepository _yxOrderRepository;
        private readonly IYxOrderlistRepository _yxOrderlistRepository;
        public FwHouseinfoController(IFwHouseinfoRepository fwHouseinfoRpt,
            HotelAppContext context,
            ISetHouseTypeRepository setHouseTypeRpt,
            IFwRepairRepository fwRepairRepository,
            ISysDicRepository sysDicRpt,
            IFwStatelogRepository fwStatelogRepository,
            IFwCleanRepository cleanRepository,
            IYxOrderRepository yxOrderRepository,
            IYxOrderlistRepository yxOrderlistRepository,
        IMapper mapper)
        {
            _fwHouseinfoRpt = fwHouseinfoRpt;
            _setHouseTypeRpt = setHouseTypeRpt;
            _fwStatelogRepository = fwStatelogRepository;
            _cleanRepository = cleanRepository;
            _fwRepairRepository = fwRepairRepository;
            _yxOrderRepository = yxOrderRepository;
            _yxOrderlistRepository = yxOrderlistRepository;
            _sysDicRpt = sysDicRpt;
            _context = context;
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
        /// <summary>
        /// 计算入住方式和客源的数量
        /// </summary>
        /// <returns></returns>
        [HttpGet("checkin")]
        public async Task<IActionResult> GetCheckin()
        {
            IEnumerable<fw_houseinfo> entityDto = null;
            await Task.Run(() =>
            {
                entityDto = _fwHouseinfoRpt.FindBy(f => f.IsValid && (f.State == 1003 || f.State == 1004)).ToList();
            });
            List<HouseCheckIn> houserCheckIns = new List<HouseCheckIn>();
            var orders = _yxOrderRepository.GetAll().ToList();
            var orderList = _yxOrderlistRepository.GetAll().ToList();
            foreach (var house in entityDto)
            {
                var odlist = orderList.FindAll(f => f.HouseCode == house.Code)
                    .OrderByDescending(f => f.CreatedAt).FirstOrDefault();
                if (odlist != null)
                {
                    var order = orders.Find(f => f.Id == odlist.OrderId);
                    houserCheckIns.Add(new HouseCheckIn()
                    {
                        Code = house.Code,
                        InType = order.InType,
                        ComeType = order.ComeType
                    });
                }
            }

            List<int> intype = new List<int>();
            List<int> comtype = new List<int>();
            foreach (var hs in houserCheckIns)
            {
                if (!intype.Contains(hs.InType))
                {
                    intype.Add(hs.InType);
                }
                if (!comtype.Contains(hs.ComeType))
                {
                    comtype.Add(hs.ComeType);
                }
            }

            var dicList = _sysDicRpt.GetAll().ToList();
            List<HouseInType> houseInTypeList = new List<HouseInType>();
            List<HouseComeType> houseComeTypeList = new List<HouseComeType>();
            foreach (var s1 in intype)
            {
                var dic = dicList.Find(f => f.Id == s1);
                if (dic != null)
                {
                    houseInTypeList.Add(new HouseInType()
                    {
                        InType = dic.Id,
                        InTypeTxt = dic.DicName,
                        Count = houserCheckIns.Count(f => f.InType == s1)
                    });
                }
            }
            foreach (var s2 in comtype)
            {
                var dic = dicList.Find(f => f.Id == s2);
                if (dic != null)
                {
                    houseComeTypeList.Add(new HouseComeType()
                    {
                        ComeType = dic.Id,
                        ComeTypeTxt = dic.DicName,
                        Count = houserCheckIns.Count(f => f.ComeType == s2)
                    });
                }
            }
            return new OkObjectResult(new HouseCheckInList()
            {
                HouseCheckIns = houserCheckIns,
                HouseInTypeList = houseInTypeList,
                HouseComeTypeList = houseComeTypeList
            });
        }

        // GET api/values/
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _fwHouseinfoRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        [HttpPost("clear")]
        public async Task<IActionResult> PostClear([FromBody] fw_clean value)
        {
            int oldState = int.Parse(value.CreatedBy);
            string createBy = string.Empty;
            if (User.Identity is ClaimsIdentity identity)
            {
                createBy = identity.Name ?? "test";
            }
            value.CreatedBy = createBy;
            value.IsValid = true;
            value.CleanTime = DateTime.Now;
            value.IsOverStay = false;

            using (var tran = _context.Database.BeginTransaction())
            {
                try
                {
                    //增加扫房日志
                    _cleanRepository.Add(value);
                    _cleanRepository.Commit();
                    //新增房态日志
                    _fwStatelogRepository.Add(new fw_statelog()
                    {
                        HouseCode = value.HouseCode,
                        OldState = oldState,
                        NewState = 1001,
                        OrderNo = string.Empty,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        IsValid = true,
                        CreatedBy = createBy
                    });
                    _fwStatelogRepository.Commit();
                    //修改房屋状态
                   var house = _fwHouseinfoRpt.GetSingle(f => f.Code == value.HouseCode);
                    if (house != null)
                    {
                        house.State = 1001;
                    }
                    _fwHouseinfoRpt.Commit();

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return new BadRequestResult();
                }
            }
            return new OkObjectResult(value);
        }
        [HttpPost("repair")]
        public async Task<IActionResult> PostRepair([FromBody] fw_repair value)
        {
            return new OkObjectResult(value);
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
