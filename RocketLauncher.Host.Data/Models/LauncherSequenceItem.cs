using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLauncher.Host.Data.Models
{
    public class LauncherSequenceItem
    {
        public int Id { get; set; }

        public string Command { get; set; }

        public int TimeInMs { get; set; }

        public int LauncherSequenceId { get; set; }

        public LauncherSequence LauncherSequence { get; set; }
    }
}
