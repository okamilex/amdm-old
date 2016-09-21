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
            List<MyPerformer> myList = new List<MyPerformer>();
            list.ForEach(x => myList.Add(new MyPerformer { Name = x.Name, Id = x.Id, SongAmount = x.Songs.Count, ImageLink = x.ImageLink }));
            Set set = new Set { SortType = 1, Page = 1, MaxPage = Convert.ToInt32(Math.Ceiling(db.Performers.Count() / 10.0)) };
            IndexModel pm = new IndexModel { Performers = myList, Set = set };
            return View(pm);
        }
        public ActionResult Performer(int id=0)
        {
            var db = new AmdmContext();
            var per = db.Performers.Where(x => x.Id == id).First();
            Set set = new Set { SortType = 1, Page = 1, MaxPage = Convert.ToInt32(Math.Ceiling(per.Songs.Count() / 10.0)) };
            MyPerformerWithSongs myPerformerWithSongs = new MyPerformerWithSongs { Name = per.Name, BiographyText = per.BiographyText, ImageLink = per.ImageLink, PerformerPageLink = per.PerformerPageLink, Songs = new List<MySong>() };
            per.Songs.Where(x => x.Id < 11).ToList().ForEach(x => myPerformerWithSongs.Songs.Add(new MySong { Name = x.Name, Id = x.Id, ChordsAmount = x.Chords.Count }));
            PerformersModel pm = new PerformersModel { Performer = myPerformerWithSongs, Set = set };

            return View(pm);
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
            List<Performer> list = new List<Performer>();
            if (type == 1)
            {
                var myDB = db.Performers.ToList();
                myDB.Sort((x, y) => x.Id.CompareTo(y.Id));
                list = myDB.GetRange((page - 1) * 10, 10);
            }
            if (type == 2)
            {
                var myDB = db.Performers.ToList();
                myDB.Sort( (x, y) => x.Name.CompareTo(y.Name));
                list = myDB.GetRange((page - 1) * 10, 10);
            }
            if (type == 3)
            {
                var myDB = db.Performers.ToList();
                myDB.Sort((x, y) => x.Songs.Count.CompareTo(y.Songs.Count));
                list = myDB.GetRange((page - 1) * 10, 10);
            }

            List<MyPerformer> myList = new List<MyPerformer>();
            list.ForEach(x => myList.Add(new MyPerformer { Name = x.Name, Id = x.Id, SongAmount = x.Songs.Count, ImageLink = x.ImageLink }));
            Set set = new Set { SortType = type, Page = page, MaxPage = Convert.ToInt32(Math.Ceiling(db.Performers.Count() / 10.0)) };
            IndexModel pm = new IndexModel { Performers = myList, Set = set };
            

            return PartialView(pm);
        }
    }
}