using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLauncher.Host.Logic.Model
{
    public class Launcher
    {
        public string Name { get; set; }

        public DateTime LastOnline { get; set; }

        public bool IsOnline { get; set; }
    }
}
