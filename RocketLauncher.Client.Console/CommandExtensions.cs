using System.Collections;
using System.Collections.Generic;
using System.Linq;

using RocketLauncher.Api;
using RocketLauncher.Host.Contracts;

namespace RocketLauncher.Client.Console
{
    public static class CommandExtensions
    {
        public static byte[] ConvertCommandToBytes(this LauncherCommand command)
        {
            byte[] bytes;
            switch (command)
            {
                case LauncherCommand.Up:
                    bytes = ByteCommands.Up;
                    break;
                case LauncherCommand.Down:
                    bytes = ByteCommands.Down;
                    break;
                case LauncherCommand.Left:
                    bytes = ByteCommands.Left;
                    break;
                case LauncherCommand.Right:
                    bytes = ByteCommands.Right;
                    break;
                case LauncherCommand.Fire:
                    bytes = ByteCommands.Fire;
                    break;
                case LauncherCommand.Stop:
                    bytes = ByteCommands.Stop;
                    break;
                default:
                    bytes = null;
                    break;
            }

            return bytes;
        }

        public static string Join(this IEnumerable<string> items, string seperator)
        {
            var joined = string.Empty;
            items.ToList().ForEach(t => joined += t + seperator);

            return joined;
        }
    }
}
