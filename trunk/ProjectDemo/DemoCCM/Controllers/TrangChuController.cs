using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoCCM.Models;

namespace DemoCCM.Controllers
{
    public class TrangChuController : Controller
    {
        //
        // GET: /TrangChu/
        ConceptMapDBContext db = new ConceptMapDBContext();
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "About";
            return View();
        }
        public ActionResult _TrinhDoPartial()
        {
            List<Level> level = db.Levels.ToList();

            ViewBag.Message = "Trình Độ";
            return PartialView(level);
        }

       

    }
}
