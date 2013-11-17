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

      
        public PartialViewResult dropdown(String LevelID, String topicId1)
        {
            List<ConceptsForTopic> ct =
                db.ConceptsForTopics.Where(p => p.TopicID.Equals(topicId1) && p.Levels.Contains(LevelID)).ToList();
            ViewBag.cd = new SelectList(ct, "ConceptID", "Question");//tham số thứ chứa Field load lên
           //=--------------
            ViewBag.levelId = LevelID;
            ViewBag.topicId = topicId1;
            //--------------------------
            return PartialView(ct);
        }

        //giúp giữ lại những câu hỏi thuộc Câp độ và CHủ đề đang đứng
        [HttpPost, ActionName("dropdown")]
        public PartialViewResult dropdownzzz(String LevelID, String topicId1,string Bien1)
        {
            List<ConceptsForTopic> ct =
                db.ConceptsForTopics.ToList();
            ViewBag.cd = new SelectList(ct, "ConceptID", "Question");//tham số thứ chứa Field load lên

            return PartialView();
        }

        // cai nay la get
        // ta lam them 1 cai post
        //mới
        string t1=null;
        string t2=null;
        //tui mun lấy gtri LevelID1, đưa wa Indexxxxx
        public ActionResult Index(String LevelID1, String topicId1, string ConceptID)
        {
            ViewBag.levelID2 = LevelID1;
            ViewBag.topicID2 = topicId1;
           
            /*
            //--------------------------------------------
            List<ConceptsForTopic> conceptForTopics;
            conceptForTopics = db.ConceptsForTopics.Where(p => p.TopicID.Equals(topicId1)).ToList();
            List<ConceptsForTopic> conceptForTopics2 = new List<ConceptsForTopic>();
            ConceptsForTopic concept = new ConceptsForTopic();

            foreach (var k in conceptForTopics)
            {
                string[] catchuoi = k.Levels.Split(new char[] { ',' });
                foreach (string s1 in catchuoi)
                {

                    if (s1.Trim() != "")
                    {
                        if (s1.ToString().Equals(LevelID1))
                        {
                            if (ConceptID != null)
                                concept = conceptForTopics.Find(p => p.ConceptID.Equals(k.ConceptID).Equals(ConceptID));
                            else
                                concept = conceptForTopics.Find(p => p.ConceptID.Equals(k.ConceptID));

                            conceptForTopics2.Add(concept);

                        }
                    }

                }

            }*/
            if (ConceptID == null)
                ConceptID = db.ConceptsForTopics.FirstOrDefault(c => c.TopicID.Equals(topicId1)&&c.Levels.Contains(LevelID1)).ConceptID;
            Map m = getMap(LevelID1, topicId1, ConceptID);
            return View(m);
        }

        [HttpPost, ActionName("Index")]
        public ActionResult Indexxx(String LevelID1, String topicId1, String ConceptID)
        {
            Map m = getMap(LevelID1, topicId1, ConceptID);
            return View(m);
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

        
       
        //public PartialViewResult _Concept_Topic_LevelPartial(String LevelID, String TopicID,String ConceptID)
        //{
        //    List<ConceptsForTopic> conceptForTopics;
        //    conceptForTopics = db.ConceptsForTopics.Where(p => p.TopicID.Equals(TopicID)).ToList();
        //    List<ConceptsForTopic> conceptForTopics2 = new List<ConceptsForTopic>();
        //    ConceptsForTopic concept = new ConceptsForTopic();

        //    foreach (var k in conceptForTopics)
        //    {
        //        string[] catchuoi = k.Levels.Split(new char[] { ',' });
        //        foreach (string s1 in catchuoi)
        //        {

        //            if (s1.Trim() != "")
        //            {
        //                if (s1.ToString().Equals(LevelID))
        //                {
        //                    if(ConceptID!=null)
        //                    concept = conceptForTopics.Find(p => p.ConceptID.Equals(k.ConceptID).Equals(ConceptID));
        //                    else
        //                        concept = conceptForTopics.Find(p => p.ConceptID.Equals(k.ConceptID));

        //                    conceptForTopics2.Add(concept);

        //                }
        //            }

        //        }

        //    }

           // return PartialView(conceptForTopics2);
       // }

         //public PartialViewResult dropdownQuestion ( string idQues)
         //{
         //    ViewBag.a = idQues;
         //    return PartialView();
         //}
    }
}
