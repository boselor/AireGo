using AireGo.Areas.Account.Models;
using AireGo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AireGo.Areas.Account.Controllers
{
    public class UrlsController : BaseController
    {
        private IFreeSql _sql;
        public UrlsController(IFreeSql sql) => _sql = sql;
        public IActionResult Index() => View();
        public IActionResult Add() => View();

        public async Task<RespMap> Get(int page, int size)
        {
            var resp = new RespMap();

            try
            {
                var list = await _sql.Select<UrlsMap>()
                    .Skip((page - 1) * size).Take(size)
                    .ToListAsync();

                resp.Code = 200;
                resp.Message = "处理成功";
                resp.Data = list;
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
