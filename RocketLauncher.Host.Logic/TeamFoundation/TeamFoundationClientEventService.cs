using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

using Elmah;

using RocketLauncher.Host.Contracts;

namespace RocketLauncher.Host.Logic.TeamFoundation
{
    public class TeamFoundationClientEventService : ITeamFoundationEventService
    {
        private SequenceService _service = new SequenceService();

        public void Notify(string eventXml, string tfsIdentityXml)
        {
            // validate time is working hours
            if (!IsWorkingDay(DateTime.Now) || !IsTimeOfDayBetween(DateTime.Now, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0)))
            {
                // do not fire out of mon-fri, 9-5.
                return;
            }

            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(eventXml);

                var requestedBy = doc.GetElementsByTagName("RequestedBy").Item(0);
                var name = requestedBy.InnerText;

                var completionStatus = doc.GetElementsByTagName("CompletionStatus").Item(0);
                var status = completionStatus.InnerText;

                if (!status.Equals("Successfully Completed"))
                {
                    // this failed by the name above, find that person
                    var personName = name.Replace(@"TOOLBOX\", string.Empty);
                    var sequences = _service.GetSequencesByName(personName);
                    foreach (var sequence in sequences)
                    {
                        var commands = sequence.Sequences.Select(t => 
                                new LauncherSequence
                                {
                                    Command = (LauncherCommand)Enum.Parse(typeof(LauncherCommand), t.Command, true), 
                                    Length = t.Length 
                                
                                });
                        if (LauncherHub.AvailableLaunchers.Any(t => t.Equals(sequence.LauncherName, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            LauncherHub.SendSequence(new[] { sequence.LauncherName }, commands);
                        }
                        else
                        {
                            ErrorSignal.FromCurrentContext().Raise(new Exception(personName  + " failed the build, and found a sequence for them in " + sequence.LauncherName + " but that launcher is not online."));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(new Exception("Error in notification sequence: " + ex.Message + ". " + eventXml, ex));
            }
        }

        private  bool IsTimeOfDayBetween(DateTime time, TimeSpan startTime, TimeSpan endTime)
        {
            if (endTime == startTime)
            {
                return true;
            }
            else if (endTime < startTime)
            {
                return time.TimeOfDay <= endTime ||
                    time.TimeOfDay >= startTime;
            }
            else
            {
                return time.TimeOfDay >= startTime &&
                    time.TimeOfDay <= endTime;
            }

        }

        private bool IsWorkingDay(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
            {
                return false;
            }

            return true;
        }
    }
}
