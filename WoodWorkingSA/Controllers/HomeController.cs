using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Services.MySql;
using WoodWorkingSA.Models;

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
