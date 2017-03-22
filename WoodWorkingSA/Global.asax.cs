using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Repository.MySql;
using WoodWorkingSA.Models;

namespace WoodWorkingSA
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

           // Mapper.Initialize(cfg => cfg.CreateMap<youtube_recommendations, YoutubeRecommendationsModel>());  
            Mapper.Initialize(cfg => cfg.CreateMap<suppliers, SuppliersModel>());
            //Mapper.Initialize(cfg => cfg.CreateMap<YoutubeRecommendationsModel, youtube_recommendations>());
        }
    }
}