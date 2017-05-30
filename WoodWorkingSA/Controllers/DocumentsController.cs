using System;
using System.Linq;
using System.Web.Mvc;
using Services.MySql;

namespace WoodWorkingSA.Controllers
{
	[Authorize]
	public class DocumentsController : Controller
	{
		public ActionResult Index(string categoryId = null)
		{
			ViewBag.Files = null;
			if (categoryId != null)
			{
				ViewBag.Files = new DocumentsService().Where(x => x.document_categories.id == categoryId).OrderBy(p => p.filename).ToList();
			}

			ViewBag.FileCategories = new DocumentsCategoriesService().GetAll().OrderBy(x=>x.category_name).ToList();
			return View();
		}


		[HttpGet]
		public ActionResult DownloadFile(string fileId)
		{
			var document = new DocumentsService().Where(x => x.id == fileId).FirstOrDefault();
			if (document == null) return null;
			var cd = new System.Net.Mime.ContentDisposition
			{
				FileName = document.filename,
				Inline = false,
			};
			Response.AppendHeader("Content-Disposition", cd.ToString());
			return File(document.document, "application/octet-stream");
		}
	}

}