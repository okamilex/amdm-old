using AmdmData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AmdmData
{
    public class PerformerData
    {
        static AmdmContext db = new AmdmContext();
        public static List<Performers> GetPageOfPerformerList(PerformersSortingTypes performersSortingType, int performersPageNumber, int pageSize)
        {            
            var performersContext = db.Performers.AsQueryable();
            

            switch (performersSortingType)
            {               
                case PerformersSortingTypes.ByName:
                    performersContext = performersContext.OrderBy(x => x.Name)
                        .Skip((performersPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
                case PerformersSortingTypes.ByNameBack:
                    performersContext = performersContext.OrderByDescending(x => x.Name)
                        .Skip((performersPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
                case PerformersSortingTypes.BySongCount:
                    performersContext = performersContext.OrderBy(x => x.Songs.Count)
                        .Skip((performersPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
                case PerformersSortingTypes.BySongCountBack:
                    performersContext = performersContext.OrderByDescending(x => x.Songs.Count)
                        .Skip((performersPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
                default:
                    break;
            }
            return performersContext.ToList();
        }
        public static int GetPerformersCount()
        {
            return db.Performers.Count();
        }
        public static Performers GetPerformerById(int performerId)
        {
            return db.Performers.Find(performerId);
        }
        public static List<Songs> GetPageOfSongList(int performerId, SongsSortingTypes songsSortingType, int songPageNumber, int pageSize)
        {
            var songs = new AmdmContext().Songs.Where(x => x.PerformerId == performerId).AsQueryable();
            switch (songsSortingType)
            {               
                case SongsSortingTypes.ByName:
                    songs = songs.OrderBy(x => x.Name)
                        .Skip((songPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
                case SongsSortingTypes.ByNameBack:
                    songs = songs.OrderByDescending(x => x.Name)
                        .Skip((songPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
                case SongsSortingTypes.ByChordCount:
                    songs = songs.OrderBy(x => x.Chords.Count)
                        .Skip((songPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
                case SongsSortingTypes.ByChordCountBack:
                    songs = songs.OrderByDescending(x => x.Chords.Count)
                        .Skip((songPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
                case SongsSortingTypes.ByViews:
                    songs = songs.OrderBy(x => x.Views)
                        .Skip((songPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
                case SongsSortingTypes.ByViewsBack:
                    songs = songs.OrderByDescending(x => x.Views)
                        .Skip((songPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
            }
            return songs.ToList();
        }
        public static int GetPerformerIdBySongId(int songId)
        {
            return (int) GetSongById(songId).PerformerId;
        }
        public static Songs GetSongById(int songId)
        {
            return db.Songs.Find(songId); 
        }
        public static int GetSongsCount(int performerId)
        {
            return db.Songs.Where(x => x.PerformerId == performerId).Count();
        }
        public static string GetPerformerNameById(int performerId)
        {
            return db.Performers.Find(performerId).Name;
        }
        public static string GetAllChords()
        {
            string s = "[";
            db.Chords.ToList().ForEach(x => s = s + ", { value: '" + x.Name + "'}");
            s = s + "],";
            return s;
        }

        public static bool EditSong(int id, string name, string text, string chords)
        {
            using (var context = db)
            {
                context.SaveChanges();
                
                var song = context.Songs.Find(id);
                song.Chords = GetChords(chords);
                song.Name = name;
                song.Text = text;              
                
                context.SaveChanges();
            }
            var ch = new AmdmContext().Songs.Find(id);
            return true;            
        }

        public static Chords GetChord(string name)
        {
            return db.Chords.FirstOrDefault(x => x.Name == name);
        }

        public static List<Chords> GetChords(string s)
        {
            var chordsNamesList = s.Split(',').ToList();
            var chordsList = db.Chords
                .Where(x =>
                Check(x.Name, chordsNamesList));
            return chordsList.ToList();
        }
        public static bool Check(string name, List<string> chords)
        {
            bool r = false;
            chords.ForEach(x => 
            {
                if (x == name)
                {
                    r = true;
                }
            });
            return r;
        }



    }
}
