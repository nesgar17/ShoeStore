namespace ShoeStore.Controllers
{
    using ShoeStore.Models;
    using System.Linq;
    using System.Web.Mvc;

    public class GenericController : Controller
    {
        private DataContext db = new DataContext();

        public JsonResult GetMunicipalities(int idState)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var municipalities = db.Municipalities.Where(e => e.IdState == idState);
            return Json(municipalities);
        }

        public JsonResult GetColonies(int idMunicipality)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var colonies = db.Colonies.Where(e => e.IdMunicipality == idMunicipality);
            return Json(colonies);
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