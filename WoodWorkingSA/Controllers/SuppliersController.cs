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
    public class SuppliersController : Controller
    {
        // GET: Suppliers
        public ActionResult Index()
        {
            ViewBag.suppliers = new SuppliersService().GetAll().ToList();
            return View();
        }
    }
}