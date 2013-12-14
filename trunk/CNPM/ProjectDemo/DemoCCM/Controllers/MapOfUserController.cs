using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoCCM.Models;

namespace DemoCCM.Controllers
{
    public class MapOfUserController : Controller
    {
        //
        // GET: /MapOfUser/

        ConceptMapDBContext db = new ConceptMapDBContext();

        public ActionResult Index(string username)
        { 
         
            List<MapOfUser> mapofUser  =db.MapOfUsers.Where(l=>l.UserName.Equals(username)).ToList();

            return View(mapofUser);
        }

        public ActionResult Load(int MapID)
        {
            String data = "";
            var lis = from lm in db.LinkOfMaps
                      join li in db.Links on lm.LinkID equals li.LinkID
                      join c1 in db.ConceptAlls on li.ConceptID1 equals c1.ConceptID
                      join c2 in db.ConceptAlls on li.ConceptID2 equals c2.ConceptID
                      where lm.MapID.Equals(MapID)
                      select new { ccID1=li.ConceptID1, cc1=c1.ConceptName , ccID2=li.ConceptID2 ,cc2=c2.ConceptName, li.LinkID, li.Text };
            foreach (var l in lis)
                data += l.cc1 + "|" + l.Text + "|" + l.cc2 + "|" + l.ccID1 + "|" + l.LinkID + "|" + l.ccID2 + "__";
            ViewBag.data = data;
            ViewBag.MapID = MapID;
            
            MapOfUser mapOfUser = db.MapOfUsers.FirstOrDefault(mu => mu.MapID.Equals(MapID));

            Map m = getMap(mapOfUser.LevelID,mapOfUser.ConceptID);
            ViewBag.levelID2 = mapOfUser.LevelID;
            ViewBag.conceptID = mapOfUser.ConceptID;
            ViewBag.UserName = Session["UserName"];

            ViewBag.mapName = mapOfUser.MapName;
            return View(m);
        }
        public ActionResult Delete(int mapID)
        {
            var lms = from l in db.LinkOfMaps
                      where l.MapID.Equals(mapID)
                      select l;
            foreach (LinkOfMap lm in lms.ToList())
            {
                db.LinkOfMaps.Remove(lm);
            }
            db.MapOfUsers.Remove(db.MapOfUsers.Single(m => m.MapID.Equals(mapID)));
            db.SaveChanges();
            return View();
        }
        [HttpPost]
        public ActionResult Save(Link[] links, String mapName, String LevelID, String MapID,String ConceptID)
        {
            //String userName = Session["UserName"].ToString();
            String userName = "ngamap";
            int mapID = int.Parse(MapID);
            MapOfUser mu = db.MapOfUsers.First(m => m.MapID.Equals(mapID));
            mu.MapName = mapName;
            mu.UserName = userName;
            mu.ConceptID = ConceptID;
            mu.LevelID = LevelID;
            var lms = from l in db.LinkOfMaps
                      where l.MapID.Equals(mapID)
                      select l;
            foreach (LinkOfMap lm in lms.ToList())
            {
                db.LinkOfMaps.Remove(lm);
            }
            for (int i = 0; i < links.Length; i++)
            {
                Link li = links[i];
                li.Text = db.Links.Find(li.LinkID).Text;
                if ((li = CheckLink(li)) != null)
                {
                    LinkOfMap lm = new LinkOfMap();
                    lm.LinkID = li.LinkID;
                    lm.MapID = mapID;
                    db.LinkOfMaps.Add(lm);
                }
            }
            db.SaveChanges();
            return View();
        }
        private Link CheckLink(Link link)
        {
            Link li = db.Links.FirstOrDefault(l => l.ConceptID1.Equals(link.ConceptID1) && l.ConceptID2.Equals(link.ConceptID2));
            if (li != null)
                if (li.Text.Equals(link.Text))
                    return li;
            return null;
        }
        public List<Link> listLink(String LevelID,String ConceptID)
        {
            List<Link> list = new List<Link>();
            var ids = from l in db.Links

                      join c in db.ConceptAlls on l.ConceptID2 equals c.ConceptID
                      join t in db.ConceptsForTopics on c.ConceptID equals t.ConceptID
                      where l.ConceptID1.Equals(ConceptID) && t.Levels.Contains(LevelID)
                      select l;
            list.AddRange(ids.ToList());
            foreach (var l in ids)
            {
                list.AddRange(listLink(LevelID,l.ConceptID2));
            }
            return list;
        }
        public Map getMap(String LevelID,String ConceptID)
        {
            Map m = new Map();
            List<Link> links = listLink(LevelID, ConceptID);
            var cons = from c in db.ConceptAlls
                       select c;
            m.Links = links;
            List<ConceptAll> concepts = cons.ToList();
            var result = from c in concepts
                         join l in links on c.ConceptID equals l.ConceptID2
                         select c;
            m.Concepts = result.ToList();
            m.Concepts.Insert(0, cons.Where(p => p.ConceptID.Equals(ConceptID)).First());
            return m;
        }
    }
}
