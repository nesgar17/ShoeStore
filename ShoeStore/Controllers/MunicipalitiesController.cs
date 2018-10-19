namespace ShoeStore.Controllers
{
    using ShoeStore.Classes;
    using ShoeStore.Models;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using PagedList;

    public class MunicipalitiesController : Controller
    {


        private DataContext db = new DataContext();

        // GET: Municipalities
        public ActionResult Index(int? page)
        {

            int pageSize = 7;
            int pageNumber = (page ?? 1);
            var municipalities = db.Municipalities.Include(m => m.State).ToList();
            return View(municipalities.ToPagedList(pageNumber, pageSize));
        }

        // GET: Municipalities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipality municipality = db.Municipalities.Find(id);
            if (municipality == null)
            {
                return HttpNotFound();
            }
            return View(municipality);
        }

        // GET: Municipalities/Create
        public ActionResult Create()
        {
            ViewBag.IdState = new SelectList(CombosHelper.GetStates(), "IdState", "Description");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Municipality municipality)
        {
            if (ModelState.IsValid)
            {
                db.Municipalities.Add(municipality);
               var response= DbHelper.SaveChanges(db);
                if (response.Successfully)
                {
                return RedirectToAction("Index");

                }
                ModelState.AddModelError(string.Empty, response.Message);

            }

            ViewBag.IdState = new SelectList(CombosHelper.GetStates(), "IdState", "Description", municipality.IdState);
            return View(municipality);
        }

        // GET: Municipalities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipality municipality = db.Municipalities.Find(id);
            if (municipality == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdState = new SelectList(CombosHelper.GetStates(), "IdState", "Description", municipality.IdState);
            return View(municipality);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Municipality municipality)
        {
            if (ModelState.IsValid)
            {
                db.Entry(municipality).State = EntityState.Modified;
                var response =DbHelper.SaveChanges(db);
                if (response.Successfully)
                {
                    return RedirectToAction("Index");

                }
                ModelState.AddModelError(string.Empty, response.Message);
            }
            ViewBag.IdState = new SelectList(CombosHelper.GetStates(), "IdState", "Description", municipality.IdState);
            return View(municipality);
        }

        // GET: Municipalities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipality municipality = db.Municipalities.Find(id);
            if (municipality == null)
            {
                return HttpNotFound();
            }
            return View(municipality);
        }

        // POST: Municipalities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Municipality municipality = db.Municipalities.Find(id);
            db.Municipalities.Remove(municipality);
            var response  = DbHelper.SaveChanges(db);
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
