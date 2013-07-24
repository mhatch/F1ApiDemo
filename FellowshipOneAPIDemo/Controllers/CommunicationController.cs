using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FellowshipOneAPIDemo.Controllers
{
    public class CommunicationController : Controller
    {
        //
        // GET: /Communication/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Communication/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Communication/Create

        public ActionResult Create(int id)
        {
            ViewBag.PersonId = id;
            return View();
        }

        //
        // POST: /Communication/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Communication/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Communication/Edit/5

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
        // GET: /Communication/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Communication/Delete/5

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
