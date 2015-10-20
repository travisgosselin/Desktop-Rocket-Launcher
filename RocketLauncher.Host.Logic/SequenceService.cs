using System.Data;

using RocketLauncher.Host.Contracts;
using RocketLauncher.Host.Data;
using RocketLauncher.Host.Data.Models;
using RocketLauncher.Host.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using LauncherSequence = RocketLauncher.Host.Data.Models.LauncherSequence;

namespace RocketLauncher.Host.Logic
{
    public class SequenceService : IDisposable
    {
        private LauncherContext _context;

        public SequenceService()
        {
            _context = new LauncherContext();
        }

        public IEnumerable<Sequence> GetSequences(string launcherName = null)
        {
            var sequences = _context.LauncherSequences
                .Include(t => t.LauncherClient)
                .Include(t => t.LauncherSequenceItems)
                .Where(t => t.LauncherClient.Name.Equals(launcherName, StringComparison.InvariantCultureIgnoreCase) || launcherName == null);

            return sequences.Select(t => new Sequence
            {
                LauncherName = t.LauncherClient.Name,
                Name = t.Name,
                Sequences = t.LauncherSequenceItems.Select(b => new SequenceItem
                {
                    Command = b.Command,
                    Length = b.TimeInMs
                }).ToList()
            });
        }

        public IEnumerable<Sequence> GetSequencesByName(string sequenceName)
        {
            if (string.IsNullOrWhiteSpace(sequenceName))
            {
                throw new ArgumentNullException("sequenceName");
            }

            var sequences = _context.LauncherSequences
                .Include(t => t.LauncherClient)
                .Include(t => t.LauncherSequenceItems)
                .Where(t => t.Name.Equals(sequenceName, StringComparison.InvariantCultureIgnoreCase));

            return sequences.Select(t => new Sequence
            {
                LauncherName = t.LauncherClient.Name,
                Name = t.Name,
                Sequences = t.LauncherSequenceItems.Select(b => new SequenceItem
                {
                    Command = b.Command,
                    Length = b.TimeInMs
                }).ToList()
            });
        }

        public void Add(Sequence sequence)
        {
            // see if sequence exists
            var anySequences = _context.LauncherSequences
                .Any(t => t.Name.Equals(sequence.Name, StringComparison.InvariantCultureIgnoreCase)
                    && t.LauncherClient.Name.Equals(sequence.LauncherName, StringComparison.InvariantCultureIgnoreCase));
            if (anySequences)
            {
                throw new DataException("Sequence in this launcher with this name already exists: " + sequence.Name + ", " + sequence.LauncherName);
            }

            var launcher = _context.LauncherClients.Single(t => t.Name.Equals(sequence.LauncherName, StringComparison.InvariantCultureIgnoreCase));
            var toAdd = new LauncherSequence
            {
                LauncherClientId = launcher.Id,
                Name = sequence.Name,
                LauncherSequenceItems = sequence.Sequences.Select(b => new LauncherSequenceItem
                                                                            {
                                                                                Command = b.Command,
                                                                                TimeInMs = b.Length
                                                                            }).ToList()
            };

            _context.LauncherSequences.Add(toAdd);
            _context.SaveChanges();
        }

        public void Remove(string launcherName, string sequenceName)
        {
            var sequences = _context.LauncherSequences
               .FirstOrDefault(t => t.Name.Equals(sequenceName, StringComparison.InvariantCultureIgnoreCase)
                   && t.LauncherClient.Name.Equals(launcherName, StringComparison.InvariantCultureIgnoreCase));
            if (sequences != null)
            {
                _context.LauncherSequences.Remove(sequences);
                _context.SaveChanges();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
