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
    public class YxBooklistController : Controller
    {
		private readonly IMapper _mapper;
        private readonly IYxBooklistRepository _yxBooklistRpt;
        public YxBooklistController(IYxBooklistRepository yxBooklistRpt,
				IMapper mapper)
        {
            _yxBooklistRpt = yxBooklistRpt;
			_mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
		    IEnumerable<yx_booklist> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _yxBooklistRpt.GetAll();
			});
            return new OkObjectResult(entityDto);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _yxBooklistRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]yx_booklist value)
        {
            _yxBooklistRpt.Add(value);
            _yxBooklistRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]yx_booklist value)
        {
            var single = _yxBooklistRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }
            //更新字段内容
            _yxBooklistRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _yxBooklistRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }
            _yxBooklistRpt.Commit();

            return new NoContentResult();
        }
    }
}
