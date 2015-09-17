using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LL_Lab6.ViewModels;


namespace LL_Lab6.Controllers
{
    public class HomeController : Controller
    {
        Repo_Incident manager = new Repo_Incident();

        public ActionResult Index()
        {
            return View(manager.getListOfIncidentName());
        }


        public ActionResult About()
        {
            var path = System.IO.Path.Combine(Server.MapPath("~/App_Data/"), "q5.html");
            ViewBag.MyData = System.IO.File.ReadAllText(path);
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}