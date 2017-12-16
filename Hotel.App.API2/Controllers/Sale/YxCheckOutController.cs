using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.App.Data.Abstract;
using Hotel.App.Model.Sale;
using Hotel.App.API2.Core;
using AutoMapper;
using System.Security.Claims;
using Hotel.App.Data;
using Hotel.App.Model.Account;
using Hotel.App.Model.Dto;
using Hotel.App.Model.House;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    public class YxCheckOutController : Controller
    {
		private readonly IMapper _mapper;
        private readonly IYxOrderRepository _yxOrderRpt;
        private readonly IYxOrderlistRepository _yxOrderlistRpt;
        private readonly IFwHouseinfoRepository _fwHouseinfoRpt;
        private readonly IFwStatelogRepository _fwStatelogRepository;
        private readonly ISetPaytypeRepository _setPaytypeRepository;
        private readonly ISetHouseTypeRepository _setHouseTypeRepository;
        private readonly IYxCustomerRepository _yxCustomerRpt;
        private readonly ISysDicRepository _sysDicRpt;
        private readonly ICwCusaccountRepository _cwCusaccountRpt;
        private readonly HotelAppContext _context;
        public YxCheckOutController(IYxOrderRepository yxOrderRpt, 
            IYxOrderlistRepository yxOrderlistRpt,
            IFwHouseinfoRepository fwHouseinfoRpt,
            IFwStatelogRepository fwStatelogRepository,
            ISetPaytypeRepository setPaytypeRepository,
            ISetHouseTypeRepository setHouseTypeRepository,
            IYxCustomerRepository yxCustomerRpt,
            ICwCusaccountRepository cwCusaccountRpt,
        ISysDicRepository sysDicRpt,
        HotelAppContext context,
                IMapper mapper)
        {
            _sysDicRpt = sysDicRpt;
            _yxOrderRpt = yxOrderRpt;
            _yxOrderlistRpt = yxOrderlistRpt;
            _fwHouseinfoRpt = fwHouseinfoRpt;
            _fwStatelogRepository = fwStatelogRepository;
            _setPaytypeRepository = setPaytypeRepository;
            _setHouseTypeRepository = setHouseTypeRepository;
            _yxCustomerRpt = yxCustomerRpt;
            _cwCusaccountRpt = cwCusaccountRpt;
            _context = context;
            _mapper = mapper;
        }
        // GET: api/values
        [HttpGet("{code}")]
        public async Task<IActionResult> Get(string code)
        {
            //订单信息
            var orderDetail = _yxOrderlistRpt.FindBy(f => f.HouseCode == code && f.IsValid).OrderByDescending(f => f.Id)
                .First();
            if (orderDetail == null)
            {
                return BadRequest($"未找到房号{code}的订单");
            }
            //房屋状态
            var houseInfo = _fwHouseinfoRpt.GetSingle(f => f.Code == code);
            if (houseInfo.State != 1003 && houseInfo.State != 1004)
            {
                var state = _sysDicRpt.GetSingle(houseInfo.State);
                return BadRequest($"房号{code}状态为{state.DicName}，不能结算。");
            }
            //订单信息
            var orderInfo = _yxOrderRpt.GetSingle(orderDetail.OrderId);
            if (orderInfo == null)
            {
                return BadRequest($"未找到房号{code}订单信息。");
            }
            //订单列表
            var houseTypeList = this._setHouseTypeRepository.GetAll();
            var orderDetailList = _yxOrderlistRpt.FindBy(f => f.OrderId == orderInfo.Id).ToList();
            var orderDetaiDTOlList = _mapper.Map<List<yx_orderlist>, List<OrderListDto>>(orderDetailList).ToList();
            foreach (var odt in orderDetaiDTOlList)
            {
                odt.HouseTypeTxt = houseTypeList.FirstOrDefault(f => f.Id == odt.HouseType).TypeName;
            }

            var orderDto = _mapper.Map<yx_order, OrderDto>(orderInfo);
            var payTypeList = this._setPaytypeRepository.GetAll();
            orderDto.PayTypeTxt = payTypeList.FirstOrDefault(f => f.Id == orderDto.PayType).Name;
            if (orderDto.ComeType > 0 && _sysDicRpt.GetSingle(orderDto.ComeType) != null)
            {
                orderDto.ComeTypeTxt = _sysDicRpt.GetSingle(orderDto.ComeType).DicName;
            }
            var orderObj = new OrderDataDto { OrderList = new List<OrderDto>(){orderDto}, OrderDetailList = orderDetaiDTOlList };
            return new OkObjectResult(orderObj);
        }
        /// <summary>
        /// 客人退房办理,对于整张单结算
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]cw_cusaccount value)
        {
            value.CreatedAt = DateTime.Now;
            value.IsValid = true;
            string createBy = string.Empty;
            if (User.Identity is ClaimsIdentity identity)
            {
                createBy = identity.Name ?? "test";
            }
            value.CreatedBy = createBy;

            using (var tran = _context.Database.BeginTransaction())
            {
                try
                {
                    _cwCusaccountRpt.Add(value);
                    _cwCusaccountRpt.Commit();

                    var orderDetail = _yxOrderlistRpt.FindBy(f => f.HouseCode == value.HouseCode && f.IsValid).OrderByDescending(f => f.Id)
                        .First();
                    //更改订单状态
                    var order = _yxOrderRpt.GetSingle(f => f.Id == orderDetail.OrderId);
                    order.Status = "已结算";
                    order.UpdatedAt = DateTime.Now;
                    _yxOrderRpt.Commit();

                    var orderDetailList = _yxOrderlistRpt.FindBy(f => f.OrderId == order.Id);
                    foreach (var ordd in orderDetailList)
                    {
                        //更新房态
                        var house = _fwHouseinfoRpt.GetSingle(f => f.Code == ordd.HouseCode);
                        house.State = 1002;//空脏
                        _fwHouseinfoRpt.Commit();

                        //新增房态日志
                        _fwStatelogRepository.Add(new fw_statelog()
                        {
                            HouseCode = orderDetail.HouseCode,
                            OldState = house.State,
                            NewState = 1002,
                            OrderNo = order.OrderNo,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            IsValid = true,
                            CreatedBy = createBy
                        });
                        _fwStatelogRepository.Commit();
                    }
                    tran.Commit();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]yx_order value)
        {
            var single = _yxOrderRpt.GetSingle(id);

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
            _yxOrderRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _yxOrderRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }
            string createBy = string.Empty;
            if (User.Identity is ClaimsIdentity identity)
            {
                createBy = identity.Name ?? "test";
            }

            single.Status = "已取消";
            var orderDetail = _yxOrderlistRpt.FindBy(f => f.OrderId == id);
            using (var tran = _context.Database.BeginTransaction())
            {
                try
                {
                    _yxOrderRpt.Commit();

                    foreach (var item in orderDetail)
                    {
                        var house = _fwHouseinfoRpt.GetSingle(f => f.Code == item.HouseCode);
                        //新增房态日志
                        _fwStatelogRepository.Add(new fw_statelog()
                        {
                            HouseCode = item.HouseCode,
                            OldState = house.State,
                            NewState = 1001,
                            OrderNo = single.OrderNo,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            IsValid = true,
                            CreatedBy = createBy
                        });

                        house.State = 1001;  //空净
                        house.CusName = string.Empty;
                        house.OrderNo = string.Empty;
                        _fwHouseinfoRpt.Update(house);
                    }
                    _fwStatelogRepository.Commit();
                    _fwHouseinfoRpt.Commit();
                    tran.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    tran.Rollback();
                    return new BadRequestResult();
                }
            }

            return new NoContentResult();
        }
        /// <summary>
        /// 获取订单号
        /// </summary>
        /// <param name="intype"></param>
        /// <returns></returns>
        private string GetOrderNo(string intype)
        {
            string preCode = string.Empty;
            switch (intype)
            {
                case "全天房":
                    preCode = "QT";
                    break;
                case "钟点房":
                    preCode = "ZD";
                    break;
                case "特殊房":
                    preCode = "TS";
                    break;
                case "免费房":
                    preCode = "MF";
                    break;
                default:
                    preCode = "DH";
                    break;
            }
            int orderCount = _yxOrderRpt
                .FindBy(f => f.CreatedAt > DateTime.Today && f.CreatedAt < DateTime.Today.AddDays(1)).Count();
            string orderNo =
                $"{preCode}{DateTime.Today.ToString("yyyyMMdd")}{(orderCount + 1).ToString().PadLeft(3, '0')}";
            return orderNo;
        }
    }
}
