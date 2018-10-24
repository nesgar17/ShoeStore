
namespace ShoeStore.Controllers
{
    using PagedList;
    using ShoeStore.Models;
    using System.Linq;
    using System.Web.Mvc;

    public class BitacorasController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult Index(int? page)
        {

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            var bitacora = db.Bitacoras.ToList();

            return View(bitacora.ToPagedList(pageNumber, pageSize));
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}