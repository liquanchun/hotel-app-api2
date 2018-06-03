using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.App.Data.Abstract;
using Hotel.App.Model.SYS;
using Hotel.App.API2.Core;
using AutoMapper;
using System.Security.Claims;
using Hotel.App.API2.Common;
using Hotel.App.Model.Sale;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    public class SetPhotoController : Controller
    {
		private readonly IMapper _mapper;
        private readonly ISetPhotoRepository _setPhotoRpt;
        private readonly IHostingEnvironment _host;
        public SetPhotoController(ISetPhotoRepository setPhotoRpt,
            IHostingEnvironment host,IMapper mapper)
        {
            _setPhotoRpt = setPhotoRpt;
            _host = host;
			_mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
		    IEnumerable<set_photo> entityDto = null;
            await Task.Run(() =>
            {
				entityDto = _setPhotoRpt.FindBy(f => f.IsValid);
			});
            return new OkObjectResult(entityDto);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var single = _setPhotoRpt.GetSingle(id);
            return new OkObjectResult(single);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {
            set_photo value = new set_photo();
            value.CreatedAt = DateTime.Now;
			value.UpdatedAt = DateTime.Now;
			value.IsValid = true;
            if(User.Identity is ClaimsIdentity identity)
            {
                value.CreatedBy = identity.Name ?? "admin";
            }
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "upload");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            value.FileName = fileName;
            _setPhotoRpt.Add(value);
            _setPhotoRpt.Commit();
            return new OkObjectResult(value);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]set_photo value)
        {
            var single = _setPhotoRpt.GetSingle(id);

            if (single == null)
            {
                return NotFound();
            }

            ObjectCopy.Copy<set_photo>(single, value, new string[] { "tags", "typeName"});
			//更新字段内容
			single.UpdatedAt = DateTime.Now;
			if(User.Identity is ClaimsIdentity identity)
			{
				single.CreatedBy = identity.Name ?? "admin";
			}
            _setPhotoRpt.Commit();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var single = _setPhotoRpt.GetSingle(id);
            if (single == null)
            {
                return new NotFoundResult();
            }

            single.IsValid = false;
            _setPhotoRpt.Commit();

            return new NoContentResult();
        }
    }
}
