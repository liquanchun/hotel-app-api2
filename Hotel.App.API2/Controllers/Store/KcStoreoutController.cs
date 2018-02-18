using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.App.Data.Abstract;
using Hotel.App.Model.Store;
using Hotel.App.API2.Core;
using AutoMapper;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Hotel.App.Data;
using Hotel.App.Model.Dto;
using Hotel.App.Model.SYS;
using NLog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class KcStoreoutController : Controller
    {
		private readonly IMapper _mapper;
        private readonly IKcStoreoutRepository _kcStoreoutRpt;
        private readonly IKcStoreoutlistRepository _kcStoreoutlistRpt;
        private readonly IKcStoreRepository _kcStoreRpt;
        private readonly IKcGoodsRepository _kcGoodsRepository;
        private readonly ISysUserRepository _sysUserRepository;
        private readonly ISysDicRepository _sysDicRepository;
        private readonly ISysOrgRepository _sysOrgRepository;
        private readonly HotelAppContext _context;
        public KcStoreoutController(IKcStoreoutRepository kcStoreoutRpt, HotelAppContext context,
            IKcStoreRepository kcStoreRpt,
            ISysUserRepository sysUserRepository,
            ISysDicRepository sysDicRepository,
            ISysOrgRepository sysOrgRepository,
            IKcGoodsRepository kcGoodsRepository,
        IKcStoreoutlistRepository kcStoreoutlistRepository, IMapper mapper)
        {
            _kcStoreoutRpt = kcStoreoutRpt;
            _mapper = mapper;
            _context = context;
            _kcStoreoutlistRpt = kcStoreoutlistRepository;
            _kcStoreRpt = kcStoreRpt;
            _sysUserRepository = sysUserRepository;
            _sysDicRepository = sysDicRepository;
            _sysOrgRepository = sysOrgRepository;
            _kcGoodsRepository = kcGoodsRepository;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<kc_storeout> entityDto = null;
            IEnumerable<kc_storeoutlist> entityDetailDto = null;
            await Task.Run(() =>
            {
                entityDto = _kcStoreoutRpt.FindBy(f => f.IsValid).OrderByDescending(f=> f.CreatedAt);
                entityDetailDto = _kcStoreoutlistRpt.GetAll();
            });
            var storeoutDtoList = _mapper.Map<IEnumerable<kc_storeout>, IEnumerable<StoreOutGridDto>>(entityDto).ToList();
            var storeoutDetailDtoList = _mapper.Map<IEnumerable<kc_storeoutlist>, IEnumerable<StoreOutListDto>>(entityDetailDto).ToList();

            var sysUserList = _sysUserRepository.GetAll();
            var sysDicList = _sysDicRepository.GetAll();
            var sysOrgList = _sysOrgRepository.GetAll();
            var kcGoodsList = _kcGoodsRepository.GetAll();

            var sysDics = sysDicList as sys_dic[] ?? sysDicList.ToArray();
            var sysUsers = sysUserList as sys_user[] ?? sysUserList.ToArray();
            var sysOrgs = sysOrgList as sys_org[] ?? sysOrgList.ToArray();

            foreach (var store in storeoutDtoList)
            {
                store.OperatorTxt = sysUsers.FirstOrDefault(f => f.Id == store.Operator)?.UserName;
                store.OrgIdTxt = sysOrgs.FirstOrDefault(f => f.Id == store.OrgId)?.DeptName;
                store.StoreIdTxt = sysDics.FirstOrDefault(f => f.Id == store.StoreId)?.DicName;
                store.TypeIdTxt = sysDics.FirstOrDefault(f => f.Id == store.TypeId)?.DicName;
            }
            var kcGoodses = kcGoodsList as kc_goods[] ?? kcGoodsList.ToArray();
            foreach (var strdetail in storeoutDetailDtoList)
            {
                strdetail.GoodsIdTxt = kcGoodses.FirstOrDefault(f => f.Id == strdetail.GoodsId)?.Name;
                strdetail.GoodsTypeIdTxt = sysDics.FirstOrDefault(f => f.Id == strdetail.GoodsTypeId)?.DicName;
            }
            return new OkObjectResult(new StoreOutAllDto() { StoreOutList = storeoutDtoList, StoreOutDetailList = storeoutDetailDtoList });
        }
        // GET api/values/5
        [HttpGet("byorg")]
        public async Task<IActionResult> GetGroupByOrg()
        {
            //var single = _kcStoreoutRpt.FindBy(f => f.IsValid).GroupBy(f => f.OrgId).Select(f => f.ToList());
            var query = from m in _kcStoreoutRpt.FindBy(f => f.IsValid)
                        group m by m.OrgId
                into g
                        join o in _sysOrgRepository.FindBy(f => f.IsValid) on g.FirstOrDefault().OrgId equals o.Id
                        select new { Name = o.DeptName, Value = g.Count() };
            return new OkObjectResult(query);
        }

        [HttpGet("bymonth")]
        public async Task<IActionResult> GetGroupByMonth()
        {
            var orgList = _sysOrgRepository.FindBy(f => f.IsValid && f.ParentId > 0).ToList();
            var storeOutList = _kcStoreoutRpt.FindBy(f => f.IsValid).ToList();
            Dictionary<string, Dictionary<string, int>> dictionary2 = new Dictionary<string, Dictionary<string, int>>();

            try
            {
                foreach (var o in orgList)
                {
                    Dictionary<string, int> dictionary = new Dictionary<string, int>();
                    for (int i = 5; i >= 0; i--)
                    {
                        var dateTime = i > 0 ? DateTime.Today.AddMonths(-i) : DateTime.Today;
                        var startTime = new DateTime(dateTime.Year, dateTime.Month, 1);
                        var endTime = startTime.AddMonths(1).AddDays(-1);
                        var storeout = storeOutList
                            .FindAll(f => f.CreatedAt >= startTime && f.CreatedAt <= endTime && f.OrgId == o.Id)
                            .ToList();

                        dictionary.Add(dateTime.ToString("yyyy年MM月"), storeout.Count);
                    }
                    dictionary2.Add(o.DeptName, dictionary);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new OkObjectResult(dictionary2);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _kcStoreoutRpt.GetSingle(id);
            return new OkObjectResult(single);
        }
        [HttpGet("cancel/{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var single = _kcStoreoutRpt.GetSingle(id);
            single.Status = "作废";
            single.UpdatedAt = DateTime.Now;
            if (User.Identity is ClaimsIdentity identity)
            {
                single.CreatedBy = identity.Name ?? "test";
            }
            using (var tran = _context.Database.BeginTransaction())
            {
                try
                {
                    var storeList = _kcStoreoutlistRpt.FindBy(f => f.orderno == single.OrderNo);
                    foreach (var store in storeList)
                    {
                        var kucun = _kcStoreRpt.GetSingle(f =>
                            f.GoodsId == store.GoodsId && f.StoreId == single.StoreId);
                        if (kucun != null)
                        {
                            kucun.Amount = kucun.Amount + store.amount;
                            kucun.Number = kucun.Number + store.number;
                        }
                    }
                    _kcStoreRpt.Commit();
                    _kcStoreoutRpt.Commit();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return BadRequest(ex.Message);
                }
            }
            return new OkObjectResult(single);
        }
        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]StoreOutDto value)
        {
            if (value.Storeout != null && value.StoreoutList != null)
            {
                if (_kcStoreoutRpt.Exist(f => f.OrderNo == value.Storeout.OrderNo))
                {
                    return BadRequest($"出库单号{value.Storeout.OrderNo}已经存在。");
                }
                var storeOut = value.Storeout;
                storeOut.CreatedAt = DateTime.Now;
                storeOut.UpdatedAt = DateTime.Now;
                storeOut.IsValid = true;
                storeOut.Status = "正常";
                if (User.Identity is ClaimsIdentity identity)
                {
                    storeOut.CreatedBy = identity.Name ?? "test";
                }
                //storeOut.OrderNo = GetOrderNo();
                using (var tran = _context.Database.BeginTransaction())
                {
                    try
                    {
                        //入库单
                        _kcStoreoutRpt.Add(storeOut);
                        _kcStoreoutRpt.Commit();
                        foreach (var store in value.StoreoutList)
                        {
                            //入库明细
                            store.orderno = storeOut.OrderNo;
                            _kcStoreoutlistRpt.Add(store);
                            _kcStoreoutlistRpt.Commit();

                            //更新库存(仓库，货位，产品)
                            var kucun = _kcStoreRpt.GetSingle(f =>
                                f.GoodsId == store.GoodsId && f.StoreId == storeOut.StoreId);
                            if (kucun != null)
                            {
                                kucun.Amount = kucun.Amount - store.amount;
                                kucun.Number = kucun.Number - store.number;
                            }
                            _kcStoreRpt.Commit();
                        }
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return BadRequest(ex.Message);
                    }
                }
                return new NoContentResult();
            }
            return new BadRequestResult();
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]kc_storeout value)
        {
            var single = _kcStoreoutRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
            //更新字段内容
            single.UpdatedAt = DateTime.Now;
            if (User.Identity is ClaimsIdentity identity)
            {
                single.CreatedBy = identity.Name ?? "test";
            }
            _kcStoreoutRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _kcStoreoutRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }
            single.IsValid = false;
            _kcStoreoutRpt.Commit();

            return new NoContentResult();
        }

        /// <summary>
        /// 获取订单号
        /// </summary>
        /// <param name="intype"></param>
        /// <returns></returns>
        [HttpGet("OrderNo")]
        public IActionResult GetOrderNo()
        {
            string preCode = "CK";
            int orderCount = _kcStoreoutRpt
                .FindBy(f => f.CreatedAt > DateTime.Today && f.CreatedAt < DateTime.Today.AddDays(1)).Count();
            string orderNo =
                $"{preCode}{DateTime.Today:yyyyMMdd}{(orderCount + 1).ToString().PadLeft(3, '0')}";
            return new OkObjectResult(new { data = orderNo });
        }
    }
}
