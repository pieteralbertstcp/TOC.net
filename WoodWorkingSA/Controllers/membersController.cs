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
                var member = new MembersService().Where(x => x.id == memberId).FirstOrDefault();
                if (member == null) return null;
                dynamic membr = new ExpandoObject();
                membr.first_name = member.first_name;
                membr.last_name = member.last_name;
                membr.email = member.email;
                membr.cell = member.cell;
                membr.profile_image_base_64 = Convert.ToBase64String(member.profile_image);
                membr.lat = member.lat;
                membr.lng = member.lng;
                membr.address = member.address;
                membr.started_woodworking_on_year = member.started_woodworking_on.ToString("yyyy");

                return Json(new { Success = true, body = JsonConvert.SerializeObject(membr) }, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
    }
}
