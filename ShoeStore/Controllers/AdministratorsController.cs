﻿namespace ShoeStore.Controllers
{
    using ShoeStore.Classes;
    using ShoeStore.Models;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    public class AdministratorsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Administrators
        public ActionResult Index()
        {
            var administrators = db.Administrators
                .Include(a => a.Colony)
                .Include(a => a.Municipality)
                .Include(a => a.State);
            return View(administrators.ToList());
        }

        // GET: Administrators/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrators.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        // GET: Administrators/Create
        public ActionResult Create()
        {
            ViewBag.IdColony = new SelectList(CombosHelper.GetColonies(0), "IdColony", "Description");
            ViewBag.IdMunicipality = new SelectList(CombosHelper.GetMunicipalities(0), "IdMunicipality", "Description");
            ViewBag.IdState = new SelectList(CombosHelper.GetStates(), "IdState", "Description");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                db.Administrators.Add(administrator);
                var response = DbHelper.SaveChanges(db);
                UsersHelper.CreateUserASP(administrator.Email, "Admin", administrator.Password);
                if (administrator.PhotoFile != null)
                {

                    var folder = "~/Content/Photo";
                    var file = string.Format("{0}.jpg", administrator.AdministratorId);
                    var responsefile = FileHelper.UploadPhoto(administrator.PhotoFile, folder, file);
                    if (responsefile)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        administrator.Photo = pic;
                        db.Entry(administrator).State = EntityState.Modified;
                        db.SaveChanges();

                    }

                }
                if (response.Successfully)
                {
                    return RedirectToAction("Index");

                }
                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.IdColony = new SelectList(CombosHelper.GetColonies(administrator.IdColony), "IdColony", "Description", administrator.IdColony);
            ViewBag.IdMunicipality = new SelectList(CombosHelper.GetMunicipalities(administrator.IdMunicipality), "IdMunicipality", "Description", administrator.IdMunicipality);
            ViewBag.IdState = new SelectList(CombosHelper.GetStates(), "IdState", "Description", administrator.IdState);
            return View(administrator);
        }

        // GET: Administrators/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrators.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdColony = new SelectList(CombosHelper.GetColonies(), "IdColony", "Description", administrator.IdColony);
            ViewBag.IdMunicipality = new SelectList(CombosHelper.GetMunicipalities(), "IdMunicipality", "Description", administrator.IdMunicipality);
            ViewBag.IdState = new SelectList(CombosHelper.GetStates(), "IdState", "Description", administrator.IdState);
            return View(administrator);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(administrator).State = EntityState.Modified;
                var response = DbHelper.SaveChanges(db);
                if (response.Successfully)
                {
                    return RedirectToAction("Index");

                }
                ModelState.AddModelError(string.Empty, response.Message);

            }

            ViewBag.IdColony = new SelectList(CombosHelper.GetColonies(administrator.IdColony), "IdColony", "Description", administrator.IdColony);
            ViewBag.IdMunicipality = new SelectList(CombosHelper.GetMunicipalities(administrator.IdMunicipality), "IdMunicipality", "Description", administrator.IdMunicipality);
            ViewBag.IdState = new SelectList(CombosHelper.GetStates(), "IdState", "Description", administrator.IdState);
            return View(administrator);
        }

        // GET: Administrators/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrators.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        // POST: Administrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Administrator administrator = db.Administrators.Find(id);
            db.Administrators.Remove(administrator);
            var response = DbHelper.SaveChanges(db);
            if (response.Successfully)
            {
                  return RedirectToAction("Index");
            }
          
            ModelState.AddModelError(string.Empty, response.Message);

            return View();
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