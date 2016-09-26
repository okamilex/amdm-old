using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using AmdmLogic;
using AmdmData;

namespace AmdmConsole
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            int count = Logic.GetPerformersCount();
            for (int i = 1; i <= count; i++)
            {
                var s = Logic.GetSongsCount(i);
                logger.Trace("Performer "+i+" : "+s+" songs");
            }

        }
    }
}
