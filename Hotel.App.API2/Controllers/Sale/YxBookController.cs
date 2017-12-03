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
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    public class YxBookController : Controller
    {
		private readonly IMapper _mapper;
        private readonly IYxBookRepository _yxBookRpt;
        public YxBookController(IYxBookRepository yxBookRpt,
				IMapper mapper)
        {
            _yxBookRpt = yxBookRpt;
			_mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
		    IEnumerable<yx_book> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _yxBookRpt.FindBy(f => f.IsValid);
			});
            return new OkObjectResult(entityDto);
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
        public async Task<IActionResult> Post([FromBody]yx_book value)
        {
            value.Status = "未完成";
            value.OrderNo = GetOrderNo();
            value.CreatedAt = DateTime.Now;
			value.UpdatedAt = DateTime.Now;
            value.BookTime = DateTime.Now;
            value.IsValid = true;
            if(User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "test";
            }
            _yxBookRpt.Add(value);
            _yxBookRpt.Commit();
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
