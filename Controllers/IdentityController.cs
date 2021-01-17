using AireGo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AireGo.Controllers
{
    public class IdentityController : Controller
    {
        public IActionResult Index(string returnUrl)
        {
            ViewData["returnurl"] = returnUrl;
            return View();
        }
        public IActionResult Login(string returnUrl) => Index(returnUrl);
        private IFreeSql _sql;
        public IdentityController(IFreeSql sql) => _sql = sql;
        public async Task<RespMap> Push(string name, string passwd)
        {
            var resp = new RespMap();

            try
            {
                var usr = await _sql.Select<UserMap>().Where(e => e.Name.Equals(name.Trim()) && e.Passwd.Equals(passwd.Trim())).FirstAsync();
                if(usr == null)
                {
                    resp.Code = 404;
                    resp.Message = "未找到信息";
                    resp.Data = null;
                }
                else
                {
                    var claims = new List<Claim>
                    {
                        new Claim("id", usr.Id.ToString()),
                        new Claim(ClaimTypes.Name, usr.Name),
                        new Claim(ClaimTypes.Email,usr.Email),
                    };
                    await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)));

                    resp.Code = 200;
                    resp.Message = "登录成功";
                    resp.Data = usr;
                }
            }
            catch (Exception ex)
            {
                resp.Code = 500;
                resp.Message = ex.Message;
                resp.Data = ex;
            }

            return resp;
        }
    }
}
