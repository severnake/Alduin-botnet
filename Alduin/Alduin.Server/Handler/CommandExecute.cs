using Alduin.Server.Commands;
using Alduin.Shared.DTOs;
using Alduin.Server.Connecter;
using System;

namespace Alduin.Server.Handler
{
    public class CommandExecute 
    {
        public static object TcpConnects(BotDTO[] botlist, object model)
        {
            for(var i = 0; i < botlist.Length; i++)
            {
                //var p = Connecter.CreateTcpSend(botlist[i].Domain, model);
            };
            throw new System.NotImplementedException();
        }
    }
}
