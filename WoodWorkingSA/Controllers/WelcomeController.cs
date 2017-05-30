using System;
using System.Web.Mvc;
using Services.MySql;
using System.Linq;
using System.Web.Configuration;
using System.Web.Security;

namespace WoodWorkingSA.Controllers
{
	[Authorize]
	public class WelcomeController : Controller
	{

		// GET: Welcome
		public ActionResult Index()
		{
			var service = new MeetingsService();
			var firstNameCookie = Request.Cookies["firstName"];
			var isAdminCookie = Request.Cookies["isAdmin"];
			var id = Request.Cookies["id"];

			if (firstNameCookie != null && isAdminCookie != null && id != null)
			{
				ViewBag.firstName = firstNameCookie.Value;
				ViewBag.isAdmin = bool.Parse(isAdminCookie.Value);
				ViewBag.idCookie = id.Value;
			}

			var nextMeetingDate = service.GetNextMeetingDate();
			ViewBag.NextMeetingDate = nextMeetingDate.ToString("dd MMMM yyyy");
			ViewBag.HasRSVPed = service.IsUserAttendingMeetingForProvidedDate(nextMeetingDate, id.Value);
			ViewBag.youtube = new YoutubeRecommendationsService().GetAll().OrderBy(x => x.channel_name).ToList();
			ViewBag.CountOfAtendees = new MeetingsRsvpService().CountOfMembersRsvpForMeeting(nextMeetingDate);
			ViewBag.Title = "Welcome Back";
			return View();
		}

		[HttpPost]
		public ActionResult Rsvp(bool isAttending = false, bool isBringingAFriend = false)
		{
			if (!isAttending || Request.Cookies["id"] == null) return Index();
			var nextMeetingDate = new MeetingsService().GetNextMeetingDate();
			new MeetingsRsvpService().RvsPforMeeting(Request.Cookies["id"].Value, nextMeetingDate, isAttending, isBringingAFriend);
			return RedirectToAction("Index", "Welcome");
		}
	}
}