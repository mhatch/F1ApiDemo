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

            if (model.FirstName == null && model.LastName == null)
            //if (ModelState.IsValid)
            {
                ModelState.AddModelError("", "Enter a first or last name or initial(s)");
                return View();
            }
            else
            {
                // create api helper 
                ApiHelper helper = new ApiHelper();

                // execute search
                var results = helper.Search(model);
                ViewBag.Count = results.Count();

                // for return link
                ViewBag.FirstName = model.FirstName;
                ViewBag.LastName = model.LastName;

                return View(results);
            }
        }
        
        
    }
}
