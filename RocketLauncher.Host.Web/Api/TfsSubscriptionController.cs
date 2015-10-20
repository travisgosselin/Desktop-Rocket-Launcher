using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

using RocketLauncher.Host.Logic;

namespace RocketLauncher.Host.Web.Api
{
    [Authorize]
    public class TfsSubscriptionController : ApiController 
    {
        private readonly TfsSubscriptionService _subscriptionService = new TfsSubscriptionService();

        public bool Post(string userId, string tfsUrl)
        {
            return _subscriptionService.CreateSubscription(userId, tfsUrl);
        }
    }
}