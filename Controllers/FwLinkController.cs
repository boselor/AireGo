using AireGo.Areas.Account.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AireGo.Controllers
{
    public class FwLinkController : Controller
    {
        private IFreeSql _sql;
        public FwLinkController(IFreeSql sql) => _sql = sql;
        public async Task<IActionResult> Index(int? id)
        {
            var map = await _sql.Select<UrlsMap>().Where(e => e.Id == id).FirstAsync();
            if (map == null)
                return RedirectToAction("Error");
            else
                return Redirect(map.Url);
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
