using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FellowshipOneAPIDemo.Helpers;
using FellowshipOneAPIDemo.Models;

namespace FellowshipOneAPIDemo.Controllers
{
    public class AddressController : Controller
    {
        //
        // GET: /Address/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Address/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Address/Create

        public ActionResult Create(int id)
        {
            ViewBag.HHID = id;
            return View();
        }

        //
        // POST: /Address/Create

        [HttpPost]
        public ActionResult Create(Address model, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var helper = new ApiHelper();

                try
                {
                    model.HouseholdId = Convert.ToInt32(collection["household_id"]);
                }
                catch
                {
                    model.HouseholdId = 0;
                }

                helper.CreateAddress(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Address/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Address/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Address/Delete/5

        public ActionResult Delete(int id, int person_id)
        {
            var helper = new ApiHelper();

            var response = helper.DeleteAddress(id);
            
            
            return View();
        }

        //
        // POST: /Address/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
