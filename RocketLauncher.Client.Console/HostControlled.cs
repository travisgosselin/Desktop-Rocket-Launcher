using Microsoft.AspNet.SignalR.Client;
using RocketLauncher.Api;

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;

using RocketLauncher.Host.Contracts;

namespace RocketLauncher.Client.Console
{
    public class HostControlled
    {
        private Launcher _launcher;

        public HostControlled(Launcher launcher)
        {
            _launcher = launcher;
        }

        public void Run()
        {
            System.Console.WriteLine("Starting connection to host at: {0} with launcher identifier name as {1}", AppSettings.HostUrl, AppSettings.LauncherName);
            
            var hubConnection = new HubConnection(AppSettings.HostUrl);
            IHubProxy hubProxy = hubConnection.CreateHubProxy("LauncherHub");
            hubProxy.On<LauncherCommand>("sendCommand", OnSendCommand);
            hubProxy.On<IEnumerable<LauncherSequence>>("sendSequence", OnSendSequence);
            ServicePointManager.DefaultConnectionLimit = 10;
            hubConnection.Start().Wait();

            hubConnection.StateChanged += change =>
            {
                System.Console.WriteLine("Connection to host state has changed to : " + change.NewState);
                if (change.NewState == ConnectionState.Connected)
                {
                    hubProxy.Invoke("Initialize", AppSettings.LauncherName);
                }
            };

            hubProxy.Invoke("Initialize", AppSettings.LauncherName);
            System.Console.WriteLine("Connection established.... waiting for commands from host...");
            while (true)
            {
                // let the app know we are health every 1000 (Ms = 1 second) * 60 sec = 1 min * 
                Thread.Sleep(1000 * 60 * 5);
                hubProxy.Invoke("Initialize", AppSettings.LauncherName);
            }
        }

        private void OnSendCommand(LauncherCommand command)
        {
            System.Console.WriteLine("Command received.... {0}", command);
            var commandBytes = command.ConvertCommandToBytes();
            if (commandBytes != null)
            {
                _launcher.SendCommand(commandBytes);
            }
        }

        private void OnSendSequence(IEnumerable<LauncherSequence> sequence)
        {
            System.Console.WriteLine("Sequence received.... {0}", sequence.Select(t => t.Command.ToString()).Join(","));
            System.Console.WriteLine("Resetting position for sequence...");
            ResetPosition();

            System.Console.WriteLine("Beginning Sequence execution...");
            foreach (var item in sequence)
            {
                var commandBytes = item.Command.ConvertCommandToBytes();
                if (commandBytes != null)
                {
                    _launcher.SendCommand(commandBytes);
                    if(item.Length > 0 && item.Command != LauncherCommand.Fire)
                    {
                        Thread.Sleep(item.Length);
                        _launcher.SendCommand(ByteCommands.Stop);
                    }
                }
            }
        }

        private void ResetPosition()
        {
            _launcher.SendCommand(ByteCommands.Left);
            Thread.Sleep(5000);
            _launcher.SendCommand(ByteCommands.Down);
            Thread.Sleep(1500);
        }
    }
}
