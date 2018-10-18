using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoeStore.Models;

namespace ShoeStore.Controllers
{
    public class ColoniesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Colonies
        public ActionResult Index()
        {
            var colonies = db.Colonies.Include(c => c.Municipality);
            return View(colonies.ToList());
        }

        // GET: Colonies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colony colony = db.Colonies.Find(id);
            if (colony == null)
            {
                return HttpNotFound();
            }
            return View(colony);
        }

        // GET: Colonies/Create
        public ActionResult Create()
        {
            ViewBag.IdMunicipality = new SelectList(db.Municipalities, "IdMunicipality", "Description");
            return View();
        }

        // POST: Colonies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdColony,Description,IdMunicipality")] Colony colony)
        {
            if (ModelState.IsValid)
            {
                db.Colonies.Add(colony);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdMunicipality = new SelectList(db.Municipalities, "IdMunicipality", "Description", colony.IdMunicipality);
            return View(colony);
        }

        // GET: Colonies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colony colony = db.Colonies.Find(id);
            if (colony == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMunicipality = new SelectList(db.Municipalities, "IdMunicipality", "Description", colony.IdMunicipality);
            return View(colony);
        }

        // POST: Colonies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdColony,Description,IdMunicipality")] Colony colony)
        {
            if (ModelState.IsValid)
            {
                db.Entry(colony).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMunicipality = new SelectList(db.Municipalities, "IdMunicipality", "Description", colony.IdMunicipality);
            return View(colony);
        }

        // GET: Colonies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colony colony = db.Colonies.Find(id);
            if (colony == null)
            {
                return HttpNotFound();
            }
            return View(colony);
        }

        // POST: Colonies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Colony colony = db.Colonies.Find(id);
            db.Colonies.Remove(colony);
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
