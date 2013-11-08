using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoCCM.Models;

namespace DemoCCM.Controllers
{
    public class TopicController : Controller
    {
        //
        // GET: /ChuDe/

        ConceptMapDBContext db = new ConceptMapDBContext();
        // cai nay la get
        // ta lam them 1 cai post
        public ActionResult Index(String LevelID1, String topicId1)
        {
            List<ConceptsForTopic> ct = 
                db.ConceptsForTopics.Where(p => p.TopicID.Equals(topicId1) && p.Levels.Contains(LevelID1)).ToList();
            ViewBag.cd = new SelectList(ct, "ConceptID", "Question");//tham số thứ chứa Field load lên
            ViewBag.levelID2 = LevelID1;
            ViewBag.topicID2 = topicId1;
            return View();

        }
        public List<Link> listLink(String LevelID, String topicId1, string ConceptID)
        {
            List<Link> list=new List<Link>();
            var ids = from l in db.Links
                      join c in db.ConceptAlls on l.ConceptID2 equals c.ConceptID
                      join t in db.ConceptsForTopics on c.ConceptID equals t.ConceptID
                      where l.ConceptID1.Equals(ConceptID) && t.Levels.Contains(LevelID) && t.TopicID.Equals(topicId1)
                      select l;
            list.AddRange(ids.ToList());
            foreach (var l in ids)
            {
                list.AddRange(listLink(LevelID,topicId1,l.ConceptID2));
            }
            return list;
        }
        //Lấy map truyền dữ liệu vào
        public Map getMap(String LevelID, String topicId1, string ConceptID)
        {
            Map m = new Map();
            List<Link> links = listLink(LevelID,topicId1,ConceptID);
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
        //dùng để test thử
        public ActionResult check()
        {
            Map m = new Map();
            List<Link> links = listLink("LV4", "TP3", "1");
            var cons = from c in db.ConceptAlls
                       select c;
            m.Links = links;
            List<ConceptAll> concepts= cons.ToList();
            var result = from c in concepts
                         join l in links on c.ConceptID equals l.ConceptID2
                         select c;
            m.Concepts = result.ToList();
            m.Concepts.Insert(0,cons.Where(p => p.ConceptID.Equals("1")).First());
            return View(m);
        }
        [HttpPost, ActionName("Index")]
        public ActionResult Indexzz(String LevelID1, String topicId1, string ConceptID)
        {
            ViewBag.levelID2 = LevelID1;
            ViewBag.topicID2 = topicId1;
            //ViewBag.conceptID = ConceptID;

            //List<ConceptsForTopic> ct =  listLink("LV3", "TP3", ConceptID))

  
            return View("Index");

        }
        //Làm tương tự như Khái Niệm 
        public PartialViewResult _LinkOfTopicPartial(List<ConceptsForTopic> concept)
        {
            //Làm tương tự như Khái Niệm 


            List<Link> links = new List<Link>();

            var ids = from a in concept
                      select a.ConceptID;

            foreach (var id in ids)
            {
                var link = from p in db.Links
                           where p.ConceptID1.Equals(id) 
                           select p;
                foreach (Link i in link)
                {
                    links.Add(i);
                }
            }

            return PartialView(links);
        }

        public PartialViewResult _Concept_Topic_LevelPartial(String LevelID, String TopicID)
        {
            List<ConceptsForTopic> conceptForTopics;
            conceptForTopics = db.ConceptsForTopics.Where(p=>p.TopicID.Equals(TopicID)).ToList();
            List<ConceptsForTopic> conceptForTopics2 = new List<ConceptsForTopic>();
            ConceptsForTopic concept = new ConceptsForTopic();

            foreach (var k in conceptForTopics)
            {
                string[] catchuoi = k.Levels.Split(new char[] { ',' });
                foreach (string s1 in catchuoi)
                {

                    if (s1.Trim() != "")
                    {
                        if (s1.ToString().Equals(LevelID))
                        {

                            concept = conceptForTopics.Find(p => p.ConceptID.Equals(k.ConceptID));
                            conceptForTopics2.Add(concept);
                            
                        }
                    }

                }

            }

            return PartialView(conceptForTopics2);
        }


         public PartialViewResult dropdownQuestion ( string idQues)
         {
             ViewBag.a = idQues;
             return PartialView();
         }
    }
}
