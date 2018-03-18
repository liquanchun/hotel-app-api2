using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.App.Data.Abstract;
using Hotel.App.Model.SYS;
using AutoMapper;
using NLog;
using System.Security.Claims;
using Hotel.App.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SysStaffController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISysStaffRepository _sysStaffRpt;
        private readonly ISysOrgRepository _orgRepository;
        private readonly HotelAppContext _context;
        public SysStaffController(ISysStaffRepository sysStaffRpt, 
            ISysRoleRepository sysRoleRpt,
            ISysOrgRepository orgRepository,
        HotelAppContext context,
            IMapper mapper)
        {
            _sysStaffRpt = sysStaffRpt;
            _context = context;
            _mapper = mapper;
            _orgRepository = orgRepository;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<SysStaffDto> entityDto = null;
            var users = _sysStaffRpt.FindBy(f => f.IsValid == false);
            entityDto = _mapper.Map<IEnumerable<sys_staff>, IEnumerable<SysStaffDto>>(users);

            var orgList = _orgRepository.GetAll().ToList();
            foreach (var item in entityDto)
            {
                item.OrgIdTxt = orgList.FirstOrDefault(f => f.Id == item.OrgId)?.DeptName;
            }
            return new OkObjectResult(entityDto.ToList());
        }

        // GET api/values/5
        [HttpGet("{EmployeeNo}")]
        public async Task<IActionResult> Get(string employeeNo)
        {
            var single = _sysStaffRpt.GetSingle(f => f.EmployeeNo == employeeNo);
            return new OkObjectResult(single);
        }
        // GET api/values/5
        [HttpGet("{empno}/{orgId}")]
        public async Task<IActionResult> GetStaffByOrgId(string empno,int orgId)
        {
            var employee = _sysStaffRpt.GetSingle(f => f.EmployeeNo == empno);
            if (employee != null)
            {
                employee.OrgId = orgId;
            }
            _sysStaffRpt.Commit();
            return new OkObjectResult(employee);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]sys_staff value)
        {
            using (var tran = _context.Database.BeginTransaction())
            {
                try
                {
                    var oldSysStaff = _sysStaffRpt.FindBy(f => f.EmployeeNo == value.EmployeeNo);
                    if (oldSysStaff.Any())
                    {
                        return BadRequest(string.Concat(value.EmployeeNo, "已经存在。"));
                    }
                    value.CreatedAt = DateTime.Now;
                    value.UpdatedAt = DateTime.Now;
                    value.IsValid = false;
                    if (User.Identity is ClaimsIdentity identity)
                    {
                        value.CreatedBy = identity.Name ?? "test";
                    }
                    _sysStaffRpt.Add(value);
                    _sysStaffRpt.Commit();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    tran.Rollback();
                    return BadRequest(ex);
                }
            }
            return new OkObjectResult(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]sys_staff value)
        {
            using (var tran = _context.Database.BeginTransaction())
            {
                try
                {
                    sys_staff userDb = _sysStaffRpt.GetSingle(id);
                    if (userDb == null)
                    {
                        return NotFound();
                    }
                    userDb.IsValid = value.IsValid;
                    userDb.Mobile = value.Mobile;
                    userDb.Tel = value.Tel;
                    userDb.Title = value.Title;
                    userDb.EmployeeNo = value.EmployeeNo;
                    userDb.Name = value.Name;
                    userDb.Tel = value.Tel;
                    userDb.Title = value.Title;
                    userDb.Address = value.Address;
                    userDb.WebChat = value.WebChat;
                    userDb.IDCard = value.IDCard;
                    userDb.UpdatedAt = DateTime.Now;
                    _sysStaffRpt.Commit();

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    tran.Rollback();
                    return BadRequest(ex);
                }
            }
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            sys_staff sysStaff = _sysStaffRpt.GetSingle(id);
            if (sysStaff == null)
            {
                return new NotFoundResult();
            }
            sysStaff.IsValid = false;
            _sysStaffRpt.Commit();
            return new NoContentResult();
        }
    }
}
