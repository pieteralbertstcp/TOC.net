using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Repository.MySql;
using Services.MySql;

namespace WoodWorkingSA.Controllers
{
    public class membersController : Controller
    {

        // GET: members
        public ActionResult Index()
        {
            ViewBag.Members = new MembersService().GetAll().OrderBy(x=>x.first_name).ToList();
            return View();
        }


        [HttpGet]
        public JsonResult GetMemberData(string memberId)
        {
            if (!string.IsNullOrEmpty(memberId))
            {
                return Json(new { Success = true, body = new MembersService().Where(x => x.id == memberId).FirstOrDefault() }, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
    }
}
