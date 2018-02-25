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
    public class YxBookserviceController : Controller
    {
		private readonly IMapper _mapper;
        private readonly IYxBookserviceRepository _yxBookserviceRpt;
        private readonly IYxServiceitemRepository _yxServiceitemRpt;
        private readonly ISysDicRepository _sysDicRpt;
        public YxBookserviceController(IYxBookserviceRepository yxBookserviceRpt,
            IYxServiceitemRepository yxServiceitemRpt,
            ISysDicRepository sysDicRpt,
                IMapper mapper)
        {
            _yxBookserviceRpt = yxBookserviceRpt;
            _yxServiceitemRpt = yxServiceitemRpt;
            _sysDicRpt = sysDicRpt;
            _mapper = mapper;
        }
        // GET: api/values
        [HttpGet("{orderNo}")]
        public async Task<IActionResult> Get(string orderNo)
        {
		    IEnumerable<yx_bookservice> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _yxBookserviceRpt.FindBy(f => f.OrderNo == orderNo);
			});
            var entity = _mapper.Map<IEnumerable<yx_bookservice>, IEnumerable<BookServiceDto>>(entityDto).ToList();
            var dicList = _sysDicRpt.GetAll().ToList();
            foreach (var hs in entity)
            {
                var dic = dicList.FirstOrDefault(f => f.Id == hs.TypeId);
                if (dic != null) hs.TypeIdTxt = dic.DicName;
            }

            var serList = _yxServiceitemRpt.GetAll().ToList();
            foreach (var hs in entity)
            {
                var dic = serList.FirstOrDefault(f => f.ItemCode == hs.ItemCode);
                if (dic != null) hs.ItemName = dic.Name;
            }

            return new OkObjectResult(entity);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _yxBookserviceRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]yx_bookservice value)
        {
            _yxBookserviceRpt.Add(value);
            _yxBookserviceRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]yx_bookservice value)
        {
            var single = _yxBookserviceRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
            _yxBookserviceRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _yxBookserviceRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }

            _yxBookserviceRpt.Delete(single);
            _yxBookserviceRpt.Commit();

            return new NoContentResult();
        }
    }
}
