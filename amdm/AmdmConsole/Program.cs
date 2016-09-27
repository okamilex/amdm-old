using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using AmdmLogic;
using AmdmData;
using Common;
using System.Net;

namespace AmdmConsole
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            var idList = Logic.GetPerformersId();
            idList.ForEach(id => {
                var s = Logic.GetSongsCount(id);
                AmdmLogger.Trace("Performer " + id + " : " + s + " songs");
            });            
            var request = WebRequest.CreateHttp("http://localhost:49992/Log/Loged");
            request.GetResponse();


        }
    }
}
