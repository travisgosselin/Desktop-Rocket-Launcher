using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;

namespace RocketLauncher.Host.Logic
{
    public class TfsSubscriptionService : IDisposable
    {
        private readonly string _deliveryAddress;

        public TfsSubscriptionService()
        {
            _deliveryAddress = ConfigurationManager.AppSettings["TfsEventSubscription"];
        }

        public bool DoesSubscriptionExist(string userId, string tfsUrl)
        {
            var projectCollection = new TfsTeamProjectCollection(new Uri(tfsUrl));
            var eventService = projectCollection.GetService<IEventService>();
            var items = eventService.GetEventSubscriptions(userId);
            var itemExists = items.Any(t => t.DeliveryPreference != null && t.DeliveryPreference.Address.Equals(_deliveryAddress, StringComparison.InvariantCultureIgnoreCase));

            return itemExists;
        }

        public bool CreateSubscription(string userId, string tfsUrl)
        {
            if (DoesSubscriptionExist(userId, tfsUrl))
            {
                return false;
            }

            var projectCollection = new TfsTeamProjectCollection(new Uri(tfsUrl));
            var eventService = projectCollection.GetService<IEventService>();

            var delPrev = new DeliveryPreference();
            delPrev.Type = DeliveryType.Soap;
            delPrev.Schedule = DeliverySchedule.Immediate;
            delPrev.Address = _deliveryAddress;

            eventService.SubscribeEvent(userId, "BuildCompletionEvent", string.Empty, delPrev);

            return true;
        }

        public void Dispose()
        {
            
        }
    }
}
