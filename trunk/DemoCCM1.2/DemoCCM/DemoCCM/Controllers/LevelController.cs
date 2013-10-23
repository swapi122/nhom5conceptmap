using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoCCM.Models;
namespace DemoCCM.Controllers
{
    public class LevelController : Controller
    {

        //
        // GET: /ChuDe/
        ConceptMapDBContext db = new ConceptMapDBContext();

        public ActionResult Topic_Level(String levelID1,String topicId)
        {
             //Lấy Level đưa wa bên  Topic/Index
            ViewBag.topicId1 = topicId;
            List<TopicOfLevel> topicOfLevel = db.TopicOfLevels.Where(p => p.Level.LevelID.Equals(levelID1)).ToList();

            TopicOfLevel tp = topicOfLevel.Find(p => p.LevelID.Equals(levelID1));
            if(tp!=null)
            ViewBag.name = tp.Level.LevelName;

            ViewBag.Title = "Level";
            return View(topicOfLevel);
        }

     
       

    }
}
