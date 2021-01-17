using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AireGo.Areas.Account.Controllers
{
    [Area(areaName: "Account"), Authorize]
    public class BaseController : Controller
    {
    }
}
