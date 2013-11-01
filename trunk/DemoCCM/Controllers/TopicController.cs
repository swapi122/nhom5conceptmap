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

        public ActionResult Index(String LevelID1, String topicId1)
        {
            ViewBag.cd = new SelectList(db.ConceptsForTopics, "Question", "Question");
            ViewBag.levelID2 = LevelID1;
            ViewBag.topicID2 = topicId1;
            return View();

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

       
    }
}
