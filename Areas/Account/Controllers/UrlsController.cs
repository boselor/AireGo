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
        public async Task<IActionResult> Edit(int? id)
        {
            var ids = id ?? -1;
            if (ids <= 0) return RedirectToAction("Index");

            var res = await _sql.Select<UrlsMap>().Where(e => e.Id == id).FirstAsync();
            if (res == null)
                return Index();
            else
                ViewData["url_id"] = res.Id;

            return View();
        }
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
        public async Task<RespMap> GetMap(int? id)
        {
            var ids = id ?? -1;
            var resp = new RespMap();

            try
            {
                var res = await _sql.Select<UrlsMap>().Where(e => e.Id == id).FirstAsync();
                resp.Code = 200;
                resp.Message = "获取成功";
                resp.Data = res;
            }
            catch (Exception ex)
            {
                resp.Code = 500;
                resp.Message = ex.Message;
                resp.Data = ex;
            }
            return resp;
        }
        public async Task<RespMap> Push(int id, string title, string tag, string url,string posttime, string desc, string action = "add")
        {
            var resp = new RespMap();
            try
            {
                var res = 0;

                if (action.Equals("add"))
                {
                    var model = new UrlsMap()
                    {
                        Title = title,
                        Posttime = DateTime.Now.ToString("yyyy/MM/dd"),
                        Tag = tag,
                        Url = url,
                        Description = desc
                    };
                    res = await _sql
                        .Insert(model)
                        .ExecuteAffrowsAsync();

                    if (res > 0)
                    {
                        resp.Code = 200;
                        resp.Message = "添加成功";
                        resp.Data = res;
                    }
                    else
                    {
                        resp.Code = 200;
                        resp.Message = "添加失败";
                        resp.Data = res;
                    }
                }
                else if(action.Equals("edit"))
                {
                    var model = new UrlsMap()
                    {
                        Title = title,
                        Posttime = posttime,
                        Tag = tag,
                        Url = url,
                        Description = desc
                    };

                    res = await _sql
                        .Update<UrlsMap>()
                        .Where(e => e.Id == id)
                        .Set(e => e.Title, model.Title)
                        .Set(e => e.Url, model.Url)
                        .Set(e => e.Description, model.Description)
                        .ExecuteAffrowsAsync();

                    if (res > 0)
                    {
                        resp.Code = 200;
                        resp.Message = "更新成功";
                        resp.Data = res;
                    }
                    else
                    {
                        resp.Code = 200;
                        resp.Message = "更新失败";
                        resp.Data = res;
                    }
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
