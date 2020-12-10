using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ApiV2.Common;
using ApiV2.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceContext.Interface;

namespace ApiV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserService _svc;
        public UserController(IUserService svc)
        {
            _svc = svc;
        }

        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                long PageIndex = 0;
                long PageSize = -1;
                string err = string.Empty;
                long totalrows = 0;
                var result = _svc.GetList(ref err, ref totalrows, PageIndex, PageSize, "", "");
                return Json(new JsonResultData(result, err, totalrows, string.IsNullOrEmpty(err) ? true : false));

            }
            catch (Exception ex)
            {
                return Json(ErrorHelper.Current.ErrorValidateDataString(ex.ToString()));
            }
        }

        [HttpPost]
        public JsonResult Post([FromForm] SearchViewModel data)
        {
            try
            {
                long PageIndex = 0;
                long PageSize = -1;
                long.TryParse(data.PageIndex, out PageIndex);
                long.TryParse(data.PageSize, out PageSize);
                string err = string.Empty;
                long totalrows = 0;
                var result = _svc.GetList(ref err, ref totalrows, PageIndex, PageSize, data.OrderBy, data.FilterQuery);
                return Json(new JsonResultData(result, err, totalrows));

            }
            catch (Exception ex)
            {
                return Json(ErrorHelper.Current.ErrorValidateDataString(ex.ToString()));
            }
        }


    }
}