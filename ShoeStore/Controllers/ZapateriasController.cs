namespace ShoeStore.Controllers
{
    using ShoeStore.Classes;
    using ShoeStore.Models;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    
    public class ZapateriasController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Zapaterias
        public ActionResult Index()
        {
            var user = db.Owners.Where(u => u.Email == User.Identity.Name).FirstOrDefault();

            var zapaterias = db.Zapaterias
                .Include(z => z.Colony)
                .Include(z => z.Municipality)
                .Include(z => z.State).Where(z => z.OwnerId == user.OwnerId);

           return View(zapaterias.ToList());
        }

        // GET: Zapaterias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zapateria zapateria = db.Zapaterias.Find(id);
            if (zapateria == null)
            {
                return HttpNotFound();
            }
            return View(zapateria);
        }

        // GET: Zapaterias/Create
        public ActionResult Create()
        {
            var user = db.Owners.Where(u => u.Email == User.Identity.Name).FirstOrDefault();

            ViewBag.IdColony = new SelectList(CombosHelper.GetColonies(0), "IdColony", "Description");
            ViewBag.IdMunicipality = new SelectList(CombosHelper.GetMunicipalities(0), "IdMunicipality", "Description");
            ViewBag.IdState = new SelectList(CombosHelper.GetStates(), "IdState", "Description");
            var zapateria = new Zapateria { OwnerId = user.OwnerId };

            return View(zapateria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Zapateria zapateria)
        {
            var user = db.Owners.Where(u => u.Email == User.Identity.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                zapateria.OwnerId = user.OwnerId;
                db.Zapaterias.Add(zapateria);
                var response = DbHelper.SaveChanges(db);
                if (zapateria.LogoFile != null)
                {

                    var folder = "~/Content/Logos";
                    var file = string.Format("{0}{1}.jpg", zapateria.ZapateriaId,zapateria.Name);
                    var responsefile = FileHelper.UploadPhoto(zapateria.LogoFile, folder, file);
                    if (responsefile)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        zapateria.Logo = pic;
                        db.Entry(zapateria).State = EntityState.Modified;
                        db.SaveChanges();

                    }

                }
                if (response.Successfully)
                {
                    DbHelper.InsertBitacora("Create", "Zapaterias", User.Identity.Name, db);
                    return RedirectToAction("Index");

                }
                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.IdColony = new SelectList(CombosHelper.GetColonies(zapateria.IdColony), "IdColony", "Description", zapateria.IdColony);
            ViewBag.IdMunicipality = new SelectList(CombosHelper.GetMunicipalities(zapateria.IdMunicipality), "IdMunicipality", "Description", zapateria.IdMunicipality);
            ViewBag.IdState = new SelectList(CombosHelper.GetStates(), "IdState", "Description", zapateria.IdState);
            return View(zapateria);
        }

        // GET: Zapaterias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zapateria zapateria = db.Zapaterias.Find(id);
            if (zapateria == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdColony = new SelectList(CombosHelper.GetColonies(), "IdColony", "Description", zapateria.IdColony);
            ViewBag.IdMunicipality = new SelectList(CombosHelper.GetMunicipalities(), "IdMunicipality", "Description", zapateria.IdMunicipality);
            ViewBag.IdState = new SelectList(CombosHelper.GetStates(), "IdState", "Description", zapateria.IdState);
            return View(zapateria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Zapateria zapateria)
        {
            var user = db.Owners.Where(u => u.Email == User.Identity.Name).FirstOrDefault();


            if (ModelState.IsValid)
            {
                zapateria.OwnerId = user.OwnerId;
                db.Entry(zapateria).State = EntityState.Modified;
                var response = DbHelper.SaveChanges(db);
                if (zapateria.LogoFile != null)
                {

                    var folder = "~/Content/Logos";
                    var file = string.Format("{0}{1}.jpg", zapateria.ZapateriaId, zapateria.Name);
                    var responsefile = FileHelper.UploadPhoto(zapateria.LogoFile, folder, file);
                    if (responsefile)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        zapateria.Logo = pic;
                        db.Entry(zapateria).State = EntityState.Modified;
                        var res = DbHelper.SaveChanges(db);
                        if (res.Successfully)
                        {

                        }else
                        {
                            ModelState.AddModelError(string.Empty, response.Message);

                        }

                    }

                }
                if (response.Successfully)
                {
                    return RedirectToAction("Index");

                }
                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.IdColony = new SelectList(CombosHelper.GetColonies(zapateria.IdColony), "IdColony", "Description", zapateria.IdColony);
            ViewBag.IdMunicipality = new SelectList(CombosHelper.GetMunicipalities(zapateria.IdMunicipality), "IdMunicipality", "Description", zapateria.IdMunicipality);
            ViewBag.IdState = new SelectList(CombosHelper.GetStates(), "IdState", "Description", zapateria.IdState);
            return View(zapateria);
        }

        // GET: Zapaterias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var zapateria = db.Zapaterias.Find(id);
            if (zapateria == null)
            {
                return HttpNotFound();
            }
            return View(zapateria);
        }

        // POST: Zapaterias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zapateria zapateria = db.Zapaterias.Find(id);
            db.Zapaterias.Remove(zapateria);
            var response = DbHelper.SaveChanges(db);
            if (response.Successfully)
            {
                return RedirectToAction("Index");
            }
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
