using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FellowshipOneAPIDemo.Models;
using FellowshipOneAPIDemo.Helpers;

namespace FellowshipOneAPIDemo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Person model)
        {

            if (ModelState.IsValid)
            {
                // create api helper 
                ApiHelper helper = new ApiHelper();

                // execute search
                var results = helper.Search(model);
                ViewBag.Count = results.Count();

                return View(results);
            }
            else
            {
                ModelState.AddModelError("", "Both fields are required");
                return View();
            }
        }
        
        
    }
}
