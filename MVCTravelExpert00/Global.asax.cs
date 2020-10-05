using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MVCTravelExpert00.Models;

namespace MVCTravelExpert00
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var confg = new MapperConfiguration(cfg =>
              {
                  cfg.AddProfile<MappingOrderhistory>();
                  //cfg.CreateMap<OrderHistoryViewModel, OrderHistory>();
              });
            //var mapper = confg.CreateMapper();
        }
    }
}
