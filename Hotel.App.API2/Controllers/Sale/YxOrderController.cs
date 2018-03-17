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
    public class YxOrderController : Controller
    {
		private readonly IMapper _mapper;
        private readonly IYxOrderRepository _yxOrderRpt;
        private readonly IYxOrderlistRepository _yxOrderlistRpt;
        private readonly IFwHouseinfoRepository _fwHouseinfoRpt;
        private readonly IFwStatelogRepository _fwStatelogRepository;
        private readonly ISetPaytypeRepository _setPaytypeRepository;
        private readonly ISetHouseTypeRepository _setHouseTypeRepository;
        private readonly IYxCustomerRepository _yxCustomerRpt;
        private readonly ICwPrefeeRepository _cwPrefeeRepository;
        private readonly ICwPreauthRepository _cwPreauthRepository;
        private readonly IYxBookRepository _bookRepository;
        private readonly IYxBookserviceRepository _yxBookserviceRepository;
        private readonly IYxOrderserviceRepository _yxOrderserviceRepository;
        private readonly ISysDicRepository _sysDicRepository;
        private readonly HotelAppContext _context;
        public YxOrderController(IYxOrderRepository yxOrderRpt, 
            IYxOrderlistRepository yxOrderlistRpt,
            IFwHouseinfoRepository fwHouseinfoRpt,
            IFwStatelogRepository fwStatelogRepository,
            ISetPaytypeRepository setPaytypeRepository,
            ISetHouseTypeRepository setHouseTypeRepository,
            IYxCustomerRepository yxCustomerRpt,
            ICwPrefeeRepository cwPrefeeRepository,
            ICwPreauthRepository cwPreauthRepository,
            IYxBookRepository bookRepository,
            IYxBookserviceRepository yxBookserviceRepository,
            IYxOrderserviceRepository yxOrderserviceRepository,
            ISysDicRepository sysDicRepository,
        HotelAppContext context,
                IMapper mapper)
        {
            _yxOrderRpt = yxOrderRpt;
            _yxOrderlistRpt = yxOrderlistRpt;
            _fwHouseinfoRpt = fwHouseinfoRpt;
            _fwStatelogRepository = fwStatelogRepository;
            _setPaytypeRepository = setPaytypeRepository;
            _setHouseTypeRepository = setHouseTypeRepository;
            _yxCustomerRpt = yxCustomerRpt;
            _cwPrefeeRepository = cwPrefeeRepository;
            _cwPreauthRepository = cwPreauthRepository;
            _bookRepository = bookRepository;
            _yxBookserviceRepository = yxBookserviceRepository;
            _yxOrderserviceRepository = yxOrderserviceRepository;
            _sysDicRepository = sysDicRepository;
            _context = context;
            _mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<yx_order> entityDto = null;
            await Task.Run(() =>
            {
                entityDto = _yxOrderRpt.FindBy(f =>
                    f.IsValid);  //&& f.CreatedAt > DateTime.Today && f.CreatedAt < DateTime.Today.AddDays(1)
            });
            var orderDtoList = _mapper.Map<IEnumerable<yx_order>, IEnumerable<OrderDto>>(entityDto).ToList();
            var payTypeList = this._setPaytypeRepository.GetAll();
            var houseTypeList = this._setHouseTypeRepository.GetAll();

            var entityListDto = new List<yx_orderlist>();
            foreach (var od in orderDtoList)
            {
                od.PayTypeTxt = payTypeList.FirstOrDefault(f => f.Id == od.PayType).Name;
                entityListDto.AddRange(_yxOrderlistRpt.FindBy(f => f.OrderId == od.Id));
            }

            var orderDetailList = _mapper.Map<List<yx_orderlist>, List<OrderListDto>>(entityListDto).ToList();
            foreach (var odt in orderDetailList)
            {
                odt.HouseTypeTxt = houseTypeList.FirstOrDefault(f => f.Id == odt.HouseType).TypeName;
            }
            var orderObj = new OrderDataDto { OrderList = orderDtoList, OrderDetailList = orderDetailList};
            return new OkObjectResult(orderObj);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _yxOrderRpt.GetSingle(id);
            return new OkObjectResult(single);
        }
        /// <summary>
        /// 客人入住办理
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CheckInOrderDto value)
        {
            //订单信息
            string createBy = string.Empty;
            var order = value.YxOrder;
            order.CreatedAt = DateTime.Now;
            order.UpdatedAt = DateTime.Now;
            order.IsValid = true;
            if (User.Identity is ClaimsIdentity identity)
            {
                createBy = identity.Name ?? "test";
            }
            order.CreatedBy = createBy;
            order.Status = "未结账";
            order.OrderNo = GetOrderNo(order.InType);
            _yxOrderRpt.Add(order);
            //事务处理
            using (var tran = _context.Database.BeginTransaction())
            {
                try
                {
                    _yxOrderRpt.Commit();
                    //订单明细
                    foreach (var orderDetail in value.YxOrderList)
                    {
                        orderDetail.OrderId = order.Id;
                        orderDetail.CreatedAt = DateTime.Now;
                        orderDetail.UpdatedAt = DateTime.Now;
                        orderDetail.IsValid = true;
                        orderDetail.CreatedBy = createBy;
                        //计算预计退房时间
                        orderDetail.PreLeaveTime = DateTime.Today.AddDays(orderDetail.Days + 1).AddHours(12);
                        _yxOrderlistRpt.Add(orderDetail);
                        //修改房屋状态
                        var houseInfo = _fwHouseinfoRpt.FindBy(f => f.Code == orderDetail.HouseCode).FirstOrDefault();
                        //新增房态日志
                        _fwStatelogRepository.Add(new fw_statelog()
                        {
                            HouseCode = orderDetail.HouseCode,
                            OldState = houseInfo.State,
                            NewState = 1003,
                            OrderNo = order.OrderNo,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            IsValid = true,
                            CreatedBy = createBy
                        });
                        houseInfo.State = 1003;  //住人净
                        houseInfo.OrderNo = order.OrderNo;
                        houseInfo.CusName = value.YxOrderList.Count == 1 && orderDetail.CusName != order.CusName ? order.CusName + "，" + orderDetail.CusName : orderDetail.CusName;
                        //添加到客户资料表中
                        if (!_yxCustomerRpt.Exist(f => f.IDCardNo == order.IdCard))
                        {
                            var customer = new yx_customer
                            {
                                CustomerName = orderDetail.CusName,
                                IDCardNo = orderDetail.IdCard,
                                Mobile = order.CusPhone,
                                IsValid = true,
                                CreatedBy = createBy,
                                CreatedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now
                            };
                            _yxCustomerRpt.Add(customer);
                        }
                    }

                    if (order.PayType == 1)
                    {
                        //预授权支付
                        _cwPreauthRepository.Add(new cw_preauth()
                        {
                            HouseCode = value.YxOrderList.First().HouseCode,
                            CusName = order.CusName,
                            AuthNo = order.BillNo,
                            Amount = order.HouseFee,
                            OrderNo = order.OrderNo,
                            Remark = order.Remark,
                            CreatedBy = order.CreatedBy,
                            CreatedAt = DateTime.Now,
                            IsValid = true
                        });
                        _cwPreauthRepository.Commit();
                    }
                    else
                    {
                        //预定金
                        _cwPrefeeRepository.Add(new cw_prefee()
                        {
                            HouseCode = value.YxOrderList.First().HouseCode,
                            CusName = order.CusName,
                            PayType = order.PayType,
                            Amount = order.HouseFee,
                            OrderNo = order.OrderNo,
                            Remark = order.Remark,
                            CreatedBy = order.CreatedBy,
                            CreatedAt = DateTime.Now,
                            IsValid = true
                        });
                        _cwPrefeeRepository.Commit();
                    }
                    if (!string.IsNullOrEmpty(order.BookOrderNo))
                    {
                        var book = _bookRepository.GetSingle(f => f.OrderNo == order.BookOrderNo);
                        book.Status = "已完成";
                        _bookRepository.Commit();

                        var bookServices = _yxBookserviceRepository.FindBy(f => f.OrderNo == order.BookOrderNo);
                        foreach (var bs in bookServices)
                        {
                            var orderSer = new yx_orderservice()
                            {
                                OrderNo = order.OrderNo,
                                TypeId = bs.TypeId,
                                ItemCode = bs.ItemCode,
                                ServiceTime = bs.ServiceTime,
                                Times = bs.Times,
                                Price = bs.Price,
                                CreatedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now,
                                IsValid = true,
                                CreatedBy = order.CreatedBy
                            };
                            _yxOrderserviceRepository.Add(orderSer);
                        }
                        _yxOrderserviceRepository.Commit();
                    }
                    _yxCustomerRpt.Commit();
                    _fwStatelogRepository.Commit();
                    _fwHouseinfoRpt.Commit();
                    _yxOrderlistRpt.Commit();
                    tran.Commit();
                }
                catch (Exception e)
                {
                    tran.Rollback();
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
        private string GetOrderNo(int intype)
        {
            string preCode = "XX";
            var type = _sysDicRepository.GetSingle(intype);
            if (type != null)
            {
                switch (type.DicName)
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
            }
            int orderCount = _yxOrderRpt
                .FindBy(f => f.CreatedAt > DateTime.Today && f.CreatedAt < DateTime.Today.AddDays(1)).Count();
            string orderNo =
                $"{preCode}{DateTime.Today.ToString("yyyyMMdd")}{(orderCount + 1).ToString().PadLeft(3, '0')}";
            return orderNo;
        }
    }
}
