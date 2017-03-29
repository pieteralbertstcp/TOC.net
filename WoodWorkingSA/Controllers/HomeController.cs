using System.Linq;
using System.Web.Mvc;
using Services.MySql;

namespace WoodWorkingSA.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.youtube = new YoutubeRecommendationsService().GetAll().ToList();       
           return View();
        }

    }
}
