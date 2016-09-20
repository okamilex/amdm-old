using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using AMDM1.Models;

namespace AMDM1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new AmdmContext();
            var list = db.Performers.Where(x => x.Id < 11).ToList();
            Set set = new Set { SortType = 1, Page = 1, MaxPage = list.Count / 10 };
            PerformersModel pm = new PerformersModel { Performers = list, Set = set };            
            return View(pm);
        }
        public ActionResult Performer(int id=0)
        {
            var db = new AmdmContext();
            var per = db.Performers.Where(x => x.Id == id);
            return View(per.First());
        }
        public ActionResult Song(int id=0)
        {
            var db = new AmdmContext();
            var song = db.Songs.Where(x => x.Id == id);
            return View(song.First());
            
        }
        [HttpPost]
        public ActionResult PerformersList(int type, int page)
        {
            var db = new AmdmContext();
            var list = db.Performers.Where(x => ((x.Id > (page - 1) * 10) && (x.Id < page * 10 + 1))).ToList();
            Set set = new Set { SortType = type, Page = page, MaxPage = list.Count / 10 };
            PerformersModel pm = new PerformersModel { Performers = list, Set = set };
            

            return PartialView(pm);
        }
    }
}