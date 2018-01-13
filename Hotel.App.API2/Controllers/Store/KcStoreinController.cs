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
using Hotel.App.Data;
using Hotel.App.Model.Dto;
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
        private readonly HotelAppContext _context;
        public KcStoreinController(IKcStoreinRepository kcStoreinRpt, HotelAppContext context,
            IKcStoreRepository kcStoreRpt,
            IKcStoreinlistRepository kcStoreinlistRepository,IMapper mapper)
        {
            _kcStoreinRpt = kcStoreinRpt;
			_mapper = mapper;
            _context = context;
            _kcStoreinlistRpt = kcStoreinlistRepository;
            _kcStoreRpt = kcStoreRpt;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
		    IEnumerable<kc_storein> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _kcStoreinRpt.FindBy(f => f.IsValid);
			});
            return new OkObjectResult(entityDto);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _kcStoreinRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]StoreInDto value)
        {
            if (value.Storein != null && value.StoreinList != null)
            {
                var storeIn = value.Storein;
                storeIn.CreatedAt = DateTime.Now;
                storeIn.UpdatedAt = DateTime.Now;
                storeIn.IsValid = true;
                storeIn.Status = "正常";
                if (User.Identity is ClaimsIdentity identity)
                {
                    storeIn.CreatedBy = identity.Name ?? "test";
                }
                storeIn.OrderNo = GetOrderNo();
                using (var tran = _context.Database.BeginTransaction())
                {
                    try
                    {
                        //入库单
                        _kcStoreinRpt.Add(storeIn);
                        foreach (var store in value.StoreinList)
                        {
                            //入库明细
                            store.orderno = storeIn.OrderNo;
                            _kcStoreinlistRpt.Add(store);
                            //更新库存
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
                                    CreatedBy = storeIn.CreatedBy
                                };
                                _kcStoreRpt.Add(kcstore);
                            }
                            else
                            {
                                kucun.Amount = kucun.Amount + store.amount;
                                kucun.Number = kucun.Number + store.number;
                            }
                        }
                        _kcStoreRpt.Commit();
                        _kcStoreinlistRpt.Commit();
                        _kcStoreinRpt.Commit();

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
        private string GetOrderNo()
        {
            string preCode = "RK";
            int orderCount = _kcStoreinRpt
                .FindBy(f => f.CreatedAt > DateTime.Today && f.CreatedAt < DateTime.Today.AddDays(1)).Count();
            string orderNo =
                $"{preCode}{DateTime.Today:yyyyMMdd}{(orderCount + 1).ToString().PadLeft(3, '0')}";
            return orderNo;
        }
    }
}
