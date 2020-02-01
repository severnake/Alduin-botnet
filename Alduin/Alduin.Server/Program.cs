using System;
using Alduin.Server.Modules;
namespace Alduin.Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            ConfigTor.StartTor();
        }
    }
}
