using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using RocketLauncher.Host.Contracts;

namespace RocketLauncher.Host.Logic
{
    public class LauncherHub : Hub
    {
        private readonly LauncherService _launcherService = new LauncherService();

        public static List<string> AvailableLaunchers = new List<string>(); 

        public void Initialize(string launcherName)
        {
            if (!AvailableLaunchers.Contains(launcherName))
            {
                AvailableLaunchers.Add(launcherName);
            }

            _launcherService.LauncherOnline(launcherName);
            this.Groups.Add(this.Context.ConnectionId, launcherName);
        }

        public static void SendCommand(string[] launcherNames, LauncherCommand command)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<LauncherHub>();
            context.Clients.Groups(launcherNames).sendCommand(command);
        }

        public static void SendSequence(string[] launcherNames, IEnumerable<Contracts.LauncherSequence> sequences)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<LauncherHub>();
            context.Clients.Groups(launcherNames).sendSequence(sequences);
        }
    }
}