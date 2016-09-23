using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmdmWeb.Models
{
    public class PerformersModel
    {
        public MyPerformerWithSongs Performer { get; set; }
        public Set Set { get; set; }
    }
}