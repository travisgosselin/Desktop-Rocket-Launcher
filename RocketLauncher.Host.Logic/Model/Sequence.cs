using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLauncher.Host.Logic.Model
{
    public class Sequence
    {
        public string LauncherName { get; set; }

        public string Name { get; set; }

        public List<SequenceItem> Sequences { get; set; } 
    }
}
