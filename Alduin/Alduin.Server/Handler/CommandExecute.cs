using Alduin.Server.Commands;
using Alduin.Server.Interfaces;
using MediatR;

namespace Alduin.Server.Handler
{
    public class CommandExecute : ICommand
    {
        public CommandExecute()
        {
            
        }
        public object Send(object botlist, ExecuteCommand model)
        {
            throw new System.NotImplementedException();
        }
    }
}
