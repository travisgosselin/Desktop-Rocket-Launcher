using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

using RocketLauncher.Host.Contracts;
using RocketLauncher.Host.Logic;

namespace RocketLauncher.Host.Web.Api
{
    public class CommandController : ApiController
    {
        public void Get([FromUri]string id, [FromUri]string key) 
        {
            LauncherCommand command;
            var launcherName = id; 
            switch (key.ToLower())
            {
                case "up":
                    command = LauncherCommand.Up;
                    break;
                case "down":
                    command = LauncherCommand.Down;
                    break;
                case "left":
                    command = LauncherCommand.Left;
                    break;
                case "right":
                    command = LauncherCommand.Right;
                    break;
                case "fire":
                    command = LauncherCommand.Fire;
                    break;
                case "stop":
                    command = LauncherCommand.Stop;
                    break;
                case "wait":
                    command = LauncherCommand.Wait;
                    break;
                case "LedOn":
                    command = LauncherCommand.LedOn;
                    break;
                case "LedOff":
                    command = LauncherCommand.LedOff;
                    break;
                default:
                    command = LauncherCommand.Stop;
                    break;
            }

            LauncherHub.SendCommand(new List<string> { launcherName }.ToArray(), command);
        }

        public void Post([FromUri]string id, [FromBody]List<Contracts.LauncherSequence> sequences)
        {
            var launcherName = id;
            LauncherHub.SendSequence(new List<string> { launcherName }.ToArray(), sequences);
        }
    }
}