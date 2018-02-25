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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    public class YxBookController : Controller
    {
		private readonly IMapper _mapper;
        private readonly IYxBookRepository _yxBookRpt;
        private readonly IYxBookserviceRepository _yxBookserviceRepository;
        private readonly ISetHouseTypeRepository _setHouseTypeRepository;
        private readonly ISysDicRepository _sysDicRepository;
        private readonly HotelAppContext _context;
        public YxBookController(IYxBookRepository yxBookRpt,
            HotelAppContext context,
            ISetHouseTypeRepository setHouseTypeRepository,
            IYxBookserviceRepository yxBookserviceRepository,
            ISysDicRepository sysDicRepository,
        IMapper mapper)
        {
            _yxBookRpt = yxBookRpt;
            _setHouseTypeRepository = setHouseTypeRepository;
            _context = context;
            _mapper = mapper;
            _yxBookserviceRepository = yxBookserviceRepository;
            _sysDicRepository = sysDicRepository;
        }
        // GET: api/values
        [HttpGet("type/{type}")]
        public async Task<IActionResult> Get(string type)
        {
		    IEnumerable<yx_book> entityDto = null;
            await Task.Run(() =>
            {
                if (type == "0")
                {
                    entityDto = _yxBookRpt.FindBy(f => f.IsValid && f.Status == "取消");
                }
                else if (type == "1")
                {
                    entityDto = _yxBookRpt.FindBy(f => f.IsValid && f.Status != "取消");
                }
                else
                {
                    entityDto = _yxBookRpt.FindBy(f => f.IsValid);
                }
            });
            var entity = _mapper.Map<IEnumerable<yx_book>, IEnumerable<BookingDto>>(entityDto).ToList();
            var houseList = _setHouseTypeRepository.GetAll().ToList();
            foreach (var hs in entity)
            {
                var dic = houseList.FirstOrDefault(f => f.Id == hs.HouseTypeId);
                if (dic != null) hs.HouseTypeName = dic.TypeName;
            }

            var dicList = _sysDicRepository.GetAll().ToList();
            foreach (var hs in entity)
            {
                var dic = dicList.FirstOrDefault(f => f.Id == hs.CheckInType);
                if (dic != null) hs.CheckInTypeTxt = dic.DicName;
            }
            return new OkObjectResult(entity);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _yxBookRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NewBooking value)
        {
            yx_book book = value.Booking;
            book.Status = "未完成";
            book.OrderNo = GetOrderNo();
            book.CreatedAt = DateTime.Now;
            book.UpdatedAt = DateTime.Now;
            book.BookTime = DateTime.Now;
            book.IsValid = true;
            if(User.Identity is ClaimsIdentity identity)
            {
                book.CreatedBy = identity.Name ?? "test";
            }
            using (var tran = _context.Database.BeginTransaction())
            {
                try
                {
                    _yxBookRpt.Add(book);
                    _yxBookRpt.Commit();
                    //保存预约服务项目
                    if (value.Bookservices != null)
                    {
                        foreach (var ser in value.Bookservices)
                        {
                            ser.Id = 0;
                            ser.OrderNo = book.OrderNo;
                            _yxBookserviceRepository.Add(ser);
                        }
                    }
                    _yxBookserviceRepository.Commit();
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
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]yx_book value)
        {
            var single = _yxBookRpt.GetSingle(id);

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
            _yxBookRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _yxBookRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }
            single.Status = "取消";
            _yxBookRpt.Commit();

            return new NoContentResult();
        }
        /// <summary>
        /// 获取订单号
        /// </summary>
        /// <param name="intype"></param>
        /// <returns></returns>
        private string GetOrderNo()
        {
            string preCode = "YY";
            int orderCount = _yxBookRpt
                .FindBy(f => f.CreatedAt > DateTime.Today && f.CreatedAt < DateTime.Today.AddDays(1)).Count();
            string orderNo =
                $"{preCode}{DateTime.Today.ToString("yyyyMMdd")}{(orderCount + 1).ToString().PadLeft(3, '0')}";
            return orderNo;
        }
    }
}
