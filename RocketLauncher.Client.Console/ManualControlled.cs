using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using RocketLauncher.Api;

namespace RocketLauncher.Client.Console
{
    public class ManualControlled
    {
        private Launcher _launcher;

        public ManualControlled(Launcher launcher)
        {
            _launcher = launcher;
        }

        public void Run()
        {
            System.Console.WriteLine("Manual mode is ready to receive input (Up, Down, Left, Right, Spacebar - to fire, S - to exit)");
            var isRunning = true;
            while (isRunning)
            {
                var input = System.Console.ReadKey(true);
                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                        _launcher.SendCommand(ByteCommands.Up);
                        break;
                    case ConsoleKey.DownArrow:
                        _launcher.SendCommand(ByteCommands.Down);
                        break;
                    case ConsoleKey.LeftArrow:
                        _launcher.SendCommand(ByteCommands.Left);
                        break;
                    case ConsoleKey.RightArrow:
                        _launcher.SendCommand(ByteCommands.Right);
                        break;
                    case ConsoleKey.Spacebar:
                        _launcher.SendCommand(ByteCommands.Fire);
                        break;
                    case ConsoleKey.A:
                        _launcher.SendCommand(ByteCommands.Stop);
                        break;
                    case ConsoleKey.S:
                        isRunning = false;
                        break;
                }

                Thread.Sleep(1);
            }
        }
    }
}
