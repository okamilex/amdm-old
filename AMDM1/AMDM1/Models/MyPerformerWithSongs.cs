using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMDM1.Models
{
    public class MyPerformerWithSongs
    {        
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public string PerformerPageLink { get; set; }
        public string BiographyText { get; set; }
        public List<MySong> Songs { get; set; }
    }
}