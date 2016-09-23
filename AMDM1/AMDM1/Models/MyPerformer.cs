using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmdmWeb.Models
{
    public class MyPerformer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public int SongAmount { get; set; }
    }
}