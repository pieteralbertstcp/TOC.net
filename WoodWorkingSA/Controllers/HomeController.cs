using System;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Services.MySql;

namespace WoodWorkingSA.Controllers
{
	public class HomeController : Controller
	{
		//
		// GET: /Home/

		public ActionResult Index(string message)
		{
			ViewBag.youtube = new YoutubeRecommendationsService().GetAll().OrderBy(x=>x.channel_name).ToList();
			ViewBag.Message = message;
			return View();
		}

		public ActionResult LoginResult(string username, string password, string returnUrl)
		{
			var result = new MembersService().IsValidMember(username, password);
			if (result != null)
			{
				Response.Cookies.Add(new HttpCookie("firstName", result.first_name));
				Response.Cookies.Add(new HttpCookie("isAdmin", result.is_admin.ToString()));
				Response.Cookies.Add(new HttpCookie("id", result.id));
				FormsAuthentication.SetAuthCookie(username, false);
				if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
					return Redirect(returnUrl);

				return RedirectToAction("Index", "Welcome");
			}
			return RedirectToAction("Index", "Home", new { message = "Authentication failure." });
		}
	}
}
