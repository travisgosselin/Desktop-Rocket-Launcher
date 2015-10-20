using RocketLauncher.Api;
using System;

namespace RocketLauncher.Client.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // all output is wrapped in try catch
            try
            {
                // try and initialize the launcher to see if we can connect
                System.Console.WriteLine("Attempting to initialize the launcher...");
                var rocket = new Launcher();
                rocket.Connect();
                System.Console.WriteLine("SUCCESS!");

                // turn on LED to indicate its connected
                System.Console.WriteLine("LED on to indicate connection.");
                rocket.SendCommand(ByteCommands.LedOn);

                // determine the mode we are in to run manual or host controlled
                var isManual = AppSettings.Mode.Equals("manual", StringComparison.InvariantCultureIgnoreCase);
                if (isManual)
                {
                    // running in manual control
                    System.Console.WriteLine("Manual control mode is enabled...");
                    var manualControl = new ManualControlled(rocket);
                    manualControl.Run();
                }
                else
                {
                    // running in host control
                    System.Console.WriteLine("Host control mode is enabled...");
                    var hostControlled = new HostControlled(rocket);
                    hostControlled.Run();
                }

                // turn off LED to indicate its not connected
                System.Console.WriteLine("LED off in order to shut down.");
                rocket.SendCommand(ByteCommands.LedOff);
                rocket.Disconnect();
                System.Console.WriteLine("Disconnected from the launcher....press any key to exit.");
                System.Console.ReadLine();

            }
            catch (Exception ex)
            {
                // log output so the user knows what went wrong
                System.Console.WriteLine("Error occured running launcher: {0}, STACK TRACE: {1}", ex.Message, ex.StackTrace);
                System.Console.WriteLine("Press any key to exit.");
                System.Console.ReadLine();
            }
        }
    }
}
