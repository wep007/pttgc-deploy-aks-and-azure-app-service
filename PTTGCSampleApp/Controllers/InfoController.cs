using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PTTGCSampleApp.Models;

namespace PTTGCSampleApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Info()
        {
            AppInfo info = new AppInfo();
            info.AppName = "user-service";
            info.Status = "OK";

            return new OkObjectResult(info);
        }
    }
}
