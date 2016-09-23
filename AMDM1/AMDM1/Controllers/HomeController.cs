using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using AmdmWeb.Models;

namespace AmdmWeb.Controllers
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
            IndexModel im = new IndexModel { Performers = myList, Set = set };
            return View(im);
        }
       
        public ActionResult Song(int id=0)
        {
            var db = new AmdmContext();
            var song = db.Songs.SingleOrDefault(x => x.Id == id);
            return View(song);
            
        }
      

        
        [HttpPost]
        public ActionResult SaveSong(AmdmWeb.Controllers.Song model)
        {
            
            
            return PartialView();
        }

        [HttpGet]
        public ActionResult PrevSong(int songId)
        {
            var db = new AmdmContext();
            Song song = db.Songs.SingleOrDefault(x => x.Id == songId);
            var newSong = db.Songs.Where(x => x.PerformerId == song.PerformerId).TakeWhile(x => x.Id != songId).Last();       


            return PartialView(newSong);
        }
        [HttpGet]
        public ActionResult NextSong(int songId)
        {
            var db = new AmdmContext();
            Song song = db.Songs.SingleOrDefault(x => x.Id == songId + 1);
            var newSong = db.Songs.Where(x => x.PerformerId == song.PerformerId).SkipWhile(x => x.Id != songId).First();
            return PartialView(song);
        }

        [HttpGet]
        public ActionResult PerformersList(int type, int page)
        {
            var db = new AmdmContext();
            List<Performer> list = new List<Performer>();
            if (type == 1)
            {
                list = db.Performers
                    .OrderBy(x => x.Id)
                    .Skip((page - 1) * 10)
                    .Take(10)
                    .ToList();                
            }
            if (type == 2)
            {
                list = db.Performers
                    .OrderBy(x => x.Name)
                    .Skip((page - 1) * 10)
                    .Take(10)
                    .ToList();             
            }
            if (type == 3)
            {
                list = db.Performers
                    .OrderBy(x => x.Songs.Count)
                    .Skip((page - 1) * 10)
                    .Take(10)
                    .ToList();               
            }

            List<MyPerformer> myList = new List<MyPerformer>();
            list.ForEach(x => myList.Add(new MyPerformer { Name = x.Name, Id = x.Id, SongAmount = x.Songs.Count, ImageLink = x.ImageLink }));
            Set set = new Set { SortType = type, Page = page, MaxPage = Convert.ToInt32(Math.Ceiling(db.Performers.Count() / 10.0)) };
            IndexModel im = new IndexModel { Performers = myList, Set = set };
            

            return PartialView(im);
        }
        public ActionResult Performer(int id = 0)
        {
            var db = new AmdmContext();
            var per = db.Performers.Where(x => x.Id == id).First();
            MyPerformerWithSongs myPerformerWithSongs = new MyPerformerWithSongs { Id = per.Id, Name = per.Name, BiographyText = per.BiographyText, ImageLink = per.ImageLink, PerformerPageLink = per.PerformerPageLink, Songs = new List<MySong>() };

            List<Performer> list = new List<Performer>();
           
            
                db.Songs
                   .OrderBy(x => x.Id)
                   .Where(x => x.PerformerId == id)
                   .Skip((1 - 1) * 10)
                   .Take(10)
                   .ToList()
                   .ForEach(x => myPerformerWithSongs.Songs.Add(new MySong { Id = x.Id, Name = x.Name, ChordsAmount = x.Chords.Count }));
            
           
          

            Set set = new Set { SortType = 1, Page = 1, MaxPage = Convert.ToInt32(Math.Ceiling(per.Songs.Count() / 10.0)) };

            PerformersModel pm = new PerformersModel { Performer = myPerformerWithSongs, Set = set };

            return View(pm);
        }
        [HttpGet]
        public ActionResult SongList(int performerId, int type, int page)
        {
            var db = new AmdmContext();
            var per = db.Performers.Where(x => x.Id == performerId).First();
            MyPerformerWithSongs myPerformerWithSongs = new MyPerformerWithSongs { Id = per.Id, Name = per.Name, BiographyText = per.BiographyText, ImageLink = per.ImageLink, PerformerPageLink = per.PerformerPageLink, Songs = new List<MySong>() };

            List<Performer> list = new List<Performer>();
            var songs = db.Songs.AsQueryable();
            if (type == 1)
            {
                songs = songs.OrderBy(x => x.Id);
                 
            }
            if (type == 2)
            {
                db.Songs
                   .OrderBy(x => x.Name)
                   .Where(x => x.PerformerId == performerId)
                   .Skip((page - 1) * 10)
                   .Take(10)
                   .ToList()
                   .ForEach(x => myPerformerWithSongs.Songs.Add(new MySong { Id = x.Id, Name = x.Name, ChordsAmount = x.Chords.Count }));
            }
            if (type == 3)
            {
                db.Songs
                   .OrderBy(x => x.Chords.Count)
                   .Where(x => x.PerformerId == performerId)
                   .Skip((page - 1) * 10)
                   .Take(10)
                   .ToList()
                   .ForEach(x => myPerformerWithSongs.Songs.Add(new MySong { Id = x.Id, Name = x.Name, ChordsAmount = x.Chords.Count }));
            }
            songs.Where(x => x.PerformerId == performerId)
                    .Skip((page - 1) * 10)
                    .Take(10)
                    .ToList()
                    .ForEach(x => myPerformerWithSongs.Songs.Add(new MySong { Id = x.Id, Name = x.Name, ChordsAmount = x.Chords.Count }));
            Set set = new Set { SortType = type, Page = page, MaxPage = Convert.ToInt32(Math.Ceiling(per.Songs.Count() / 10.0)) };
            
            PerformersModel pm = new PerformersModel { Performer = myPerformerWithSongs, Set = set };



            return PartialView(pm);
        }


    }
}