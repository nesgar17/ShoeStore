namespace ShoeStore.Controllers
{
    using PagedList;
    using ShoeStore.Classes;
    using ShoeStore.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    public class OwnersController : Controller
    {
        private DataContext db = new DataContext();

        #region MethodsController 
        public ActionResult Index(int? page)
        {
            int pageSize = 7;
            int pageNumber = (page ?? 1);

            var owners = db.Owners
                .Include(o => o.Colony)
                .Include(o => o.Municipality)
                .Include(o => o.State).ToList();
            return View(owners.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Owner owner = db.Owners.Find(id);
            if (owner == null)
            {
                return HttpNotFound();
            }
            return View(owner);
        }

        public ActionResult Create()
        {
            ViewBag.IdColony = new SelectList(CombosHelper.GetColonies(0), "IdColony", "Description");
            ViewBag.IdMunicipality = new SelectList(CombosHelper.GetMunicipalities(0), "IdMunicipality", "Description");
            ViewBag.IdState = new SelectList(CombosHelper.GetStates(), "IdState", "Description");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Owner owner)
        {
            if (ModelState.IsValid)
            {
                db.Owners.Add(owner);
                var response = DbHelper.SaveChanges(db);
                UsersHelper.CreateUserASP(owner.Email, "Admin", owner.Password);
                if (owner.PhotoFile != null)
                {

                    var folder = "~/Content/Photo";
                    var file = string.Format("{0}{1}.jpg", owner.OwnerId,owner.FirstName);
                    var responsefile = FileHelper.UploadPhoto(owner.PhotoFile, folder, file);
                    if (responsefile)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        owner.Photo = pic;
                        db.Entry(owner).State = EntityState.Modified;
                        RegisterUser(owner);
                        db.SaveChanges();

                    }

                }
                if (response.Successfully)
                {
                    return RedirectToAction("Message");

                }
                ModelState.AddModelError(string.Empty, response.Message);

            }

            ViewBag.IdColony = new SelectList(CombosHelper.GetColonies(owner.IdColony), "IdColony", "Description", owner.IdColony);
            ViewBag.IdMunicipality = new SelectList(CombosHelper.GetMunicipalities(owner.IdMunicipality), "IdMunicipality", "Description", owner.IdMunicipality);
            ViewBag.IdState = new SelectList(CombosHelper.GetStates(), "IdState", "Description", owner.IdState);
            return View(owner);
        }

        private void RegisterUser(Owner owner)
        {
            var user = new User
            {
                Address = owner.Address,
                BirthDay = owner.BirthDay,
                Email = owner.Email,
                FirstName = owner.FirstName,
                IdColony = owner.IdColony,
                IdMunicipality = owner.IdMunicipality,
                IdState = owner.IdState,
                LastName = owner.LastName,
                Password = owner.Password,
                Phone = owner.Phone,
                Photo = owner.Photo,

            };
            db.Users.Add(user);
            db.SaveChanges();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Owner owner = db.Owners.Find(id);
            if (owner == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdColony = new SelectList(CombosHelper.GetColonies(), "IdColony", "Description", owner.IdColony);
            ViewBag.IdMunicipality = new SelectList(CombosHelper.GetMunicipalities(), "IdMunicipality", "Description", owner.IdMunicipality);
            ViewBag.IdState = new SelectList(CombosHelper.GetStates(), "IdState", "Description", owner.IdState);
            return View(owner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Owner owner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(owner).State = EntityState.Modified;
               var response= DbHelper.SaveChanges(db);
                if (response.Successfully)
                {
                    return RedirectToAction("Index");

                }
                ModelState.AddModelError(string.Empty, response.Message);
            }
            ViewBag.IdColony = new SelectList(CombosHelper.GetColonies(owner.IdColony), "IdColony", "Description", owner.IdColony);
            ViewBag.IdMunicipality = new SelectList(CombosHelper.GetMunicipalities(owner.IdMunicipality), "IdMunicipality", "Description", owner.IdMunicipality);
            ViewBag.IdState = new SelectList(CombosHelper.GetStates(), "IdState", "Description", owner.IdState);
            return View(owner);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Owner owner = db.Owners.Find(id);
            if (owner == null)
            {
                return HttpNotFound();
            }
            return View(owner);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Owner owner = db.Owners.Find(id);
            db.Owners.Remove(owner);
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

        #endregion

        #region MethodsApp

        public ActionResult Message()
        {
            ViewBag.Message = "Tu Registro fue exitoso!!.";
            return View();
        }

        #endregion
    }
}
