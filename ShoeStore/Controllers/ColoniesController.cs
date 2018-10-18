namespace ShoeStore.Controllers
{
    using ShoeStore.Classes;
    using ShoeStore.Models;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;


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
            ViewBag.IdMunicipality = new SelectList(CombosHelper.GetMunicipalities(), "IdMunicipality", "Description");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Colony colony)
        {
            if (ModelState.IsValid)
            {
                db.Colonies.Add(colony);
                var response= DbHelper.SaveChanges(db);
                if (response.Successfully)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, response.Message);
               
            }

            ViewBag.IdMunicipality = new SelectList(CombosHelper.GetMunicipalities(), "IdMunicipality", "Description", colony.IdMunicipality);
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
            ViewBag.IdMunicipality = new SelectList(CombosHelper.GetMunicipalities(), "IdMunicipality", "Description", colony.IdMunicipality);
            return View(colony);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Colony colony)
        {
            if (ModelState.IsValid)
            {
                db.Entry(colony).State = EntityState.Modified;
                var response = DbHelper.SaveChanges(db);
                if (response.Successfully)
                {

                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, response.Message);
            }
            ViewBag.IdMunicipality = new SelectList(CombosHelper.GetMunicipalities(), "IdMunicipality", "Description", colony.IdMunicipality);
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
            var response=DbHelper.SaveChanges(db);
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
