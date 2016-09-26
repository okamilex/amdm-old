using AmdmData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmdmLogic;
using AmdmWeb.Models.Song;
using AmdmWeb.Models;
using AmdmData;

namespace AmdmWeb.Controllers
{
    public class SongController : Controller
    {
        public ActionResult Song()
        {
            return View();
        }

        public ActionResult SongEditPage(int songId, SongsSortingTypes s = SongsSortingTypes.ByName)
        {
            var song = Logic.GetSongById(songId);
            SongEditModel songEditModel = new SongEditModel
            {
                Id = song.Id,
                Name = song.Name,
                Text = song.Text,
                Number = Logic.GetNumber(songId, s),
                Chords = "",
                SongsSortingType = s
            };
            song.Chords.ToList().ForEach(x => songEditModel.Chords = songEditModel.Chords + x.Name + ",");
            
            if (songEditModel.Chords.Length > 0)
            {
                songEditModel.Chords = songEditModel.Chords.Substring(0, songEditModel.Chords.Length - 1);
            }
            return View(songEditModel);
        }

        [HttpGet]
        public ActionResult SongMain(int performerId = 1, SongsSortingTypes s = SongsSortingTypes.ByName, int songNumber = 1)
        {
            var song = Logic.GetPageOfSongList(performerId, s, songNumber, 1).First();
            var songModel = new SongModel
            {
                Id = song.Id,
                Number = songNumber,
                Name = song.Name,
                Chords = song.Chords.ToList(),
                PerformerId = (int)song.PerformerId,
                SongPageLink = song.SongPageLink,
                Text = song.Text,
                VideoLink = song.VideoLink,
                Views = song.Views
            };
            SortAndPageData sortAndPageData = new SortAndPageData
            {
                LasPageNumber = Logic.GetSongsCount(performerId),
                SongsSortingType = s
            };
            SongPageModel songPageModel = new SongPageModel
            {
                Song = songModel,
                BackPage = songNumber / Const.PageSize + 1,
                SortAndPageData = sortAndPageData,
                PerfoemerName = song.Performers.Name
            };
            return PartialView(songPageModel);
        }


/////////////////////////////////////////////////////////////////////////////


        [HttpGet]
        public ActionResult GetSong(int performerId = 1, SongsSortingTypes s = SongsSortingTypes.ByName, int songNumber = 1)
        {
            var song = Logic.GetPageOfSongList(performerId, s, songNumber, 1).First();
            var songModel = new SongModel
            {
                Id = song.Id,
                Number = songNumber,
                Name = song.Name,
                Chords = song.Chords.ToList(),
                PerformerId = (int)song.PerformerId,
                SongPageLink = song.SongPageLink,
                Text = song.Text,
                VideoLink = song.VideoLink,
                Views = song.Views
            };
            SortAndPageData sortAndPageData = new SortAndPageData
            {                
                LasPageNumber = Logic.GetSongsCount(performerId),
                SongsSortingType = s
            };
            SongPageModel songPageModel = new SongPageModel
            {
                Song = songModel,
                BackPage = songNumber / Const.PageSize + 1,
                SortAndPageData = sortAndPageData,
                PerfoemerName = song.Performers.Name
            };
            return PartialView(songPageModel);
        }
        [HttpGet]
        public ActionResult SongInfo(int performerId = 1, SongsSortingTypes s = SongsSortingTypes.ByName, int songNumber = 1)
        {
            var song = Logic.GetPageOfSongList(performerId, s, songNumber, 1).First();            
            return PartialView(song);
        }


/// /////////////////////////////////////////////////////////////////////


        [HttpGet]
        public ActionResult EditSong(int songId)
        {
            var song = Logic.GetSongById(songId);
            SongEditModel songEditModel = new SongEditModel
            {
                Id = song.Id,
                Name = song.Name,
                Text = song.Text,
                Chords = ""                            
            };
            song.Chords.ToList().ForEach(x => songEditModel.Chords = songEditModel.Chords + x.Name + ",");
            if (songEditModel.Chords.Length > 0)
            {
                songEditModel.Chords = songEditModel.Chords.Substring(0, songEditModel.Chords.Length - 1);
            }
            return PartialView(songEditModel);
        }
        [HttpGet]
        public ActionResult CloseEditSong()
        {            
            return PartialView();
        }
        [HttpPost]
        public ActionResult SaveSong(SongEditModel model)
        {
            PerformerData.EditSong(model.Id, model.Name, model.Text, model.Chords);
            return PartialView();
        }
        
    }
}