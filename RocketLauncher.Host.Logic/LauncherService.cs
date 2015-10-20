using Microsoft.TeamFoundation.WorkItemTracking.Client;

using RocketLauncher.Host.Data;
using RocketLauncher.Host.Data.Models;
using RocketLauncher.Host.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RocketLauncher.Host.Logic
{
    public class LauncherService : IDisposable
    {
        private LauncherContext _context;

        public LauncherService()
        {
            _context = new LauncherContext();
        }

        public void LauncherOnline(string name)
        { 
            // determine if it exists
            var foundLauncher = _context.LauncherClients.FirstOrDefault(t => t.Name.ToLower().Equals(name.ToLower()));
            if (foundLauncher != null)
            {
                // the launcher already exists
                foundLauncher.LastOnline = DateTime.Now;
            }
            else
            {
                _context.LauncherClients.Add(new LauncherClient { Name = name, Created = DateTime.Now, LastOnline = DateTime.Now });
            }

            _context.SaveChanges();
        }

        public void CreateLauncher(string name)
        {
            if (_context.LauncherClients.Any(t => t.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new ArgumentException("Launcher with name " + name + " already exists.");
            }

            _context.LauncherClients.Add(new LauncherClient { Name = name, Created = DateTime.Now, LastOnline = DateTime.Now });
            _context.SaveChanges();
        }

        public IEnumerable<Launcher> GetLaunchers()
        {
            return _context.LauncherClients.ToList()
                .Select(t => new Launcher() { Name = t.Name, IsOnline = LauncherHub.AvailableLaunchers.Any(b => b.Equals(t.Name, StringComparison.InvariantCultureIgnoreCase)), LastOnline = t.LastOnline });
        }

        public void RemoveLauncher(string name)
        {
            var launcher = _context.LauncherClients.SingleOrDefault(t => t.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (launcher != null)
            {
                _context.LauncherClients.Remove(launcher);
                _context.SaveChanges();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
