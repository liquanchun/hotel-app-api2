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
    public class KcStoreinController : Controller
    {
		private readonly IMapper _mapper;
        private readonly IKcStoreinRepository _kcStoreinRpt;
        private readonly IKcStoreinlistRepository _kcStoreinlistRpt;
        private readonly IKcStoreRepository _kcStoreRpt;
        private readonly IKcGoodsRepository _kcGoodsRepository;
        private readonly ISysUserRepository _sysUserRepository;
        private readonly IKcSupplierRepository _kcSupplierRepository;
        private readonly ISysDicRepository _sysDicRepository;
        private readonly ISysOrgRepository _sysOrgRepository;
        private readonly HotelAppContext _context;
        public KcStoreinController(IKcStoreinRepository kcStoreinRpt, HotelAppContext context,
            IKcStoreRepository kcStoreRpt,
            ISysUserRepository sysUserRepository,
            IKcSupplierRepository kcSupplierRepository,
            ISysDicRepository sysDicRepository,
            ISysOrgRepository sysOrgRepository,
            IKcGoodsRepository kcGoodsRepository,
        IKcStoreinlistRepository kcStoreinlistRepository,IMapper mapper)
        {
            _kcStoreinRpt = kcStoreinRpt;
			_mapper = mapper;
            _context = context;
            _kcStoreinlistRpt = kcStoreinlistRepository;
            _kcStoreRpt = kcStoreRpt;
            _sysUserRepository = sysUserRepository;
            _kcSupplierRepository = kcSupplierRepository;
            _sysDicRepository = sysDicRepository;
            _sysOrgRepository = sysOrgRepository;
            _kcGoodsRepository = kcGoodsRepository;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<kc_storein> entityDto = null;
            IEnumerable<kc_storeinlist> entityDetailDto = null;
            await Task.Run(() =>
            {
                entityDto = _kcStoreinRpt.FindBy(f => f.IsValid).OrderByDescending(f => f.CreatedAt);
                entityDetailDto = _kcStoreinlistRpt.GetAll();
            });
            var storeinDtoList = _mapper.Map<IEnumerable<kc_storein>, IEnumerable<StoreInGridDto>>(entityDto).ToList();
            var storeinDetailDtoList = _mapper.Map<IEnumerable<kc_storeinlist>, IEnumerable<StoreInListDto>>(entityDetailDto).ToList();

            var sysUserList = _sysUserRepository.GetAll();
            var kcSupplierList = _kcSupplierRepository.GetAll();
            var sysDicList = _sysDicRepository.GetAll();
            var sysOrgList = _sysOrgRepository.GetAll();
            var kcGoodsList = _kcGoodsRepository.GetAll();

            var sysDics = sysDicList as sys_dic[] ?? sysDicList.ToArray();
            var sysUsers = sysUserList as sys_user[] ?? sysUserList.ToArray();
            var sysOrgs = sysOrgList as sys_org[] ?? sysOrgList.ToArray();
            var kcSuppliers = kcSupplierList as kc_supplier[] ?? kcSupplierList.ToArray();

            foreach (var store in storeinDtoList)
            {
                store.OperatorTxt = sysUsers.FirstOrDefault(f => f.Id == store.Operator)?.UserName;
                store.OrgIdTxt = sysOrgs.FirstOrDefault(f => f.Id == store.OrgId)?.DeptName;
                store.StoreIdTxt = sysDics.FirstOrDefault(f => f.Id == store.StoreId)?.DicName;
                store.SupplierIdTxt = kcSuppliers.FirstOrDefault(f => f.Id == store.SupplierId)?.Name;
                store.TypeIdTxt = sysDics.FirstOrDefault(f => f.Id == store.TypeId)?.DicName;
            }
            var kcGoodses = kcGoodsList as kc_goods[] ?? kcGoodsList.ToArray();
            foreach (var strdetail in storeinDetailDtoList)
            {
                strdetail.GoodsIdTxt = kcGoodses.FirstOrDefault(f => f.Id == strdetail.GoodsId)?.Name;
                strdetail.GoodsTypeIdTxt = sysDics.FirstOrDefault(f => f.Id == strdetail.GoodsTypeId)?.DicName;
            }
            return new OkObjectResult(new StoreInAllDto(){  StoreInList = storeinDtoList , StoreInDetailList = storeinDetailDtoList });
        }
        // GET api/values/5
        [HttpGet("byorg")]
        public async Task<IActionResult> GetGroupByOrg()
        {
            //var single = _kcStoreinRpt.FindBy(f => f.IsValid).GroupBy(f => f.OrgId).Select(f => f.ToList());
            var query = from m in _kcStoreinRpt.FindBy(f => f.IsValid)
                group m by m.OrgId
                into g
                join o in _sysOrgRepository.FindBy(f => f.IsValid) on g.FirstOrDefault().OrgId equals o.Id
                select new {Name = o.DeptName, Value = g.Count()};
            return new OkObjectResult(query);
        }

        [HttpGet("bymonth")]
        public async Task<IActionResult> GetGroupByMonth()
        {
            var orgList = _sysOrgRepository.FindBy(f => f.IsValid && f.ParentId > 0).ToList();
            var storeInList = _kcStoreinRpt.FindBy(f => f.IsValid).ToList();
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
                        var storein = storeInList
                            .FindAll(f => f.CreatedAt >= startTime && f.CreatedAt <= endTime && f.OrgId == o.Id)
                            .ToList();

                        dictionary.Add(dateTime.ToString("yyyy年MM月"), storein.Count);
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
            var single = _kcStoreinRpt.GetSingle(id);
            return new OkObjectResult(single);
        }
        [HttpGet("cancel/{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var single = _kcStoreinRpt.GetSingle(id);
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
                    var storeList = _kcStoreinlistRpt.FindBy(f => f.orderno == single.OrderNo);
                    foreach (var store in storeList)
                    {
                        var kucun = _kcStoreRpt.GetSingle(f =>
                            f.GoodsId == store.GoodsId && f.StoreId == single.StoreId);
                        if (kucun != null)
                        {
                            kucun.Amount = kucun.Amount - store.amount;
                            kucun.Number = kucun.Number - store.number;
                        }
                    }
                    _kcStoreRpt.Commit();
                    _kcStoreinRpt.Commit();
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
        public async Task<IActionResult> Post([FromBody]StoreInDto value)
        {
            if (value.Storein != null && value.StoreinList != null)
            {
                if (_kcStoreinRpt.Exist(f => f.OrderNo == value.Storein.OrderNo))
                {
                    return BadRequest($"入库单号{value.Storein.OrderNo}已经存在。");
                }
                var storeIn = value.Storein;
                storeIn.CreatedAt = DateTime.Now;
                storeIn.UpdatedAt = DateTime.Now;
                storeIn.IsValid = true;
                storeIn.Status = "正常";
                if (User.Identity is ClaimsIdentity identity)
                {
                    storeIn.CreatedBy = identity.Name ?? "test";
                }
                //storeIn.OrderNo = GetOrderNo();
                using (var tran = _context.Database.BeginTransaction())
                {
                    try
                    {
                        //入库单
                        _kcStoreinRpt.Add(storeIn);
                        _kcStoreinRpt.Commit();
                        foreach (var store in value.StoreinList)
                        {
                            //入库明细
                            store.orderno = storeIn.OrderNo;
                            _kcStoreinlistRpt.Add(store);
                            _kcStoreinlistRpt.Commit();

                            //更新库存(仓库，货位，产品)
                            var kucun = _kcStoreRpt.GetSingle(f =>
                                f.GoodsId == store.GoodsId && f.StoreId == storeIn.StoreId);
                            if (kucun == null)
                            {
                                var kcstore = new kc_store
                                {
                                    StoreId = storeIn.StoreId,
                                    GoodsId = store.GoodsId,
                                    Amount = store.amount,
                                    Number = store.number,
                                    CreatedAt = DateTime.Now,
                                    UpdatedAt = DateTime.Now,
                                    IsValid = true,
                                    CreatedBy = storeIn.CreatedBy,
                                    OrgId = storeIn.OrgId,
                                    GoodsSite = store.goodssite,
                                    GoodsTypeId = store.GoodsTypeId
                                };
                                _kcStoreRpt.Add(kcstore);
                            }
                            else
                            {
                                kucun.Amount = kucun.Amount + store.amount;
                                kucun.Number = kucun.Number + store.number;
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
            return  new BadRequestResult();
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]kc_storein value)
        {
            var single = _kcStoreinRpt.GetSingle(id);

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
            _kcStoreinRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _kcStoreinRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }
            single.IsValid = false;
            _kcStoreinRpt.Commit();

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
            string preCode = "RK";
            int orderCount = _kcStoreinRpt
                .FindBy(f => f.CreatedAt > DateTime.Today && f.CreatedAt < DateTime.Today.AddDays(1)).Count();
            string orderNo =
                $"{preCode}{DateTime.Today:yyyyMMdd}{(orderCount + 1).ToString().PadLeft(3, '0')}";
            return new OkObjectResult(new {data = orderNo});
        }
    }
}
