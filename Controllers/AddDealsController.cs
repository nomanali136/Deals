using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Deals;

namespace Deals.Controllers
{
    public class AddDealsController : Controller
    {
        private POSEntities db = new POSEntities();

        // GET: AddDeals
        public ActionResult Index()
        {
            var addDeals = db.AddDeals.Include(a => a.deal).Include(a => a.item);
            return View(addDeals.ToList());
        }

        // GET: AddDeals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddDeal addDeal = db.AddDeals.Find(id);
            if (addDeal == null)
            {
                return HttpNotFound();
            }
            return View(addDeal);
        }

        // GET: AddDeals/Create
        public ActionResult Create()
        {
            ViewBag.d_id = new SelectList(db.deals, "d_id", "dname");
            ViewBag.it_id = new SelectList(db.items, "it_id", "pname");
            return View();
        }

        // POST: AddDeals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,d_id,it_id")] AddDeal addDeal)
        {
            if (ModelState.IsValid)
            {
                db.AddDeals.Add(addDeal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.d_id = new SelectList(db.deals, "d_id", "dname", addDeal.d_id);
            ViewBag.it_id = new SelectList(db.items, "it_id", "pname", addDeal.it_id);
            return View(addDeal);
        }

        // GET: AddDeals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddDeal addDeal = db.AddDeals.Find(id);
            if (addDeal == null)
            {
                return HttpNotFound();
            }
            ViewBag.d_id = new SelectList(db.deals, "d_id", "dname", addDeal.d_id);
            ViewBag.it_id = new SelectList(db.items, "it_id", "pname", addDeal.it_id);
            return View(addDeal);
        }

        // POST: AddDeals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,d_id,it_id")] AddDeal addDeal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(addDeal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.d_id = new SelectList(db.deals, "d_id", "dname", addDeal.d_id);
            ViewBag.it_id = new SelectList(db.items, "it_id", "pname", addDeal.it_id);
            return View(addDeal);
        }

        // GET: AddDeals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddDeal addDeal = db.AddDeals.Find(id);
            if (addDeal == null)
            {
                return HttpNotFound();
            }
            return View(addDeal);
        }

        // POST: AddDeals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AddDeal addDeal = db.AddDeals.Find(id);
            db.AddDeals.Remove(addDeal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
