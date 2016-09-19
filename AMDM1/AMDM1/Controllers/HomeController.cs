using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace AMDM1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new AmdmContext();
            var per = db.Performers;
            return View(per);
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
    }
}