﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmdmData;
using AmdmData.Enums;

namespace AmdmLogic
{
    public class Logic
    {
        public static List<Performers> GetPageOfPerformerList(PerformersSortingTypes performersSortingType, int performersPageNumber, int pageSize)
        {
            return PerformerData.GetPageOfPerformerList(performersSortingType, performersPageNumber, pageSize);
        }
        public static int GetPerformersCount()
        {
            return PerformerData.GetPerformersCount();
        }
        
        ////////////////////////////////////////////////////////
       
        public static Performers GetPerformerById(int performerId)
        {
            return PerformerData.GetPerformerById(performerId);
        }
        public static List<Songs> GetPageOfSongList(int performerId, SongsSortingTypes songsSortingType, int songPageNumber, int pageSize)
        {
            return PerformerData.GetPageOfSongList(performerId, songsSortingType, songPageNumber, pageSize);
        }
        public static int GetSongsCount(int performerId)
        {
            return PerformerData.GetSongsCount(performerId);
        }

        /////////////////////////////////////////////////////////
        
        public static Songs GetSongById(int songId)
        {
            return PerformerData.GetSongById(songId);
        }        
        public static int GetNumber(int songId, SongsSortingTypes s)
        {
            var song = PerformerData.GetSongById(songId);
            var list = PerformerData.GetPageOfSongList((int)song.PerformerId, s, 1, PerformerData.GetSongsCount((int)song.PerformerId));
            return list.FindIndex(x => x.Id == songId)+1;            
        }

        /////////////////////////////////////////////////////////

        public static bool EditSong(int id, string name, string text, string chords)
        {
            return PerformerData.EditSong(id, name, text, chords);
        }

        /////////////////////////////////////////////////////////

        public static List<int> GetPerformersId()
        {
            return PerformerData.GetPerformersId();
        }







        public static string GetPerformerNameById(int performerId)
        {
            return PerformerData.GetPerformerNameById(performerId);
        }
        public static string GetAllChords()
        {
            return PerformerData.GetAllChords();
        }
        public static Chords GetChord(string name)
        {
            return PerformerData.GetChord(name);
        }
        public static List<Chords> GetChords(string s)
        {
            return PerformerData.GetChords(s);
        }
    }
}
