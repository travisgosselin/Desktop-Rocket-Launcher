using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLauncher.Host.Data.Models
{
    public class LauncherSequence
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int LauncherClientId { get; set; }

        public LauncherClient LauncherClient { get; set; }

        public virtual ICollection<LauncherSequenceItem> LauncherSequenceItems { get; set; } 
    }
}
