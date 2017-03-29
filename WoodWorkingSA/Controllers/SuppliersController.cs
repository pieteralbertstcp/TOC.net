using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Repository.MySql;
using Services.MySql;

namespace WoodWorkingSA.Controllers
{
    public class SuppliersController : Controller
    {
        // GET: Suppliers
        public ActionResult Index()
        {
            ViewBag.suppliers = new SuppliersService().GetAll().ToList();
            ViewBag.youtube = new YoutubeRecommendationsService().GetAll().ToList();
            return View();
        }

        private static string GeneratePopupWindowText(suppliers supplier,string telephone)
        {
            return "<center><table>" +
                   "<tr><td><img style='width:200px;height:auto;' src=" + supplier.image_url + "></td></tr> <tr><td><br /></td></tr>" +
                   "<tr><td style='width:20px; word-wrap: break-word; '>" + supplier.description + "</td></tr> <tr><td><br /></td></tr>" +
                   "<tr><td><a target='_blank' href='" + supplier.url + "/'>" + supplier.url + "</td></tr></table></center> <tr><td><br /></td></tr>" +
                   "<tr><td style='width:20px;  word-wrap: break-word; '><center><b>" + telephone + "</b></center></td></tr>";
        }

        [System.Web.Mvc.HttpGet]
        public JsonResult GetPointerData()
        {
            var points = new List<dynamic>();
            using (var service = new SuppliersService())
            {
                foreach (var sl in service.GetAll())
                {
                    using (var service2 = new SuppliersLocationsService())
                    {
                        foreach (var i in service2.Where(x => x.supplier_id == sl.id))
                        {
                            dynamic temp = new ExpandoObject();
                            temp.popupWindowValue = GeneratePopupWindowText(sl, i.telephone_number);
                            temp.lat = i.lat;
                            temp.lng = i.lng;
                            temp.markerColor = sl.supplier_marker_color;
                            points.Add(JsonConvert.SerializeObject(temp));
                        }
                    }
                }
            }
            return Json(new { Success = true, body = points }, JsonRequestBehavior.AllowGet);
        }
    }
}