using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace AmdmWeb
{
    [HubName("myHub")]
    public class MyHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
        public void Send()
        {
            Clients.All.addMessage(DateTime.Now.ToShortTimeString());
        }
    }
}