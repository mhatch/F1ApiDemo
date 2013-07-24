using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using System.IO;
using FellowshipOneAPIDemo.Models;
using FellowshipOneAPIDemo.Helpers;

namespace FellowshipOneAPIDemo.Controllers
{
    public class PersonController : Controller
    {
        //
        //// GET: /Person/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        ////
        //// GET: /Person/Details/5

        public ActionResult Address(int id)
        {
            ViewBag.ID = id;
            ApiHelper helper = new ApiHelper();
            Person person = helper.GetPerson(id);
            ViewBag.Name = String.Format("{0} {1}", person.FirstName, person.LastName);
            ViewBag.HHID = person.HouseholdId;

            var addresses = helper.GetAddresses(id);
            ViewBag.Count = addresses.Count();

            return View(addresses);
        }
        
        
        public ActionResult Details(int id)
        {
            ViewBag.ID = id;
            return View();
        }


        public ActionResult Household(int id)
        {
            ViewBag.ID = id;

            ApiHelper helper = new ApiHelper();

            return View(helper.GetHousehold(id));
        }


        public ActionResult Communications(int id)
        {
            ViewBag.ID = id;
            ApiHelper helper = new ApiHelper();
            Person person = helper.GetPerson(id);
            ViewBag.Name = String.Format("{0} {1}", person.FirstName, person.LastName);

            var communications = helper.GetCommunications(id);

            ViewBag.Count = communications.Count();

            return View(communications);

        }

    }
}
