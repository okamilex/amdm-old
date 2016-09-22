using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AmdmLogic;
using AmdmWeb.Models.Home;
using AmdmWeb.Models;
using AmdmData.Enums;

namespace AmdmWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(PerformersSortingTypes performersSortingType = PerformersSortingTypes.ById, SongsSortingTypes songSortingType = SongsSortingTypes.ById, int performersPageNumber = 1, int songPageNumber = 1)
        {
            List<PerformerInHomeModel> performers = new List<PerformerInHomeModel>();
            Logic.GetListOfPerformers(performersSortingType, performersPageNumber, 10).ForEach(x => performers.Add(new PerformerInHomeModel { Name = x.Name, Id = x.Id, SongCount = x.Songs.Count, ImageLink = x.ImageLink }));
            SortAndPageData sortAndPageData = new SortAndPageData { PerformersPageNumber = performersPageNumber, SongsPageNumber = songPageNumber, LasPageNumber = Convert.ToInt32(Math.Ceiling(Logic.GetPerformersCount() / 10.0)), PerformersSortingType = performersSortingType, SongsSortingType = songSortingType };
            HomePageModel homePageModel = new HomePageModel { Performers = performers, SortAndPageData = sortAndPageData };           
            return View(homePageModel);        
        }


        [HttpGet]
        public ActionResult PerformersPage(PerformersSortingTypes performersSortingType, SongsSortingTypes songSortingType, int performersPageNumber, int songPageNumber)
        {
            List<PerformerInHomeModel> performers = new List<PerformerInHomeModel>();
            Logic.GetListOfPerformers(performersSortingType, performersPageNumber, 10).ForEach(x => performers.Add(new PerformerInHomeModel { Name = x.Name, Id = x.Id, SongCount = x.Songs.Count, ImageLink = x.ImageLink }));
            SortAndPageData sortAndPageData = new SortAndPageData { PerformersPageNumber = performersPageNumber, SongsPageNumber = songPageNumber, LasPageNumber = Convert.ToInt32(Math.Ceiling(Logic.GetPerformersCount() / 10.0)), PerformersSortingType = performersSortingType, SongsSortingType = songSortingType };
            HomePageModel homePageModel = new HomePageModel { Performers = performers, SortAndPageData = sortAndPageData };

            return PartialView(homePageModel);
        }


    }
}