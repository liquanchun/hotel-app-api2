using System;
//using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Hotel.App.Data;
using Hotel.App.Data.Abstract;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Hotel.App.Model.SYS;
using Microsoft.AspNetCore.Http;

namespace Hotel.App.API2.Controllers
{
    [Route("api/[controller]")]
    public class TokenAuthController : Controller
    {
        private readonly ISysUserRepository _sysUserRpt;
        public TokenAuthController(ISysUserRepository sysUserRpt)
        {
            _sysUserRpt = sysUserRpt;
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody]sys_user user)
        {
            //User existUser = UserStorage.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            var existUser =
                _sysUserRpt.GetSingle(f => f.UserId == user.UserId && f.Pwd == user.Pwd);
            if (existUser != null)
            {
                existUser.LastLoginTime = DateTime.Now;
                _sysUserRpt.Commit();

                var requestAt = DateTime.Now;
                var expiresIn = requestAt + TokenAuthOption.ExpiresSpan;
                var token = GenerateToken(existUser, expiresIn);

                return Json(new RequestResult
                {
                    State = RequestState.Success,
                    Data = new
                    {
                        requertAt = requestAt,
                        expiresIn = TokenAuthOption.ExpiresSpan.TotalSeconds,
                        tokeyType = TokenAuthOption.TokenType,
                        accessToken = token
                    }
                });
            }
            return Json(new RequestResult
            {
                State = RequestState.Failed,
                Msg = "用户名或密码错误。"
            });
        }

        private string GenerateToken(sys_user user, DateTime expires)
        {
            var handler = new JwtSecurityTokenHandler();

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.UserId, "TokenAuth"),
                new[] { new Claim("ID", user.UserId)}
            );

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = TokenAuthOption.Issuer,
                Audience = TokenAuthOption.Audience,
                SigningCredentials = TokenAuthOption.SigningCredentials,
                Subject = identity,
                Expires = expires
            });
            return handler.WriteToken(securityToken);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public IActionResult CheckSession()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                return new NoContentResult();
            }
            return new OkObjectResult(new { data = claimsIdentity.Name });
        }
    }
}

