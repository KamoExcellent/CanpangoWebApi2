using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Caching;


namespace CanpangoWebApi.Controllers
{
    public class HomeController : Controller
    {
        //Load data into memory Cache


        //private IMemoryCache _cache;

        //public HomeController(IMemoryCache memoryCache)
        //{
        //    _cache = memoryCache;
        //}


        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
